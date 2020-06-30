using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Class for creating a filter. Sets parameters when retrieving objects from the database.
    /// </summary>
    public class GetDataFilter
    {
        public Guid? Id { get; set; }
        public List<Guid> Ids { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid> ParentIds { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllObjects { get; set; }

        public GetDataFilter()
        {
            Id = null;
            Ids = null;
            ParentId = null;
            ParentIds = null;
            Description = null;
            AllObjects = false;
        }
    }

    /// <summary>
    /// Class for creating an event filter.
    /// </summary>
    public class EventDataFilter : GetDataFilter
    {
        public bool IsDayPlan { get; set; }
        public bool IsWeekPlan { get; set; }
        public bool IsMonthPlan { get; set; }
        public DateTime DayDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Month { get; set; }
    }

    /// <summary>
    /// Class for identifying the filter by AppointmentEvent.
    /// </summary>
    public class GetDataFilterAppointmentEvent : EventDataFilter
    {
    }

    /// <summary>
    /// Class for identifying the filter by TaskEvent.
    /// </summary>
    public class GetDataFilterTaskEvent : EventDataFilter
    {
    }

    /// <summary>
    /// Classes for identifying the filter by Contacts.
    /// </summary>
    public class GetDataFilterContact : GetDataFilter
    {

    }
}
