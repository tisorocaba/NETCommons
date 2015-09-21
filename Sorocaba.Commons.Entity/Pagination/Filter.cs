using Sorocaba.Commons.Entity.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Pagination {
    public class Filter {

        private static readonly string FIELD = @"(\w+\.)*(\w)+";
        private static readonly string OPERATOR = @"(=|!=|\*|%|>=|>|<=|<)";
        private static readonly string VALUE = @".+";
        private static readonly string FILTER = String.Format("^{0}{1}{2}$", FIELD, OPERATOR, VALUE);

        public string FieldName { get; private set; }
        public string Operator { get; private set; }
        public object FieldValue { get; private set; }

        public static Filter CreateFilter(string filterExpression) {
            if (!Regex.IsMatch(filterExpression, FILTER)) {
                throw new PaginationException(Strings.InvalidFilterExpression);
            }
            string[] values = Regex.Split(filterExpression, OPERATOR);
            return new Filter(values[0], values[1], values[2]);
        }

        public static Filter CreateSimpleFilter(string field, string value) {
            if (!Regex.IsMatch(field, FIELD)) {
                throw new PaginationException(Strings.InvalidFilterExpression);
            }
            return new Filter(field, Operators.LIKE.Symbol, value);
        }

        private Filter(string field, string @operator, object value) {
            this.FieldName = field;
            this.Operator = @operator;
            this.FieldValue = value;
        }
    }
}
