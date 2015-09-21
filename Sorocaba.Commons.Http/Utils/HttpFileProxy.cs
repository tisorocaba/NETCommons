using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Http.Utils {
    public static class HttpFileProxy {

        public static HttpResponseMessage Download(string url, string fileType, string fileName, bool downloadFile = false) {

            HttpWebRequest remoteRequest = (HttpWebRequest) WebRequest.Create(url);
            remoteRequest.Credentials = GetCredentials();
            HttpWebResponse remoteResponse = (HttpWebResponse) remoteRequest.GetResponse();

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new PushStreamContent((outputStream, content, context) => {
                remoteResponse.GetResponseStream().CopyTo(outputStream);
                outputStream.Close();
            });
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(fileType);
            if (downloadFile) {
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName };
            }

            return response;
        }

        private static NetworkCredential GetCredentials() {

            string user = ConfigurationManager.AppSettings["HttpFileProxy.User"]
                ?? ConfigurationManager.AppSettings["HttpFileProxy.DefaultUser"];

            string password = ConfigurationManager.AppSettings["HttpFileProxy.Password"]
                ?? ConfigurationManager.AppSettings["HttpFileProxy.DefaultPassword"];

            string domain = ConfigurationManager.AppSettings["HttpFileProxy.Domain"]
                ?? ConfigurationManager.AppSettings["HttpFileProxy.DefaultDomain"];

            if (String.IsNullOrWhiteSpace(user) && String.IsNullOrWhiteSpace(password) && String.IsNullOrWhiteSpace(domain)) {
                return CredentialCache.DefaultNetworkCredentials;
            } else {
                return new NetworkCredential(user, password, domain);
            }
        }
    }
}
