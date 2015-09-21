using Sorocaba.Commons.Foundation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Http.Exceptions.Translation {
    public class HttpExceptionData {

        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorType { get; set; }
        public object ErrorData { get; set; }

        public IEnumerable<string> ErrorStackTrace { get; set; }

        public void SetFromException(Exception exception) {
            ErrorMessage = exception.Message;
            ErrorType = exception.GetType().FullName;
            ErrorStackTrace = exception.StackTrace.Split("\r\n");
        }
    }
}
