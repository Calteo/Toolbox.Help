using System.IO;
using System.Net;

namespace Toolbox.Help.Handlers
{
    /// <summary>
    /// Base class for handling help requests from <see cref="HelpServer"/>.
    /// </summary>
    public class RequestHandler
    {
        /// <summary>
        /// Send the response for a given reqeuest
        /// </summary>
        /// <param name="request">The requested information.</param>
        /// <param name="response">The response the make.</param>
        /// <param name="stream">The ressource stream ot the requested url if it exists.</param>
        public virtual void SendResponse(HttpListenerRequest request, HttpListenerResponse response, Stream stream)
        {
            if (stream == null)
            {
                ReplyWithError(response, HttpStatusCode.NotFound);
            }
            else
            {
                response.ContentLength64 = stream.Length;
                stream.CopyTo(response.OutputStream);
            }
        }

        /// <summary>
        /// Send an <see cref="HttpStatusCode"/> as the response
        /// </summary>
        /// <param name="response">The response to make.</param>
        /// <param name="code">The code the the response gets.</param>
        /// <remarks>
        /// The response will be closed.
        /// </remarks>
        public void ReplyWithError(HttpListenerResponse response, HttpStatusCode code)
        {
            response.StatusCode = (int)code;
            response.ContentLength64 = 0;
            response.OutputStream.Close();
        }
    }
}
