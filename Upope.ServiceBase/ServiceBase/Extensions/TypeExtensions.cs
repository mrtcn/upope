using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Upope.ServiceBase.ServiceBase.Extensions
{
    public static class TypeExtensions
    {
        public static bool CheckNullOrEmpty<T>(T value)
        {
            if (typeof(T) == typeof(string))
            {
                return string.IsNullOrEmpty(value as string);
            }

            return value == null || value.Equals(default(T));
        }

        public static bool CheckNullOrEmpty(Type type, object value)
        {
            if (type == typeof(string))
            {
                return string.IsNullOrEmpty(value as string);
            }

            if (type.IsArray)
            {
                var array = value as IEnumerable<object>;

                if (array != null)
                {
                    return !array.Any();
                }
            }

            return value == null || value.Equals(GetDefaultValue(type));
        }

        public static T DeepCopy<T>(this T other)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(ms, other);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static object GetDefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static object GetPropertyValue(this object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null)
                {
                    return null;
                }

                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null)
                {
                    return null;
                }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropertyValue<T>(this object obj, string name)
        {
            var retval = GetPropertyValue(obj, name);
            if (retval == null)
            {
                return default(T);
            }

            return (T)retval;
        }

        public static T IfNotNull<T, TSource>(this TSource item, Func<TSource, T> lambda) where TSource : class
        {
            return item == null ? default(T) : lambda(item);
        }
        
        public static void SetPropertyValue(this object obj, string propName, object value)
        {
            obj.GetType().GetProperty(propName).SetValue(obj, value, null);
        }

        public static bool HasPropertyType(this Type type, Type targetType)
        {
            return type.GetProperties().Any(x => x.PropertyType == targetType);
        }
    }
}
