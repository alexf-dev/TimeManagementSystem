using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TimeManagementSystem.Objects;

namespace TimeManagementSystem.Data
{
    public static class ApplicationData
    {
        private static object locker = new object();
        private static Dictionary<MemberInfo, Attribute[]> attributies_member_dict = new Dictionary<MemberInfo, Attribute[]>();
        private static Dictionary<Type, Attribute[]> attributies_type_dict = new Dictionary<Type, Attribute[]>();

        /// <summary>
        /// Возвращает атрибут связанный с указанным свойством, либо NULL если данный атрибут не связан с свойством
        /// </summary>
        /// <param name="property_info">Свойство объекта</param>
        /// <param name="attribute_type">Тип атрибута</param>
        /// <returns>Атрибут, либо NULL</returns>
        public static T GetCustomAttribute<T>(MemberInfo property_info) where T : Attribute
        {
            lock (locker)
            {
                if (!attributies_member_dict.ContainsKey(property_info))
                    attributies_member_dict.Add(property_info, Attribute.GetCustomAttributes(property_info));
            }
            return (T)attributies_member_dict[property_info].FirstOrDefault(a => object.Equals(a.GetType(), typeof(T)));
        }
        /// <summary>
        /// Возвращает атрибут связанный с указанным типом, либо NULL если данный атрибут не связан с типом
        /// </summary>
        /// <param name="object_type">Тип</param>
        /// <param name="attribute_type">Тип атрибута</param>
        /// <returns>Атрибут, либо NULL</returns>
        public static T GetCustomAttribute<T>(Type object_type) where T : Attribute
        {
            if (object_type != null)
            {
                lock (locker)
                {
                    if (!attributies_type_dict.ContainsKey(object_type))
                        attributies_type_dict.Add(object_type, Attribute.GetCustomAttributes(object_type));
                }
                return (T)attributies_type_dict[object_type].FirstOrDefault(a => object.Equals(a.GetType(), typeof(T)));
            }
            return null;
        }

        public static string GetEnumText(Enum enumitem)
        {
            string rez = string.Empty;
            if (string.IsNullOrWhiteSpace(rez))
                rez = Attributies.GetLocalizedName(enumitem);
            if (string.IsNullOrWhiteSpace(rez))
                rez = enumitem.ToString();
            return rez;
        }
    }
}
