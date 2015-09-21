using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Foundation.Extensions {
    public static class StringExtensions {
        public static IEnumerable<string> Split(this string value, string delimiter) {
            return value
                .Split(new String[] { delimiter }, StringSplitOptions.None)
                .Select(s => s.Trim());
        }
    }
}
