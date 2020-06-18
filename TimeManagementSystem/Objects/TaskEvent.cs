using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Task event class
    /// </summary>
    public class TaskEvent : BaseEvent, ISaveObject
    {
        public bool DelRec { get ; set ; }

        public DateTime RecDate { get ; set ; }
    }
}
