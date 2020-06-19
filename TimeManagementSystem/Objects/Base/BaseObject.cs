using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Abstract base class
    /// </summary>
    public abstract class BaseObject : IBaseObject
    {
        public int Id { get; set; }
    }
}
