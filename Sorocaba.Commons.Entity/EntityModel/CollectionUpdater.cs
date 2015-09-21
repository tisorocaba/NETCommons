using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {

    public class CollectionUpdater {
        public static CollectionUpdater<TEntity> Init<TEntity>(IEnumerable<TEntity> baseCollection, IEnumerable<TEntity> dataCollection) where TEntity : IEntity {
            return new CollectionUpdater<TEntity>(baseCollection, dataCollection);
        }
    }

    public class CollectionUpdater<TEntity> where TEntity : IEntity {

        private Action<TEntity> insertCallback;
        private Action<TEntity, TEntity> updateCallback;
        private Action<TEntity> deleteCallback;

        private IEnumerable<TEntity> baseCollection;
        private IEnumerable<TEntity> dataCollection;

        public CollectionUpdater(IEnumerable<TEntity> baseCollection, IEnumerable<TEntity> dataCollection) {
            this.baseCollection = baseCollection;
            this.dataCollection = dataCollection;
        }

        public CollectionUpdater<TEntity> WithInserted(Action<TEntity> callback) {
            this.insertCallback = callback;
            return this;
        }

        public CollectionUpdater<TEntity> WithUpdated(Action<TEntity, TEntity> callback) {
            this.updateCallback = callback;
            return this;
        }

        public CollectionUpdater<TEntity> WithDeleted(Action<TEntity> callback) {
            this.deleteCallback = callback;
            return this;
        }

        public void Execute() {
            var inserted = dataCollection.Where(i => i.Key == null).ToList();
            var notInserted = dataCollection.Where(i => i.Key != null).ToList();
            var deleted = baseCollection.Except(notInserted).ToList();
            var updated = baseCollection.Except(deleted).ToList();

            deleted.ForEach(deleteCallback);
            inserted.ForEach(insertCallback);
            updated.ForEach(i =>
                updateCallback(i, dataCollection.Where(j => i.Equals(j)).First())
            );
        }
    }
}
