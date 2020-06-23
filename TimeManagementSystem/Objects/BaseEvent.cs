using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Base event class
    /// </summary>
    public class BaseEvent : BaseObject
    {
        /// <summary>
        /// Event's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Event's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Year regdate event
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month regdate event
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Registration date event
        /// </summary>
        public DateTime RegDate { get; set; }

        /// <summary>
        /// Date of regdate event
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Time of regdate event
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Event type
        /// </summary>
        public EventType ActionType { get; set; }

        public string EventName { get { return Name; } }

        public DayOfWeek DayOfWeek { get { return Date.DayOfWeek; } }

        public int DayOfMonth { get { return Date.Day; } }

    }
}
