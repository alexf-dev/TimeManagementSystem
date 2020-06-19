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
            if (typeof(T).Equals(typeof(TaskEvent)))
                return GetData((GetDataFilterTaskEvent)filter);

            return null;
        }

        private static IBaseObject GetData(GetDataFilterTaskEvent filter)
        {
            var customer = new List<TaskEvent>();
            using (var conn = OpenConnection(ConnectionString))
            {
                string query = @"SELECT id as Id, firstname as FirstName, lastname as LastName, middlename as MiddleName, workplace_id as WorkplaceId, department_id as DepartmentId, username as UserName, password as Password, status as Status, access_level_value as AccessLevelValue, rec_date as RecDate, del_rec as DelRec  FROM t_events WHERE true ";
                string queryFilter = "";
                //if (!string.IsNullOrWhiteSpace(filter.FirstName))
                //    queryFilter += " AND firstname = @FirstName ";
                //if (!string.IsNullOrWhiteSpace(filter.LastName))
                //    queryFilter += " AND lastname = @LastName ";
                //if (!string.IsNullOrWhiteSpace(filter.MiddleName))
                //    queryFilter += " AND middlename = @MiddleName ";

                query += queryFilter;

                customer = conn.Query<TaskEvent>(query).ToList();
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

            return null;
        }

        /// <summary>
        /// Get List<User> from DB
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
                    //id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, time_regdate as Time, reg_date as RegDate, event_type as ActionType, rec_date as RecDate, del_rec as DelRec
                    //    @Name, @Description, @Year, @Month, @Date, @Time, @RegDate, @ActionType, @RecDate, @DelRec

                    customer = conn.Query<TaskEvent>("SELECT id as Id, name as Name, description as Description, year as Year, month as Month, date_regdate as Date, reg_date as RegDate, event_type as ActionType, rec_date as RecDate, del_rec as DelRec FROM t_events").ToList();
                }
            }

            foreach (var item in customer)
                item.Time = item.RegDate - item.Date;

            return customer.Cast<IBaseObject>().ToList();
        }
    }
}
