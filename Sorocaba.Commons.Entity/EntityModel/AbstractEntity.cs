using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public abstract class AbstractEntity : IEntity, IEquatable<AbstractEntity> {

        [NotMapped]
        public abstract object[] Key { get; }

        public bool ShouldSerializeKey() {
            return false;
        }

        protected abstract bool IsEmptyKey();

        public abstract bool Equals(AbstractEntity other);

        public abstract override int GetHashCode();

        protected bool IsEmptyKeyValue<T>(T keyValue) {
            Type type = typeof(T);
            if (type.IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null) {
                return keyValue.Equals(default(T));
            } else {
                return keyValue == null;
            }
        }
    }
}
