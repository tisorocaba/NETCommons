using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Http.Json.Converters {
    public class JsonTrimmingConverter : JsonConverter {

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(string);
        }

        public override bool CanRead { get { return true; } }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            string value = ((string) reader.Value).Trim();
            return (value.Length == 0) ? null : value;
        }

        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
