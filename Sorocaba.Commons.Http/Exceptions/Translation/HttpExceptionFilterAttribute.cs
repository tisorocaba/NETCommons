using Sorocaba.Commons.Foundation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Sorocaba.Commons.Http.Exceptions.Translation {
    public class HttpExceptionFilterAttribute : ExceptionFilterAttribute {

        protected HttpExceptionTranslator ExceptionTranslator { get; set; }

        protected bool ShowStackTrace { get; set; }

        public HttpExceptionFilterAttribute(bool showStackTrace = false, params IExceptionTranslator[] customTranslators) {
            ExceptionTranslator = new HttpExceptionTranslator(customTranslators);
            ShowStackTrace = showStackTrace;
        }

        public override void OnException(HttpActionExecutedContext ctx) {

            HttpStatusCode responseStatus = HttpStatusCode.InternalServerError;
            HttpExceptionData responseData = new HttpExceptionData();

            ExceptionTranslator.TranslateException(ctx.Exception, ref responseStatus, ref responseData);

            if (!ShowStackTrace) {
                responseData.ErrorStackTrace = null;
            }

            ctx.Response = ctx.Request.CreateResponse(responseStatus, responseData);
        }
    }
}
