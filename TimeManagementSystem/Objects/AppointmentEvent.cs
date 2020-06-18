using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Appointment event class
    /// </summary>
    public class AppointmentEvent : BaseEvent, ISaveObject
    {
        /// <summary>
        /// Appointment event's location
        /// </summary>
        public string Location { get; set; }

        private Contact _contact;

        /// <summary>
        /// Event's contact
        /// </summary>
        public Contact Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                ContactId = _contact != null ? _contact.Id : Guid.Empty;
            }
        }

        public Guid ContactId { get; set; }

        public bool DelRec { get; set; }

        public DateTime RecDate { get; set; }
    }
}
