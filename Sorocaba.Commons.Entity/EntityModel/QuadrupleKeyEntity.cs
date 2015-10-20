using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public abstract class QuadrupleKeyEntity<TKeyA, TKeyB, TKeyC, TKeyD> : AbstractEntity {

        public abstract TKeyA KeyA { get; }
        public abstract TKeyB KeyB { get; }
        public abstract TKeyC KeyC { get; }
        public abstract TKeyD KeyD { get; }

        [NotMapped]
        public override object[] Key {
            get {
                if (IsEmptyKey()) {
                    return null;
                } else {
                    return new object[] { KeyA, KeyB, KeyC, KeyD };
                }
            }
        }

        protected override bool IsEmptyKey() {
            return IsEmptyKeyValue<TKeyA>(KeyA) || IsEmptyKeyValue<TKeyB>(KeyB) || IsEmptyKeyValue<TKeyC>(KeyC) || IsEmptyKeyValue<TKeyD>(KeyD);
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

            QuadrupleKeyEntity<TKeyA, TKeyB, TKeyC, TKeyD> castedOther = (QuadrupleKeyEntity<TKeyA, TKeyB, TKeyC, TKeyD>) other;
            return !castedOther.IsEmptyKey()
                && KeyA.Equals(castedOther.KeyA)
                && KeyB.Equals(castedOther.KeyB)
                && KeyC.Equals(castedOther.KeyC)
                && KeyD.Equals(castedOther.KeyD);
        }

        public override int GetHashCode() {
            return IsEmptyKey() ? 0 : KeyA.GetHashCode() ^ KeyB.GetHashCode() ^ KeyC.GetHashCode() ^ KeyD.GetHashCode();
        }
    }
}
