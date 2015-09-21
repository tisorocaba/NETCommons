using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sorocaba.Commons.Foundation.Exceptions;
using Sorocaba.Commons.Http.Json.Binding;

namespace Sorocaba.Commons.Http.Exceptions.Translation {
    public class HttpExceptionTranslator  {

        private IExceptionTranslator[] customTranslators;

        public HttpExceptionTranslator(params IExceptionTranslator[] customTranslators) {
            this.customTranslators = customTranslators;
        }

        public void TranslateException(Exception exception, ref HttpStatusCode responseStatus, ref HttpExceptionData responseData) {

            if (exception is AggregateException) {
                exception = ExceptionUtils.GetInnerException(exception);
            }

            responseData.Message = Strings.HttpExceptionDefaultMessage;
            responseData.SetFromException(exception);

            if (exception is IBusinessException) {
                responseStatus = HttpStatusCode.PreconditionFailed;
                responseData.Message = exception.Message;
            }

            else if (exception is JsonBindingException) {
                responseStatus = HttpStatusCode.BadRequest;
                responseData.Message = Strings.MalformedRequestData;
                responseData.SetFromException(exception.InnerException);
            }

            else {
                string message = null;
                Exception newException = null;

                foreach (var t in customTranslators) {
                    if (t.TranslateException(exception, out message, out newException)) {
                        responseData.Message = message;
                        responseData.SetFromException(newException);
                        break;
                    }
                }
            }

            if (exception is IHttpStatusCodeException) {
                responseStatus = (exception as IHttpStatusCodeException).GetStatusCode();
            }

            if (exception is IDataException) {
                responseData.ErrorData = (exception as IDataException).GetExceptionData();
            }
        }
    }
}
