using Sorocaba.Commons.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public static class DbSetExtensions {

        public static TEntity TryFind<TEntity>(this DbSet<TEntity> dbSet, params object[] keyValues) where TEntity : class, IEntity {
            TEntity entity = dbSet.Find(keyValues);
            if (entity == null) {
                throw new EntityNotFoundException(
                    Strings.EntityNotFound(typeof(TEntity).Name)
                );
            }
            return entity;
        }

        public static TEntity ByExample<TEntity>(this DbSet<TEntity> dbSet, TEntity exampleEntity) where TEntity : class, IEntity {
            if (exampleEntity == null || exampleEntity.Key == null) {
                return null;
            } else {
                return TryFind(dbSet, exampleEntity.Key);
            }
        }

        public static TEntity TryByExample<TEntity>(this DbSet<TEntity> dbSet, TEntity exampleEntity) where TEntity : class, IEntity {
            if (exampleEntity == null || exampleEntity.Key == null) {
                throw new EntityNotFoundException(
                    Strings.EntityIncorrectlySpecified(typeof(TEntity).Name)
                );
            } else {
                return TryFind(dbSet, exampleEntity.Key);
            }
        }
    }
}
