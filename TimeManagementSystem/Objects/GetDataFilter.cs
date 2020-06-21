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

    public class GetDataFilterUser : GetDataFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }

    public class GetDataFilterAppointmentEvent : GetDataFilter
    {

    }

    public class GetDataFilterTaskEvent : GetDataFilter
    {

    }

    public class GetDataFilterScriptMD : GetDataFilter
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
