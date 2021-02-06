using System;
using System.IO;
using System.Net;
using Toolbox.Help.Handlers;

namespace Toolbox.Help.Example.WinForms
{
    /// <summary>
    /// Custom <see cref="RequestHandler"/> to handle all '.info' requests.
    /// </summary>
    class InfoHandler : HttpHandler 
    {
        public override void SendResponse(HttpListenerRequest request, HttpListenerResponse response, Stream stream)
        {
            SendResponse(request, response, $"<html><head><title>Custom Handler</title></head><body><p>Hello - you requested '{request.Url.LocalPath}'</p><p>Current time is {DateTime.Now}</p><a href='index.html'>Main Page</a></body></html>");
        }
    }
}
