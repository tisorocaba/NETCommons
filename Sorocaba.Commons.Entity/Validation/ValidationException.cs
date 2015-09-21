using Sorocaba.Commons.Foundation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Validation {
    public class ValidationException : Exception, IBusinessException {
        public ValidationException(string message) : base(message) {
        }
    }
}
