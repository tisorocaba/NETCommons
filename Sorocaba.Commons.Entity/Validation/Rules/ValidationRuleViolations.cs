using Sorocaba.Commons.Entity.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Validation.Rules {
    public class ValidationRuleViolations : List<ValidationRuleViolation> {

        public void Add(IEnumerable<DbEntityValidationResult> validationResults) {
            foreach (var result in validationResults) {
                Type entity = ObjectContext.GetObjectType(result.Entry.Entity.GetType());
                foreach (var error in result.ValidationErrors) {
                    PropertyInfo property = entity.GetProperty(error.PropertyName);
                    if (property.GetCustomAttribute(typeof(NotValidatedAttribute)) == null) {
                        Add(new ValidationRuleViolation() {
                            Message = error.ErrorMessage,
                            Entity = TypeUtils.LowercaseFirst(entity.Name),
                            Property = TypeUtils.LowercaseFirst(property.Name)
                        });
                    }
                }
            }
        }

        public void Add<TEntity>(string message, params object[] messageArgs) {
            Add(new ValidationRuleViolation() {
                Message = String.Format(message, messageArgs),
                Entity = GetEntityName<TEntity>()
            });
        }

        public void Add<TEntity>(int index, string message, params object[] messageArgs) {
            Add(new ValidationRuleViolation() {
                Message = String.Format(message, messageArgs),
                Entity = GetEntityName<TEntity>(),
                Index = index
            });
        }

        public void Add<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, string message, params object[] messageArgs) {
            Add(new ValidationRuleViolation() {
                Message = String.Format(message, messageArgs),
                Entity = GetEntityName<TEntity>(),
                Property = GetPropertyName(propertyExpression)
            });
        }

        public void Add<TEntity, TProperty>(int index, Expression<Func<TEntity, TProperty>> propertyExpression, string message, params object[] messageArgs) {
            Add(new ValidationRuleViolation() {
                Message = String.Format(message, messageArgs),
                Entity = GetEntityName<TEntity>(),
                Property = GetPropertyName(propertyExpression),
                Index = index,
            });
        }

        public bool HasViolations() {
            return Count > 0;
        }

        public void ThrowException() {
            throw new ValidationRuleException(this);
        }

        public void ThrowExceptionIfHasViolations() {
            if (HasViolations()) {
                ThrowException();
            }
        }

        private string GetEntityName<TEntity>() {
            return TypeUtils.LowercaseFirst(typeof(TEntity).Name);
        }

        private string GetPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property) {
            return TypeUtils.LowercaseFirst(TypeUtils.GetPropertyPathFromExpression(property));
        }
    }
}
