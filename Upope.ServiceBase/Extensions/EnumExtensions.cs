using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Upope.ServiceBase.ServiceBase.Extensions;

namespace Upope.ServiceBase.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum value)
        {
            return GetDisplayAttribute(value).GetDescription();
        }

        public static string DescriptionOrName(this Enum value)
        {
            var displayAttribute = GetDisplayAttribute(value);

            return string.IsNullOrEmpty(displayAttribute.GetDescription()) ?
                displayAttribute.GetName() : displayAttribute.GetDescription();
        }

        public static string DisplayName(this Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return GetDisplayAttribute(value).GetName();
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();

            return type.GetMember(Enum.GetName(type, value))[0].GetCustomAttribute<TAttribute>(false);
        }

        public static object GetAttributeValue<TAttributeType>(
            this Enum value, Expression<Func<TAttributeType, object>> valueSelector)
            where TAttributeType : Attribute
        {
            return ((TAttributeType)value.GetType().GetMember(Enum.GetName(value.GetType(), value))[0]
                .GetCustomAttribute(typeof(TAttributeType)))
                .GetPropertyValue(((MemberExpression)valueSelector.Body).Member.Name);
        }

        private static DisplayAttribute GetDisplayAttribute(Enum value)
        {
            var type = value.GetType();

            return (DisplayAttribute)type.GetMember(Enum.GetName(type, value))[0]
                .GetCustomAttribute(typeof(DisplayAttribute), false);
        }

        public static string GroupName(this Enum value)
        {
            return GetDisplayAttribute(value).GetGroupName();
        }

        public static bool IsDefined(this Enum obj)
        {
            return Enum.IsDefined(obj.GetType(), Convert.ToInt32(obj));
        }

        public static string ShortName(this Enum value)
        {
            return GetDisplayAttribute(value).GetShortName();
        }

        public static IList<int> ToIntValues<TEnum>(this IEnumerable<TEnum> collection) where TEnum : struct
        {
            return collection.Select(x => Convert.ToInt32(x)).ToList();
        }

        public static TEnum? TryConvertToEnum<TEnum>(this string value) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            TEnum result;
            if (Enum.TryParse(value, true, out result))
            {
                return result;
            }

            return null;
        }

        public static TEnum? TryConvertToEnum<TEnum>(this int? value) where TEnum : struct
        {
            if (!value.HasValue)
            {
                return null;
            }

            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum?)Enum.Parse(typeof(TEnum), value.ToString());
            }

            return null;
        }
    }
}
