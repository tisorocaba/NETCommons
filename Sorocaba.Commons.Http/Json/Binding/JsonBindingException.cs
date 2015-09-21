using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Http.Json.Binding {
    public class JsonBindingException : Exception {
        public JsonBindingException(Exception exception) : base(exception.Message, exception) {
        }
    }
}
