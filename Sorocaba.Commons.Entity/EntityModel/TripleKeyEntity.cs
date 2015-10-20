using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public abstract class TripleKeyEntity<TKeyA, TKeyB, TKeyC> : AbstractEntity {

        public abstract TKeyA KeyA { get; }
        public abstract TKeyB KeyB { get; }
        public abstract TKeyC KeyC { get; }

        [NotMapped]
        public override object[] Key { get {
            if (IsEmptyKey()) {
                return null;
            } else {
                return new object[] { KeyA, KeyB, KeyC };
            }
        }}

        protected override bool IsEmptyKey() {
            return IsEmptyKeyValue<TKeyA>(KeyA) || IsEmptyKeyValue<TKeyB>(KeyB) || IsEmptyKeyValue<TKeyC>(KeyC);
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

            TripleKeyEntity<TKeyA, TKeyB, TKeyC> castedOther = (TripleKeyEntity<TKeyA, TKeyB, TKeyC>) other;
            return !castedOther.IsEmptyKey()
                && KeyA.Equals(castedOther.KeyA)
                && KeyB.Equals(castedOther.KeyB)
                && KeyC.Equals(castedOther.KeyC);
        }

        public override int GetHashCode() {
            return IsEmptyKey() ? 0 : KeyA.GetHashCode() ^ KeyB.GetHashCode() ^ KeyC.GetHashCode();
        }
    }
}
