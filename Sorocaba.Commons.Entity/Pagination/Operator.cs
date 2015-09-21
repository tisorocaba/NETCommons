using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Pagination {
    public class Operator {

        public Operator(string symbol, Func<Expression, Expression, Expression> expression) {
            this.Symbol = symbol;
            this.Expression = expression;
        }

        public string Symbol { get; private set; }

        public Func<Expression, Expression, Expression> Expression { get; private set; }
    }
}
