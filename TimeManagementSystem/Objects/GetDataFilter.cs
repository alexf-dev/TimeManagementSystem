using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
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

    public class GetDataFilterAppointmentEvent : EventDataFilter
    {
    }

    public class GetDataFilterTaskEvent : EventDataFilter
    {
    }

    public class GetDataFilterContact : GetDataFilter
    {

    }

    public class GetDataFilterActionMD : GetDataFilter
    {

    }

    public class GetDataFilterReport : GetDataFilter
    {
        public string OperatorFullName { get; set; }
    }
}
