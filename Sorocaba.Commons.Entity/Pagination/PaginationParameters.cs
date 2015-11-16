using Sorocaba.Commons.Entity.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Pagination {
    public class PaginationParameters {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public bool ShowAllItems { get; set; }
        public ICollection<Sorter> Sorters { get; set; }
        public ICollection<Filter> Filters { get; set; }
        public ICollection<Filter> FullFilters { get; set; }
    }

    public static class FilterCollectionExtensions {

        public static Filter GetFilter(this ICollection<Filter> filters, string field) {
            return filters
                .Where(f => f.FieldName == field)
                .FirstOrDefault();
        }

        public static void AddFilter(this ICollection<Filter> filters, string field, string @operator, object value) {
            var oldFilter = GetFilter(filters, field);
            if (oldFilter != null) {
                filters.Remove(oldFilter);
            }
            filters.Add(Filter.CreateFilter(String.Format("{0}{1}{2}", field, @operator, value)));
        }

        public static T GetValue<T>(this ICollection<Filter> filters, string field, T defaultValue = default(T)) {
            Filter filter = GetFilter(filters, field);
            if (filter != null) {
                return Convert<T>(filter.FieldValue);
            } else {
                return defaultValue;
            }
        }

        public static T RemoveValue<T>(this ICollection<Filter> filters, string field, T defaultValue = default(T)) {
            Filter filter = GetFilter(filters, field);
            if (filter != null) {
                filters.Remove(filter);
                return Convert<T>(filter.FieldValue);
            } else {
                return defaultValue;
            }
        }

        private static T Convert<T>(object value) {
            T convertedValue;
            if (!TypeUtils.TryConvert(value, out convertedValue)) {
                throw new Exception(Strings.CouldNotConvertToType);
            }
            return convertedValue;
        }
    }
}
