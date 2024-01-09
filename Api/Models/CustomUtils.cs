using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Api.Models
{
    internal static class CustomUtils
    {
        public static bool CanAssignNull(Type type)
        {
            if (type.GetTypeInfo().IsValueType)
            {
                return IsNullable(type);
            }

            return true;
        }

        public static bool IsNullable(Type type)
        {
            TypeInfo typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType)
            {
                return typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
            }

            return false;
        }

        public static Type MakeNullable(Type type)
        {
            return typeof(Nullable<>).MakeGenericType(type);
        }

        public static object GetDefaultValue(Type type)
        {
            if (type.GetTypeInfo().IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        public static object ConvertClientValue(object value, Type type)
        {
            value = UnwrapNewtonsoftValue(value);
            if (value == null || type == null)
            {
                return value;
            }

            type = StripNullableType(type);
            if (IsIntegralType(type) && value is string)
            {
                value = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            }

            if (type == typeof(DateTime) && value is string)
            {
                return DateTime.Parse((string)value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (converter != null && converter.CanConvertFrom(value.GetType()))
            {
                return converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
            }

            if (type.GetTypeInfo().IsEnum)
            {
                return Enum.Parse(type, Convert.ToString(value), ignoreCase: true);
            }

            return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

        public static Type StripNullableType(Type type)
        {
            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return underlyingType;
            }

            return type;
        }

        public static string GetSortMethod(bool first, bool desc)
        {
            if (!first)
            {
                if (!desc)
                {
                    return "ThenBy";
                }

                return "ThenByDescending";
            }

            if (!desc)
            {
                return "OrderBy";
            }

            return "OrderByDescending";
        }

        public static IEnumerable<CustomSortingInfo> AddRequiredSort(IEnumerable<CustomSortingInfo> sort, IEnumerable<string> requiredSelectors)
        {
            sort = sort ?? new CustomSortingInfo[0];
            requiredSelectors = requiredSelectors.Except<string>(sort.Select((CustomSortingInfo i) => i.Selector), StringComparer.OrdinalIgnoreCase);
            bool? desc = sort.LastOrDefault()?.Desc;
            return sort.Concat(requiredSelectors.Select((string i) => new CustomSortingInfo
            {
                Selector = i,
                Desc = (desc.HasValue && desc.Value)
            }));
        }

        public static string[] GetPrimaryKey(Type type)
        {
            return (from m in new MemberInfo[0].Concat(type.GetRuntimeProperties()).Concat(type.GetRuntimeFields())
                    where m.GetCustomAttributes(inherit: true).Any((object i) => i.GetType().Name == "KeyAttribute")
                    select m.Name into i
                    orderby i
                    select i).ToArray();
        }

        public static int DynamicCompare(object selectorResult, object clientValue)
        {
            clientValue = ((selectorResult == null) ? UnwrapNewtonsoftValue(clientValue) : ConvertClientValue(clientValue, selectorResult.GetType()));
            return Comparer<object>.Default.Compare(selectorResult, clientValue);
        }

        public static object UnwrapNewtonsoftValue(object value)
        {
            JValue jValue;
            if ((jValue = value as JValue) != null)
            {
                return jValue.Value;
            }

            return value;
        }

        private static bool IsIntegralType(Type type)
        {
            if (!(type == typeof(int)) && !(type == typeof(long)) && !(type == typeof(byte)) && !(type == typeof(sbyte)) && !(type == typeof(uint)))
            {
                return type == typeof(ulong);
            }

            return true;
        }
    }
}
