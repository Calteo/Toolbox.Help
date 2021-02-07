using System;
using System.IO;
using System.Net;
using System.Text;
using Toolbox.Help.Handlers;

namespace Toolbox.Help.Test
{
    class CustomHandler : RequestHandler
    {
        public CustomHandler()
        {
            Id = Guid.NewGuid().ToString("D");
        }

        public string Id { get; }

        public const string ContentType = "text/plain";

        public override void SendResponse(HttpListenerRequest request, HttpListenerResponse response, Stream stream)
        {
            response.ContentType = ContentType;

            var idStream = new MemoryStream(Encoding.Default.GetBytes(Id));

            base.SendResponse(request, response, idStream);
        }
    }
}
