using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Validation {
    public static class Validation {

        public static void NotNull(object value, string message) {
            if (Conditions.IsNull(value)) {
                throw new ValidationException(message);
            }
        }

        public static void NotBlank(string value, string message) {
            if (Conditions.IsBlank(value)) {
                throw new ValidationException(message);
            }
        }

        public static void NotEmpty<T>(IEnumerable<T> value, string message) {
            if (Conditions.IsEmpty(value)) {
                throw new ValidationException(message);
            }
        }

        public static void MinimumCount<T>(IEnumerable<T> value, int count, string message) {
            if (!Conditions.HasMinimumCount(value, count)) {
                throw new ValidationException(message);
            }
        }

        public static void ExactCount<T>(IEnumerable<T> value, int count, string message) {
            if (!Conditions.HasExactCount(value, count)) {
                throw new ValidationException(message);
            }
        }

        public static void MaximumCount<T>(IEnumerable<T> value, int count, string message) {
            if (!Conditions.HasMaximumCount(value, count)) {
                throw new ValidationException(message);
            }
        }

        public static void Range(decimal value, decimal min, decimal max, string message) {
            if (!Conditions.IsInRange(value, min, max)) {
                throw new ValidationException(message);
            }
        }
    }
}
