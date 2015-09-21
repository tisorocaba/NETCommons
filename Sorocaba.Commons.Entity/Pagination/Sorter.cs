using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Pagination {
    public class Sorter {

        public Sorter(string fieldName, string sortOrder) {
            FieldName = fieldName;
            SortOrder = sortOrder;
        }

        public string FieldName { get; set; }
        public string SortOrder { get; set; }
    }
}
