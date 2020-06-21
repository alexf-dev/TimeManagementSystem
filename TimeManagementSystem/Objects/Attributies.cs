using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Objects
{
    /// <summary>
    /// Локализованный атрибут наименования
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Field)]
    public class LocalizedNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public LocalizedNameAttribute(string name)
        {
            Name = name;
        }
    }

    public sealed class Attributies<T> where T : Attribute
    {
        private Attributies()
        {

        }

        public static T Get(MemberInfo member_info)
        {
            return Data.ApplicationData.GetCustomAttribute<T>(member_info);
        }

        public static T Get(Type type)
        {
            return Data.ApplicationData.GetCustomAttribute<T>(type);
        }
    }

    public sealed class Attributies
    {
        public static string GetLocalizedName(object item)
        {
            LocalizedNameAttribute localized_name_attribute = null;
            if (item is Type)
                localized_name_attribute = Attributies<LocalizedNameAttribute>.Get((Type)item);
            if (item is PropertyInfo)
                localized_name_attribute = Attributies<LocalizedNameAttribute>.Get((PropertyInfo)item);
            if (item is Enum)
            {
                FieldInfo field_info = item.GetType().GetField(item.ToString());
                if (field_info != null)
                    localized_name_attribute = Attributies<LocalizedNameAttribute>.Get(field_info);
            }
            return localized_name_attribute != null && localized_name_attribute.Name != null ? localized_name_attribute.Name : "";
        }
    }
}
