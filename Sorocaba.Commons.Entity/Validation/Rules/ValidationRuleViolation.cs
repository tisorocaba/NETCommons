using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Validation.Rules {
    public class ValidationRuleViolation {
        public string Entity { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
        public int? Index { get; set; }
    }
}
