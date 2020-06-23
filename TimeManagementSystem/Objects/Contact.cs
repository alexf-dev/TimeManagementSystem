using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Contact class
    /// </summary>
    public class Contact : BaseObject, ISaveObject
    {
        /// <summary>
        /// Contact's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Contact's phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Contact's email
        /// </summary>
        public string EMail { get; set; }

        public bool DelRec { get ; set ; }

        public DateTime RecDate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
