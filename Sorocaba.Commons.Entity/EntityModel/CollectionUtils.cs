using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public static class CollectionUtils {

        public static bool HasDuplicates<TKeyA>(IEnumerable<SingleKeyEntity<TKeyA>> entities) {
            return entities.GroupBy(e => new { e.Key })
                .Count() != entities.Count();
        }

        public static bool HasDuplicates<TKeyA, TKeyB>(IEnumerable<DoubleKeyEntity<TKeyA, TKeyB>> entities) {
            return entities.GroupBy(e => new { e.KeyA, e.KeyB })
                .Count() != entities.Count();
        }

        public static bool HasDuplicates<TKeyA, TKeyB, TKeyC>(IEnumerable<TripleKeyEntity<TKeyA, TKeyB, TKeyC>> entities) {
            return entities.GroupBy(e => new { e.KeyA, e.KeyB, e.KeyC })
                .Count() != entities.Count();
        }

        public static bool HasDuplicates<TKeyA, TKeyB, TKeyC, TKeyD>(IEnumerable<QuadrupleKeyEntity<TKeyA, TKeyB, TKeyC, TKeyD>> entities) {
            return entities.GroupBy(e => new { e.KeyA, e.KeyB, e.KeyC, e.KeyD })
                .Count() != entities.Count();
        }
    }
}
