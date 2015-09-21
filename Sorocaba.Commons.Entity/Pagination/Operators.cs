using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Pagination {
    public class Operators {

        public static readonly Operator EQUAL = new Operator(
            "=", Expression.Equal
        );

        public static readonly Operator NOT_EQUAL = new Operator(
            "!=", Expression.NotEqual
        );

        public static readonly Operator LIKE = new Operator(
            "*", (left, right) => { return Expression.Call(left, typeof(string).GetMethod("Contains"), right); }
        );

        public static readonly Operator LIKE_ALTERNATIVE = LIKE;

        public static readonly Operator GREATER = new Operator(
            ">=", Expression.GreaterThan
        );

        public static readonly Operator GREATER_EQUAL = new Operator(
            ">", Expression.GreaterThanOrEqual
        );

        public static readonly Operator LESS = new Operator(
            "<", Expression.LessThan
        );

        public static readonly Operator LESS_EQUAL = new Operator(
            "<=", Expression.LessThanOrEqual
        );

        private static Operator[] operatorList = new Operator[] {
             EQUAL, NOT_EQUAL, LIKE, LIKE_ALTERNATIVE, GREATER, GREATER_EQUAL, LESS, LESS_EQUAL
        };

        public static Operator GetBySymbol(string symbol) {
            return operatorList.Where(o => o.Symbol == symbol).FirstOrDefault();
        }
    }
}
