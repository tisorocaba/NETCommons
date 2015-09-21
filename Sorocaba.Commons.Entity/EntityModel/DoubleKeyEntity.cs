using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public abstract class DoubleKeyEntity<TKeyA, TKeyB> : AbstractEntity {

        public abstract TKeyA KeyA { get; }
        public abstract TKeyB KeyB { get; }

        [NotMapped]
        public override object[] Key { get {
            if (IsEmptyKey()) {
                return null;
            } else {
                return new object[] { KeyA, KeyB };
            }
        }}

        protected override bool IsEmptyKey() {
            return IsEmptyKeyValue<TKeyA>(KeyA) || IsEmptyKeyValue<TKeyB>(KeyB);
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

            DoubleKeyEntity<TKeyA, TKeyB> castedOther = (DoubleKeyEntity<TKeyA, TKeyB>) other;
            return !castedOther.IsEmptyKey() && KeyA.Equals(castedOther.KeyA) && KeyB.Equals(castedOther.KeyB);
        }

        public override int GetHashCode() {
            return IsEmptyKey() ? 0 : KeyA.GetHashCode() ^ KeyB.GetHashCode();
        }
    }
}
