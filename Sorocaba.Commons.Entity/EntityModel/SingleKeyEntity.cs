using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public abstract class SingleKeyEntity<T> : AbstractEntity {

        public abstract T Id { get; set; }

        [NotMapped]
        public override object[] Key { get {
            if (IsEmptyKey()) {
                return null;
            } else {
                return new object[] { Id };
            }
        }}

        protected override bool IsEmptyKey() {
            return IsEmptyKeyValue<T>(Id);
        }

        public override bool Equals(object other) {
            return Equals(other as AbstractEntity);
        }

        public override bool Equals(AbstractEntity other) {

            if (other == null) {
                return false;
            }

            if (!ObjectContext.GetObjectType(GetType()).Equals(ObjectContext.GetObjectType(other.GetType()))) {
                return false;
            }

            SingleKeyEntity<T> castedOther = (SingleKeyEntity<T>) other;
            return !castedOther.IsEmptyKey() && Id.Equals(castedOther.Id);
        }

        public override int GetHashCode() {
            return IsEmptyKey() ? 0 : Id.GetHashCode();
        }
    }
}
