using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Toolbox.Help.Handlers
{
    public class ScssHandler : RequestHandler
    {
        public override void SendResponse(HttpListenerRequest request, HttpListenerResponse response, Stream stream)
        {
            response.ContentType = "text/scss";

            base.SendResponse(request, response, stream);
        }
    }
}
