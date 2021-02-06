using System.IO;
using System.Net;
using System.Text;

namespace Toolbox.Help.Handlers
{
    public class HttpHandler : RequestHandler
    {
        public override void SendResponse(HttpListenerRequest request, HttpListenerResponse response, Stream stream)
        {
            response.ContentType = "text/html";

            base.SendResponse(request, response, stream);
        }

        protected void SendResponse(HttpListenerRequest request, HttpListenerResponse response, string html)
        {
            response.ContentEncoding = new UTF8Encoding(false);

            using (var stream = new MemoryStream(response.ContentEncoding.GetBytes(html)))
            {
                base.SendResponse(request, response, stream);
            }
        }
    }
}
