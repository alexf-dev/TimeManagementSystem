using System;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Base class interface
    /// </summary>
    public interface IBaseObject
    {
        /// <summary>
        /// Object identifier
        /// </summary>
        Guid Id { get; set; }
    }
}
