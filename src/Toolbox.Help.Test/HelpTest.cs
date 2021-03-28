using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Text;

namespace Toolbox.Help.Test
{
    [TestClass]
    public class HelpTest
    {
        [TestMethod]
        public void EnableHelpServer()
        {
            var cut = new HelpServer(GetType().Assembly, GetType().Namespace + ".Help");

            cut.Enabled = true;

            Assert.IsTrue(cut.Enabled);
        }

        [TestMethod]
        public void GetUrlIndexPage()
        {
            var cut = new HelpServer(GetType().Assembly, GetType().Namespace + ".Help");

            cut.Enabled = true;

            var url = cut.GetUrl("index.html");

            Assert.IsTrue(url.EndsWith("index.html"));
        }

        [TestMethod]
        public void GetIndexPage()
        {
            var prefix = GetType().Namespace + ".Help";
            var cut = new HelpServer(GetType().Assembly, prefix);

            cut.Enabled = true;

            var url = cut.GetUrl("index.html");

            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();

            var content = GetType().Assembly.GetManifestResourceStream(prefix + ".index.html");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("text/html", response.ContentType);
            Assert.AreEqual(content.Length, response.ContentLength);
            var stream = response.GetResponseStream();
            for (var i = 0; i < content.Length; i++)
            {
                Assert.AreEqual(content.ReadByte(), stream.ReadByte(), $"stream[{i}]");
            }
        }

        [TestMethod]
        public void GetIndexPageWithTypeConstructor()
        {
        var cut = new HelpServer(GetType(), "Help");

            cut.Enabled = true;

            var url = cut.GetUrl("index.html");

            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();

            var content = GetType().Assembly.GetManifestResourceStream(GetType().Namespace + ".Help.index.html");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("text/html", response.ContentType);
            Assert.AreEqual(content.Length, response.ContentLength);
            var stream = response.GetResponseStream();
            for (var i = 0; i < content.Length; i++)
            {
                Assert.AreEqual(content.ReadByte(), stream.ReadByte(), $"stream[{i}]");
            }
        }


        [TestMethod]
        public void PostRequestFails()
        {
            var prefix = GetType().Namespace + ".Help";
            var cut = new HelpServer(GetType().Assembly, prefix);

            cut.Enabled = true;

            var url = cut.GetUrl("index.html");

            var request = WebRequest.CreateHttp(url);
            request.Method = "POST";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                Assert.Fail("No exception thrown");
            }
            catch (WebException exception)
            {
                var response = (HttpWebResponse)exception.Response;
                Assert.AreEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);                
            }            
        }

        [TestMethod]
        public void GetNotExistingPageFails()
        {
            var prefix = GetType().Namespace + ".Help";
            var cut = new HelpServer(GetType().Assembly, prefix);

            cut.Enabled = true;

            try
            {
                var url = cut.GetUrl("bad.html");

                var request = WebRequest.CreateHttp(url);
                request.Method = "GET";

                var response = (HttpWebResponse)request.GetResponse();

                Assert.Fail("No exception thrown");
            }
            catch (WebException exception)
            {
                var response = (HttpWebResponse)exception.Response;
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [TestMethod]
        public void GetIndexPageMultipleTimes()
        {
            var prefix = GetType().Namespace + ".Help";
            var cut = new HelpServer(GetType().Assembly, prefix);

            cut.Enabled = true;

            var url = cut.GetUrl("index.html");

            var count = 10;

            while (count-- > 0)
            {
                var request = WebRequest.CreateHttp(url);
                request.Method = "GET";

                var response = (HttpWebResponse)request.GetResponse();

                var content = GetType().Assembly.GetManifestResourceStream(prefix + ".index.html");

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual("text/html", response.ContentType);
                Assert.AreEqual(content.Length, response.ContentLength);
                var stream = response.GetResponseStream();
                for (var i = 0; i < content.Length; i++)
                {
                    Assert.AreEqual(content.ReadByte(), stream.ReadByte(), $"stream[{i}]");
                }
            }
        }

        [TestMethod]
        public void GetPageFromCustomHandler()
        {
            var prefix = GetType().Namespace + ".Help";
            var handler = new CustomHandler();

            var cut = new HelpServer(GetType().Assembly, prefix);
            cut.Handlers["custom"] = handler;

            cut.Enabled = true;

            var url = cut.GetUrl("index.custom");

            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(CustomHandler.ContentType, response.ContentType);
            Assert.AreEqual(handler.Id.Length, response.ContentLength);

            var buffer = new byte[response.ContentLength];
            var got = response.GetResponseStream().Read(buffer, 0, buffer.Length);
            var id = Encoding.Default.GetString(buffer);

            Assert.AreEqual(buffer.Length, got);
            Assert.AreEqual(handler.Id, id);
        }
    }
}
