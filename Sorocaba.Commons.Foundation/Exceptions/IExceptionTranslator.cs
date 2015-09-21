using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Foundation.Exceptions {
    public interface IExceptionTranslator {
        bool TranslateException(Exception exception, out string message, out Exception newException);
    }
}
