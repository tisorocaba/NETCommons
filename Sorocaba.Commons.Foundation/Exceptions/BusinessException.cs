using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Foundation.Exceptions {
    public class BusinessException : Exception, IBusinessException, IDataException {

        private object data;

        public BusinessException(string message, object data = null) : base(message) {
            this.data = data;
        }

        public BusinessException(string message, Exception innerException, object data = null) : base(message, innerException) {
            this.data = data;
        }

        public object GetExceptionData() {
            return data;
        }
    }
}
