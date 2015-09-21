using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Foundation.Exceptions {
    public interface IDataException {
        object GetExceptionData();
    }
}
