using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Validation {
    public static class Validation {

        public static void NotNull(object value, string message) {
            if (Conditions.IsNull(value)) {
                Exception(message);
            }
        }

        public static void NotBlank(string value, string message) {
            if (Conditions.IsBlank(value)) {
                Exception(message);
            }
        }

        public static void NotEmpty<T>(IEnumerable<T> value, string message) {
            if (Conditions.IsEmpty(value)) {
                Exception(message);
            }
        }

        public static void MinimumCount<T>(IEnumerable<T> value, int count, string message) {
            if (!Conditions.HasMinimumCount(value, count)) {
                Exception(message);
            }
        }

        public static void ExactCount<T>(IEnumerable<T> value, int count, string message) {
            if (!Conditions.HasExactCount(value, count)) {
                Exception(message);
            }
        }

        public static void MaximumCount<T>(IEnumerable<T> value, int count, string message) {
            if (!Conditions.HasMaximumCount(value, count)) {
                Exception(message);
            }
        }

        public static void Range(decimal value, decimal min, decimal max, string message) {
            if (!Conditions.IsInRange(value, min, max)) {
                Exception(message);
            }
        }

        public static void Custom(bool value, string message) {
            if (!value) {
                Exception(message);
            }
        }

        public static void Custom(Func<bool> boolFunc, string message) {
            if (!boolFunc()) {
                Exception(message);
            }
        }

        public static void Exception(string message) {
            throw new ValidationException(message);
        }
    }
}
