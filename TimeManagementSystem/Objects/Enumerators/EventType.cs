using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
    public enum EventType
    {
        [LocalizedName("Appointment")]
        Appointment = 1,

        [LocalizedName("Task")]
        Task = 2
    }
}
