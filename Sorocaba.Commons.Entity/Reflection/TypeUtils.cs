using Sorocaba.Commons.Foundation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Reflection {
    public static class TypeUtils {

        public static PropertyInfo GetPropertyInfo<T>(string propertyPath) where T : class {
            bool discardedBool;
            return GetPropertyInfo<T>(propertyPath, out discardedBool);
        }

        public static PropertyInfo GetPropertyInfo<T>(string propertyPath, out bool hasCollections) where T : class {

            hasCollections = false;

            string[] names = propertyPath.Split('.');

            string propertyName = UppercaseFirst(names[0]);
            PropertyInfo property = typeof(T).GetProperty(propertyName);

            for (int i = 1; i < names.Length; i++) {
                if (property != null) {
                    Type propertyType = property.PropertyType;
                    if (IsCollectionType(property)) {
                        propertyType = property.PropertyType.GenericTypeArguments[0];
                        hasCollections = true;
                    }
                    propertyName = char.ToUpper(names[i][0]) + names[i].Substring(1);
                    property = propertyType.GetProperty(propertyName);
                } else {
                    break;
                }
            }

            return property;
        }

        public static string GetPropertyPathFromExpression<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression) {

            Type entityType = typeof(TEntity);

            MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null) {
                throw new ArgumentException(Strings.ExpressionMustBePropertyExpression(propertyExpression.ToString()));
            }

            if (!(memberExpression.Member is PropertyInfo)) {
                throw new ArgumentException(Strings.ExpressionMustBePropertyExpression(propertyExpression.ToString()));
            }

            return string.Join(
                ".",
                memberExpression.ToString()
                    .Split('.')
                    .Select(s => LowercaseFirst(s))
            );
        }

        public static MemberExpression GetPropertyExpression<T>(string propertyPath, out ParameterExpression entity) {
            entity = Expression.Parameter(typeof(T));
            Expression property = entity;
            foreach (var name in propertyPath.Split('.')) {
                if (property != null) {
                    property = Expression.Property(property, UppercaseFirst(name));
                } else {
                    break;
                }
            }
            return (property is MemberExpression) ? (MemberExpression) property : null;
        }

        public static bool IsCollectionType(PropertyInfo property) {
            return typeof(IEnumerable<object>).IsAssignableFrom(property.PropertyType);
        }

        public static string UppercaseFirst(string value) {
            if (value.Length < 2) {
                return value.ToUpper();
            } else {
                return value.Substring(0, 1).ToUpper() + value.Substring(1);
            }
        }

        public static string LowercaseFirst(string value) {
            if (value.Length < 2) {
                return value.ToLower();
            } else {
                return value.Substring(0, 1).ToLower() + value.Substring(1);
            }
        }

        public static bool TryConvert<T>(object value, out T convertedValue) {
            return TryConvert<T>(typeof(T), value, out convertedValue);
        }

        public static bool TryConvert<T>(Type type, object value, out T convertedValue) {

            if (value == null) {
                if (!type.IsValueType || Nullable.GetUnderlyingType(type) != null) {
                    convertedValue = (T) value;
                    return true;
                } else {
                    throw new Exception(Strings.TypeNotNullable);
                }
            }

            if (value.GetType() == type) {
                convertedValue = (T) value;
                return true;
            }

            string stringValue = value.ToString();

            try {
                if (type.Equals(typeof(string))) {
                    convertedValue = (T) (Object) stringValue;
                } else if (type.Equals(typeof(bool)) || type.Equals(typeof(bool?))) {
                    convertedValue = (T) (Object) bool.Parse(stringValue);
                } else if (type.Equals(typeof(byte)) || type.Equals(typeof(byte?))) {
                    convertedValue = (T) (Object) byte.Parse(stringValue);
                } else if (type.Equals(typeof(sbyte)) || type.Equals(typeof(sbyte?))) {
                    convertedValue = (T) (Object) sbyte.Parse(stringValue);
                } else if (type.Equals(typeof(short)) || type.Equals(typeof(short?))) {
                    convertedValue = (T) (Object) short.Parse(stringValue);
                } else if (type.Equals(typeof(ushort)) || type.Equals(typeof(ushort?))) {
                    convertedValue = (T) (Object) ushort.Parse(stringValue);
                } else if (type.Equals(typeof(int)) || type.Equals(typeof(int?))) {
                    convertedValue = (T) (Object) int.Parse(stringValue);
                } else if (type.Equals(typeof(uint)) || type.Equals(typeof(uint?))) {
                    convertedValue = (T) (Object) uint.Parse(stringValue);
                } else if (type.Equals(typeof(long)) || type.Equals(typeof(long?))) {
                    convertedValue = (T) (Object) long.Parse(stringValue);
                } else if (type.Equals(typeof(ulong)) || type.Equals(typeof(ulong?))) {
                    convertedValue = (T) (Object) ulong.Parse(stringValue);
                } else if (type.Equals(typeof(float)) || type.Equals(typeof(float?))) {
                    convertedValue = (T) (Object) float.Parse(stringValue);
                } else if (type.Equals(typeof(double)) || type.Equals(typeof(double?))) {
                    convertedValue = (T) (Object) double.Parse(stringValue);
                } else if (type.Equals(typeof(decimal)) || type.Equals(typeof(decimal?))) {
                    convertedValue = (T) (Object) decimal.Parse(stringValue);
                } else if (type.Equals(typeof(char)) || type.Equals(typeof(char?))) {
                    convertedValue = (T) (Object) char.Parse(stringValue);
                } else if (type.Equals(typeof(DateTime)) || type.Equals(typeof(DateTime?))) {
                    convertedValue = (T) (Object) DateTime.Parse(stringValue);
                } else {
                    convertedValue = default(T);
                    return false;
                }
            } catch {
                convertedValue = default(T);
                return false;
            }

            return true;
        }
    }
}
