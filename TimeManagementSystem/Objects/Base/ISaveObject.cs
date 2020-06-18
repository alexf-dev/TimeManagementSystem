using System;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Сlass interface save in DB
    /// </summary>
    public interface ISaveObject
    {
        /// <summary>
        /// Delete object identifier
        /// </summary>
        bool DelRec { get; set; }

        /// <summary>
        /// Date of object change in the database
        /// </summary>
        DateTime RecDate { get; set; }
    }
}
