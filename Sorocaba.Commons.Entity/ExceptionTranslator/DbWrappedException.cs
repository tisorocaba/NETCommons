using Sorocaba.Commons.Foundation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.ExceptionTranslator {
    public class DbWrappedException : Exception, IBusinessException, IDataException {

        private object data;

        public DbWrappedException(string message, Exception innerException = null, object data = null) : base(message, innerException) {
            this.data = data;
        }

        public object GetExceptionData() {
            return data;
        }
    }
}
