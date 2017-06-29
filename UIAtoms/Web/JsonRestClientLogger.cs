using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms.Web
{

    public class RestClientWriter : StringWriter {

        public RestClientWriter()
        {

        }

        public DateTime Date { get; set; }

    }

    public class JsonRestclientLogger
    {

        public LogMode Mode { get; set; } = LogMode.Url;

        public Action<string> Log { get; set; }
            = (s) => {
                System.Diagnostics.Debug.WriteLine("JSON-HTTP-CLIENT: " + s.Replace("\n", "\nJSON-HTTP-CLIENT: "));
            };

        private static ConcurrentQueue<RestClientWriter> _pool = new ConcurrentQueue<RestClientWriter>();

        public RestClientWriter BeginLog()
        {
            RestClientWriter sw;
            if (_pool.TryDequeue(out sw))
            {
                sw.GetStringBuilder().Clear();
                sw.Date = DateTime.UtcNow; 
                return sw;
            }
            sw = new RestClientWriter();
            sw.Date = DateTime.UtcNow;
            return sw;
        }

        public async Task LogRequestAsync(TextWriter writer, HttpRequestMessage message)
        {
            writer.WriteLine($"{message.Method.Method} {message.RequestUri.ToString()} {message.Version}");
            if (Mode == LogMode.Headers || Mode == LogMode.Body)
            {
                foreach (var h in message.Headers)
                {
                    writer.WriteLine($"{h.Key}: { string.Join(";", h.Value)}");
                }
                if (message.Content != null)
                {
                    foreach (var h in message.Content.Headers)
                    {
                        writer.WriteLine($"{h.Key}: { string.Join(";", h.Value)}");
                    }
                }
            }
            if (Mode == LogMode.Body && message.Content != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await message.Content.CopyToAsync(ms);
                    writer.WriteLine(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
                }
            }
        }
        public void LogResponse(TextWriter writer, HttpResponseMessage r, object content)
        {
            TimeSpan ts = DateTime.UtcNow - ((RestClientWriter)writer).Date;
            writer.WriteLine($"{(int)r.StatusCode} {r.ReasonPhrase} Total Time (Seconds): {ts.TotalSeconds}");

            if (Mode == LogMode.Body || Mode == LogMode.Headers)
            {
                foreach (var h in r.Headers)
                {
                    writer.WriteLine($"{h.Key}: { string.Join(";", h.Value)}");
                }
                foreach (var h in r.Content.Headers)
                {
                    writer.WriteLine($"{h.Key}: { string.Join(";", h.Value)}");
                }
            }

            if (Mode == LogMode.Body || ( Mode == LogMode.BodyOnError && ((int)r.StatusCode) > 300))
            {

                if (content != null)
                {
                    if (content is string)
                    {
                        writer.WriteLine(content as string);
                    }
                    else
                    {
                        if (content is byte[])
                        {
                            writer.WriteLine(System.Text.Encoding.UTF8.GetString(content as byte[]));
                        }
                        else
                        {
                            writer.WriteLine(content);
                        }
                    }
                }
            }
        }
        public void LogException(TextWriter writer, Exception ex)
        {
            if (ex is ServiceException)
                return;
            writer.WriteLine(ex.ToString());
        }

        public void LogText(TextWriter writer, string text)
        {
            writer.WriteLine(text);
        }

        public void EndLog(TextWriter writer)
        {
            Log?.Invoke(writer.ToString());
            _pool.Enqueue((RestClientWriter)writer);
        }
    }
}
