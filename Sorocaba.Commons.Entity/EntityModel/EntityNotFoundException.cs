using Sorocaba.Commons.Foundation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public class EntityNotFoundException : Exception, IBusinessException {
        public EntityNotFoundException(String message) : base(message) {
        }
    }
}
