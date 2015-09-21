using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Pagination {
    public class PaginatedResult<T> where T : class {

        public PaginatedResult() {
            ItemList = Enumerable.Empty<T>();
        }

        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public long ItemCount { get; set; }
        public long ItemOffset { get; set; }
        public IEnumerable<T> ItemList { get; set; }
    }
}
