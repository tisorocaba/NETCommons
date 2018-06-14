using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sorocaba.Commons.Http.Json.Binding;
using Sorocaba.Commons.Http.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Sorocaba.Commons.Http.Json {
    public class JsonConfiguration {

        public static void ApplyConfiguration(HttpConfiguration configuration) {

            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            var settings = configuration.Formatters.JsonFormatter.SerializerSettings;

            settings.Converters.Add(new JsonTrimmingConverter());

            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Include;
            settings.Formatting = Formatting.None;
            settings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
            settings.Error = (sender, a) => {
                if (!HttpContext.Current.Items.Contains("JSON_BINDING_ERROR")) {
                    HttpContext.Current.Items.Add("JSON_BINDING_ERROR", a.ErrorContext.Error);
                }
            };

            configuration.Filters.Add(new JsonBindingErrorActionFilter());
        }
    }
}
