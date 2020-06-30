using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementSystem.Objects;

namespace TimeManagementSystem.Data
{
    /// <summary>
    /// Get data class
    /// </summary>
    public static partial class DataTransfer
    {
        /// <summary>
        /// Get data object from BD
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public static IBaseObject GetDataObject<T>(GetDataFilter filter)
        {
            if (typeof(T).Equals(typeof(Contact)))
                return GetData((GetDataFilterContact)filter);
            else
                if (typeof(T).Equals(typeof(BaseEvent)))
                    return GetData((EventDataFilter)filter);

            return null;
        }

        private static IBaseObject GetData(EventDataFilter filter)
        {
            var customer = new List<BaseEvent>();
            using (var conn = OpenConnection(ConnectionString))
            {
                if (filter.DayDate != DateTime.MinValue)
                {
                    customer = conn.Query<BaseEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE reg_date = @FilterRegDate ", new { FilterRegDate = filter.DayDate }).ToList();
                }
            }

            return customer.Count > 0 ? customer.First() : null;
        }

        private static IBaseObject GetData(GetDataFilterContact filter)
        {
            var customer = new List<Contact>();
            using (var conn = OpenConnection(ConnectionString))
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    customer = conn.Query<Contact>("SELECT id as Id, name as Name, phone as Phone, email as EMail, rec_date as RecDate, del_rec as DelRec FROM t_contacts WHERE UPPER(name) like @ContactName ", new { ContactName = filter.Name.ToUpper() }).ToList();
                }
            }

            return customer.Count > 0 ? customer.First() : null;
        }

        /// <summary>
        /// Get data objects from BD
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public static List<IBaseObject> GetDataObjects<T>(GetDataFilter filter)
        {
            if (typeof(T).Equals(typeof(TaskEvent)))
                return GetDataList((GetDataFilterTaskEvent)filter);
            else if (typeof(T).Equals(typeof(AppointmentEvent)))
                return GetDataList((GetDataFilterAppointmentEvent)filter);
            else if (typeof(T).Equals(typeof(Contact)))
                return GetDataList((GetDataFilterContact)filter);

            return null;
        }

        /// <summary>
        /// Get List<TaskEvent> from DB
        /// </summary>
        /// <param name="dataObject"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static List<IBaseObject> GetDataList(GetDataFilterTaskEvent filter)
        {
            var customer = new List<TaskEvent>();
            using (var conn = OpenConnection(ConnectionString))
            {
                if (filter.AllObjects)
                {
                    customer = conn.Query<TaskEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE event_type = 2").ToList();
                }
                else if (filter.IsDayPlan)
                {
                    customer = conn.Query<TaskEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE event_type = 2 AND date_regdate = @DayDate ", new { DayDate = filter.DayDate }).ToList();
                }
                else if (filter.IsWeekPlan)
                {
                    customer = conn.Query<TaskEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE event_type = 2 AND date_regdate >= @BeginDate AND date_regdate <= @EndDate ", new { BeginDate = filter.BeginDate, EndDate = filter.EndDate }).ToList();
                }
                else if (filter.IsMonthPlan)
                {
                    customer = conn.Query<TaskEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE event_type = 2 AND month = @MonthFilter ", new { MonthFilter = filter.Month }).ToList();
                }
            }

            foreach (var item in customer)
                item.Time = item.RegDate - item.Date;

            return customer.Cast<IBaseObject>().ToList();
        }

        /// <summary>
        /// Get List<AppointmentEvent> from DB
        /// </summary>
        /// <param name="dataObject"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static List<IBaseObject> GetDataList(GetDataFilterAppointmentEvent filter)
        {
            var customer = new List<AppointmentEvent>();
            using (var conn = OpenConnection(ConnectionString))
            {
                if (filter.AllObjects)
                {
                    customer = conn.Query<AppointmentEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, contact_id as ContactId, location as Location, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE event_type = 1 ").ToList();
                }
                else if (filter.IsDayPlan)
                {
                    customer = conn.Query<AppointmentEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, contact_id as ContactId, location as Location, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE event_type = 1 AND date_regdate = @DayDate ", new { DayDate = filter.DayDate }).ToList();
                }
                else if (filter.IsWeekPlan)
                {
                    customer = conn.Query<AppointmentEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, contact_id as ContactId, location as Location, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE event_type = 1 AND date_regdate >= @BeginDate AND date_regdate <= @EndDate ", new { BeginDate = filter.BeginDate, EndDate = filter.EndDate }).ToList();
                }
                else if (filter.IsMonthPlan)
                {
                    customer = conn.Query<AppointmentEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, contact_id as ContactId, location as Location, rec_date as RecDate, del_rec as DelRec FROM t_events WHERE event_type = 1 AND month = @MonthFilter ", new { MonthFilter = filter.Month }).ToList();
                }
            }

            foreach (var item in customer)
                item.Time = item.RegDate - item.Date;

            return customer.Cast<IBaseObject>().ToList();
        }

        /// <summary>
        /// Get List<Contact> from DB
        /// </summary>
        /// <param name="dataObject"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static List<IBaseObject> GetDataList(GetDataFilterContact filter)
        {
            var customer = new List<Contact>();
            using (var conn = OpenConnection(ConnectionString))
            {
                if (filter.AllObjects)
                {
                    customer = conn.Query<Contact>("SELECT id as Id, name as Name, phone as Phone, email as EMail, rec_date as RecDate, del_rec as DelRec FROM t_contacts ").ToList();
                }
            }

            return customer.Cast<IBaseObject>().ToList();
        }
    }
}
