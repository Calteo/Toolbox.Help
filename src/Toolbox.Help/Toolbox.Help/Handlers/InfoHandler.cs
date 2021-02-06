using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Toolbox.Help.Handlers
{
    class InfoHandler : HttpHandler 
    {
        public override void SendResponse(HttpListenerRequest request, HttpListenerResponse response, Stream stream)
        {
            SendResponse(request, response, "<html><body><b>Hellp</b></body></html>");
        }
    }
}
