using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Validation {
    public static class Conditions {

        public static bool IsNull(object value) {
            return value == null;
        }

        public static bool IsBlank(string value) {
            return String.IsNullOrWhiteSpace(value);
        }

        public static bool IsEmpty<T>(IEnumerable<T> value) {
            return value == null || value.Count() < 1;
        }

        public static bool HasMinimumCount<T>(IEnumerable<T> value, int count) {
            return value != null && value.Count() >= count;
        }

        public static bool HasExactCount<T>(IEnumerable<T> value, int count) {
            return value != null && value.Count() == count;
        }

        public static bool HasMaximumCount<T>(IEnumerable<T> value, int count) {
            return value != null && value.Count() <= count;
        }

        public static bool IsInRange(decimal value, decimal min, decimal max) {
            return value >= min && value <= max;
        }
    }
}
