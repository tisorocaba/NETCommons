using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Validation.Rules {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotValidatedAttribute : Attribute {
    }
}
