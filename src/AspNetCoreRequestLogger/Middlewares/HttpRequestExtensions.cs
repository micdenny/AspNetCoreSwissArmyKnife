using System.IO;
using Microsoft.AspNetCore.Http.Internal;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Dump the raw http request to a string. 
        /// </summary>
        public static string ToRaw(this HttpRequest request)
        {
            using (StringWriter writer = new StringWriter())
            {
                WriteStartLine(request, writer);
                WriteHeaders(request, writer);
                WriteBody(request, writer);
                return writer.ToString();
            }
        }

        private static void WriteStartLine(HttpRequest request, StringWriter writer)
        {
            writer.Write(request.Method);
            writer.Write(" " + request.Path);
            writer.WriteLine(" " + request.Protocol);
        }

        private static void WriteHeaders(HttpRequest request, StringWriter writer)
        {
            foreach (string key in request.Headers.Keys)
            {
                writer.WriteLine(string.Format("{0}: {1}", key, request.Headers[key]));
            }
        }

        private static void WriteBody(HttpRequest request, StringWriter writer)
        {
            // HACK: change the body stream so that it can be read and then repositoned to the beginning
            // see https://github.com/aspnet/KestrelHttpServer/issues/1113
            request.EnableRewind();

            StreamReader reader = new StreamReader(request.Body);
            try
            {
                string body = reader.ReadToEnd();
                if (body.Length > 0)
                {
                    writer.WriteLine();
                    writer.Write(body);
                }
            }
            finally
            {
                request.Body.Position = 0;
            }

        }
    }
}
