using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Http.Csv {
    public class HttpCsvPushStreamResponse : HttpResponseMessage {

        public HttpCsvPushStreamResponse(string filename, Action<Action<string[]>> writeAction) : base(HttpStatusCode.OK) {
            Content = new PushStreamContent((outputStream, content, context) => {
                using (StreamWriter writer = new StreamWriter(outputStream, Encoding.UTF8)) {
                    writeAction((values) => CsvWriter.WriteRow(writer, values));
                }
                outputStream.Close();
            });
            Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = filename };
        }
    }
}
