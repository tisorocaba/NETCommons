using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Sorocaba.Commons.Http.Json.Binding {
    public class JsonBindingErrorActionFilter : ActionFilterAttribute {
        public override void OnActionExecuting(HttpActionContext actionContext) {
            if (HttpContext.Current.Items["JSON_BINDING_ERROR"] != null) {
                throw new JsonBindingException((Exception) HttpContext.Current.Items["JSON_BINDING_ERROR"]);
           }
        }
    }
}
