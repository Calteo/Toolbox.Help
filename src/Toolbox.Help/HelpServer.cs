using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using Toolbox.Help.Handlers;

namespace Toolbox.Help
{
    /// <summary>
    /// Help server to run internal help system
    /// </summary>
    public class HelpServer
    {        
        /// <summary>
        /// Initializes a new instance of <see cref="HelpServer"/>.
        /// </summary>
        /// <param name="assembly">Assembly contain the embedded help files.</param>
        /// <param name="namespacePrefix">Prfix for the namespace to get the help files from.</param>
        public HelpServer(Assembly assembly, string namespacePrefix)
        {
            Assembly = assembly;
            NamespacePrefix = namespacePrefix;

            // insert default handlers
            Handlers["html"] = new HttpHandler();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HelpServer"/>.
        /// </summary>
        /// <param name="type">Type that is located on the same level as the help files in the target assembly.</param>
        /// <param name="namespacePrefix">Name of the help folder.</param>
        /// <remarks>
        /// The type must bei in the default namespace of the assembly, since the namespace prefix is inferred from it.
        /// </remarks>
        public HelpServer(Type type, string folder)
            : this(type.Assembly, type.Namespace + "." + folder)
        {
        }

        private Assembly Assembly { get; }
        private string NamespacePrefix { get; }
        private HttpListener Listener { get; set; }

        #region Enabled
        private bool enabled;
        /// <summary>
        /// Enables this help server
        /// </summary>
        public bool Enabled 
        { 
            get => enabled;
            set
            {
                if (enabled == value) return;
                if (value)
                {
                    Listener = new HttpListener();
                    
                    var retries = 20;

                    while (!Listener.IsListening && retries > 0)
                    {
                        if (!TryGetUnusedPort(out var port))
                            throw new Exception("No port available for help server.");

                        retries--;  // stops infinite looping
                        try
                        {
                            Listener.Prefixes.Add($"http://localhost:{port}/");
                            Listener.Start();
                        }
                        catch (HttpListenerException exception)
                        {
                            if (exception.ErrorCode == 5)  // Access denied - race condition?
                                Listener.Prefixes.Clear();
                            else
                                throw;                     // everything else is a failure
                        }
                    }                    
                    if (!Listener.IsListening)
                    {
                        Listener.Close();
                        Listener = null;
                        throw new Exception("Help server could not bind to an available port.");
                    }

                    Trace.WriteLine(Listener.Prefixes.First(), "Listening");

                    Listener.BeginGetContext(ListenerCallback, null);
                }
                else
                {
                    Listener.Close();
                    Listener = null;
                }
                enabled = value;
            }
        }
        #endregion

        public Dictionary<string, RequestHandler> Handlers { get; } = new Dictionary<string, RequestHandler>(StringComparer.InvariantCulture);
        private RequestHandler DefaultHandler { get; } = new RequestHandler();

        /// <summary>
        /// Gets the url for a help page.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetUrl(string name)
        {
            if (!Enabled)
                throw new InvalidOperationException("Help server not enabled.");

            return Listener.Prefixes.First() + name;
        }

        private static bool TryGetUnusedPort(out int port)
        {
            var ports = new HashSet<int>(IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Select(a => a.Port));

            for (var i = 49215; i <= 65535; i++)
            {
                if (ports.Contains(i)) continue;
                port = i;
                return true;
            }

            port = 0;
            return false;
        }

        public void ListenerCallback(IAsyncResult result)
        {
            var context = Listener.EndGetContext(result); // complete the asynchronous operation.

            Listener.BeginGetContext(ListenerCallback, null); // get the next request running

            var request = context.Request;
            var response = context.Response;

            if (request.HttpMethod != "GET")  // only handle get requests.
            {
                DefaultHandler.ReplyWithError(response, HttpStatusCode.MethodNotAllowed);         
            }
            else
            {
                var extension = Path.GetExtension(request.Url.LocalPath) ?? "";
                extension = extension.TrimStart('.');

                if (!Handlers.TryGetValue(extension, out var handler))
                    handler = DefaultHandler;

                var ressourceName = NamespacePrefix + request.Url.LocalPath.Replace('/', '.');
                using (var stream = Assembly.GetManifestResourceStream(ressourceName))
                {
                    handler.SendResponse(request, response, stream);
                }
            }
        }
    }
}
