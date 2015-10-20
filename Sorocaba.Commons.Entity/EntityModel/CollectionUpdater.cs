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

        public void Execute(bool insertWithId = false) {

            var itemsWithId = dataCollection.Where(i => i.Key != null).ToList();
            var itemsWithoutId = dataCollection.Where(i => i.Key == null).ToList();

            var insertedItems = itemsWithId.Except(baseCollection).ToList();
            var updatedItems = baseCollection.Intersect(itemsWithId).ToList();
            var deletedItems = baseCollection.Except(itemsWithId).ToList();

            if (deleteCallback != null) {
                deletedItems.ForEach(deleteCallback);
            }
            if (insertCallback != null) {
                if (insertWithId) {
                    insertedItems.ForEach(insertCallback);
                }
                itemsWithoutId.ForEach(insertCallback);
            }
            if (updateCallback != null) {
                updatedItems.ForEach(i =>
                    updateCallback(i, dataCollection.Where(j => i.Equals(j)).First())
                );
            }
        }
    }
}
