using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Http.Json.Converters {
    public class LocalDateConverter : IsoDateTimeConverter {
        public LocalDateConverter() {
            base.DateTimeFormat = "dd/MM/yyyy";
        }
    }
}
