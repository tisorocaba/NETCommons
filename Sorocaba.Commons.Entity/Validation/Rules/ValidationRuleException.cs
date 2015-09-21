using Sorocaba.Commons.Foundation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Validation.Rules {
    public class ValidationRuleException : Exception, IBusinessException, IDataException {

        private ValidationRuleViolations violations;

        public ValidationRuleException(ValidationRuleViolations violations) : base(Strings.ThereAreValidationErrors) {
            this.violations = violations;
        }

        public object GetExceptionData() {
            return violations;
        }
    }
}
