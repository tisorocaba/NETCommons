using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Pagination {
    public class PaginationException : Exception {
        public PaginationException(string message) : base(message) {
        }
    }
}
