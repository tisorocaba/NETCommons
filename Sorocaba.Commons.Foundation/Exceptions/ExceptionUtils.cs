using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Foundation.Exceptions {
    public static class ExceptionUtils {
        public static Exception GetInnerException(Exception exception) {
            if (exception.InnerException == null) {
                return exception;
            } else {
                return GetInnerException(exception.InnerException);
            }
        }
    }
}
