using NeuroSpeech.UIAtoms.DI;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Web
{

    public enum LogMode {
        Url,
        Headers,
        Body,
        BodyOnError
    }





    public class JsonRestClient : IDisposable
    {

        private static JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        private INotificationService notify;

        public JsonRestClient()
        {

            Client = CreateClient();
            CreateCache();

            notify = DependencyService.Get<INotificationService>();
        }

        private void CreateCache()
        {
            this.Cache = DependencyService.Get<ShortMemoryCache>();
        }

        protected virtual HttpClient CreateClient()
        {
            return DependencyService.Get<IWebClient>().Client;

        }

        protected HttpClient Client
        {
            get;
            private set;
        }

        public JsonRestclientLogger Logger { get; set; }

        #region IDisposable implementation

        public void Dispose()
        {

        }

        #endregion

        private string _UserAgent;
        protected string UserAgent
        {
            get {
                return _UserAgent;
            }
            set {
                _UserAgent = value;
                Client.DefaultRequestHeaders.Remove("User-Agent");
                if (_UserAgent != null)
                {
                    Client.DefaultRequestHeaders.Add("User-Agent", value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        protected virtual string BaseUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="url">The URL.</param>
        /// <param name="pl">The pl.</param>
        /// <returns></returns>
        protected virtual Task<T> InvokeAsync<T, TError>(
           HttpMethod method,
           string url,
           params RestParameter[] pl)
           where T : class
        {
            return Run<T>(async tw =>
            {
                Type resultType = typeof(T);

                if (resultType == typeof(Stream)) {
                    throw new NotSupportedException("Stream Type not supported as return type, please use HttpResponseMessage instead");
                }

                HttpRequestMessage message = new HttpRequestMessage(method, url);
                message.Headers.Add("Accept", "application/json");

                if (!url.Contains("?"))
                {
                    url += "?";
                }

                List<KeyValuePair<string, string>> form = null;

                List<KeyValuePair<string, string>> jsonContent = null;

                foreach (var p in pl)
                {
                    if (p.Value == null)
                        continue;
                    switch (p.Type)
                    {
                        case RestParameterType.Header:
                            message.Headers.Add(p.Name, p.Value.ToString());
                            break;
                        case RestParameterType.Query:
                            url += "&" + p.Name + "=" + Uri.EscapeDataString(p.Value.ToString());
                            break;
                        case RestParameterType.Body:
                            if (message.Content != null)
                                throw new ArgumentException();
                            message.Content = EncodePostBody(p.Value);
                            break;
                        case RestParameterType.BodyPath:
                            jsonContent.Add(new KeyValuePair<string, string>(p.Name, JsonConvert.SerializeObject(p.Value, settings)));
                            break;
                        case RestParameterType.Path:
                            url = url.Replace("{" + p.Name + "}", Uri.EscapeUriString(p.Value.ToString()));
                            break;
                        case RestParameterType.Form:
                            form = form ?? new List<KeyValuePair<string, string>>();
                            form.Add(new KeyValuePair<string, string>(p.Name, Uri.EscapeDataString(p.Value.ToString())));
                            break;
                        default:
                            break;
                    }
                }

                if (form != null)
                {
                    if (message.Content != null)
                        throw new ArgumentException();
                    message.Content = new FormUrlEncodedContent(form);
                }

                if (jsonContent != null) {
                    if (message.Content != null)
                        throw new ArgumentException();
                    message.Content = EncodePostBodyContent(jsonContent);
                }

                if (!(url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))) {
                    if (!url.StartsWith("/")) {
                        url = "/" + url;
                    }
                    url = BaseUrl + url;
                }

                message.RequestUri = new Uri(url);

                await Logger?.LogRequestAsync(tw, message);

                if (resultType == typeof(HttpResponseMessage)) {
                    return (T)(object)(await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead));
                }

                using (var response = await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead))
                {

                    byte[] data = null;
                    string content = null;

                    if (resultType == typeof(HttpResponseMessage))
                    {
                        Logger?.LogResponse(tw, response, null);
                        return (T)(object)response;
                    }

                    if (resultType == typeof(Stream))
                    {
                        Logger?.LogResponse(tw, response, null);
                        return (T)(object)(await response.Content.ReadAsStreamAsync());
                    }

                    object result = null;

                    if (resultType == typeof(byte[]))
                    {
                        data = await response.Content.ReadAsByteArrayAsync();
                        result = data;
                    }
                    else
                    {
                        content = await response.Content.ReadAsStringAsync();
                        result = content;
                    }

                    Logger?.LogResponse(tw, response, result);
                    if ((int)response.StatusCode > 300)
                    {
                        if (typeof(TError) == typeof(object))
                        {
                            throw new ApiException(content);
                        }
                        else
                        {
                            throw new ApiException<TError>("Api Error", Deserialize<TError>(content));
                        }
                    }

                    if (resultType == typeof(string))
                        return (T)result;

                    return Deserialize<T>(content);
                }
            });
        }

        protected virtual HttpContent EncodePostBodyContent(List<KeyValuePair<string, string>> jsonContent)
        {
            string json = "{\r\n" +  string.Join(",\r\n", jsonContent.Select( x=> "\t\"" + x.Key + "\": " + x.Value) )   + "}";
            return new FormData(new KeyValuePair<string, string>("formModel",json));
        }

        protected virtual T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text, settings);
        }

        protected ShortMemoryCache Cache { get; private set; }

        protected virtual Task<T> SendResult<T>(T item)
        {
            TaskCompletionSource<T> taskResult = new TaskCompletionSource<T>();
            Device.BeginInvokeOnMainThread(async () => {

                // artificial delay of 1 second
                await Task.Delay(1000);

                taskResult.SetResult(item);
            });
            return taskResult.Task;
        }

        protected virtual Task<string> PostAsync(string path, object query, object body)
        {

            return Run(async (tw) => {
                string url = PrepareUrl(path, query);
                HttpRequestMessage message = PrepareMessage(HttpMethod.Post, url, false);
                message.Content = EncodePostBody(body);

                await Logger?.LogRequestAsync(tw,message);

                using (var r = await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead))
                {

                    var content = await r.Content.ReadAsStringAsync();

                    Logger?.LogResponse(tw, r, content);

                    if ((int)r.StatusCode > 300)
                    {
                        throw new ServiceException(content, content);
                    }


                    return content;
                }
            });

        }

        protected virtual Task<byte[]> PostRawAsync(string path, object query, object body)
        {

            return Run(async (tw) => {
                string url = PrepareUrl(path, query);
                HttpRequestMessage message = PrepareMessage(HttpMethod.Post, url, false);
                message.Content = EncodePostBody(body);

                await Logger?.LogRequestAsync(tw, message);

                using (var r = await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead))
                {

                    var content = await r.Content.ReadAsByteArrayAsync();

                    Logger?.LogResponse(tw, r, content);

                    if ((int)r.StatusCode > 300)
                    {
                        throw new ServiceException(content.ToString(), content.ToString());
                    }


                    return content;
                }
            });

        }

        protected virtual HttpContent EncodePostBody(object body)
        {
            HttpContent content = body as HttpContent;
            if (content == null)
            {
                string textBody = body as string;
                if (textBody != null)
                {
                    content = new StringContent(textBody);
                }
                else
                {
                    textBody = JsonConvert.SerializeObject(body);
                    content = new FormData(
                        new KeyValuePair<string, string>("formModel", textBody)
                    );
                }
            }

            return content;
        }


        private Task<T> Run<T>(Func<TextWriter,Task<T>> f) {
            return Task.Run(async () =>
            {
                using (ShowBusy())
                {

                    TextWriter sw = null;

                    try
                    {
                        sw = Logger?.BeginLog();
                        return await f(sw);
                    }
                    catch (Exception ex)
                    {
                        Logger?.LogException(sw, ex);
                        throw ex;
                    }
                    finally
                    {
                        Logger?.EndLog(sw);
                    }
                }
            });
            
        }

        private IDisposable ShowBusy()
        {
            IDisposable d = null;

            Device.BeginInvokeOnMainThread(() => {
                d = notify.ShowBusy();
            });

            return new AtomDisposableAction(()=> {
                Device.BeginInvokeOnMainThread(() =>
                {
                    d?.Dispose();
                });
            });
        }

        protected virtual Task<string> GetAsync(string path, object query = null, bool cached = false)
        {
            return Run(async (tw) => {
                string url = PrepareUrl(path, query);

                if (cached) {
                    string cachedResult = Cache.Get(url) as string;
                    if (cachedResult != null)
                        return cachedResult;
                }

                HttpRequestMessage message = PrepareMessage(HttpMethod.Get, url, false);

                await Logger?.LogRequestAsync(tw,message);

                using (var r = await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead))
                {
                    var content = await r.Content.ReadAsStringAsync();

                    Logger?.LogResponse(tw, r, content);

                    if ((int)r.StatusCode > 300)
                        throw new ServiceException(content, content);

                    if (cached)
                    {
                        Cache.Add(url, content);
                    }

                    return content;
                }
            });
        }



        protected string PrepareUrl(string path, object p)
        {
            
            string url = 
                path.StartsWith("https://") || path.StartsWith("http://") 
                ? path
                : (BaseUrl + path);

            if (p != null)
            {
                if (url.Contains("?"))
                {
                    url += "&";
                }
                else
                {
                    url += "?";
                }
                foreach (var prop in p.GetType().GetProperties())
                {
                    object v = prop.GetValue(p);
                    if (v == null)
                        continue;

                    if (!(v is string || v.GetType().IsValueType))
                    {
                        v = JsonConvert.SerializeObject(v);
                    }

                    url += prop.Name + "=" + System.Net.WebUtility.UrlEncode(v.ToString()) + "&";
                }
            }

            return url;
        }

        protected virtual Task<T> JsonPostAsync<T>(string path, object query, object body)
            where T:class
        {
            return JsonSendAsync<T>(HttpMethod.Post, path, query, body);
        }

        protected virtual Task<T> JsonPutAsync<T>(string path, object query, object body)
            where T : class
        {
            return JsonSendAsync<T>(HttpMethod.Put, path, query, body);
        }

        protected virtual Task<T> JsonDeleteAsync<T>(string path, object query, object body)
            where T : class
        {
            return JsonSendAsync<T>(HttpMethod.Delete, path, query, body);
        }

        protected virtual Task<T> JsonSendAsync<T>(HttpMethod method, string path, object query, object body)
            where T : class
        {
            return Run(async (tw) =>
            {
                string url = PrepareUrl(path, query);
                HttpRequestMessage message = PrepareMessage(method, url, true);
                message.Content = EncodePostBody(body);
                await Logger?.LogRequestAsync(tw,message);
                using (var r = await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead))
                {
                    var content = await r.Content.ReadAsStringAsync();
                    Logger?.LogResponse(tw, r, content);
                    if ((int)r.StatusCode > 300)
                        throw new ServiceException(content, content);

                    return ParseJson<T>(content);
                }
            });
        }


        protected virtual T ParseJson<T>(string content)
            where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Json Parsing failed", content, ex);
            }
        }

        protected virtual Task<T> JsonPostAsync<T>(string path, object body)
            where T : class
        {
            return JsonPostAsync<T>(path, null, body);
        }

        private static System.Net.Http.Headers.MediaTypeWithQualityHeaderValue JSON
            = System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json");

        private HttpRequestMessage PrepareMessage(HttpMethod method, string url, bool acceptJson)
        {
            HttpRequestMessage m = new HttpRequestMessage(method, url);
            if (acceptJson)
            {
                m.Headers.Add("accept", "application/json");
            }
            return m;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <param name="cached"></param>
        /// <returns></returns>
        protected virtual Task<T> JsonGetAsync<T>(string path, object query, bool cached = false)
            where T : class
        {
            return Run(async (tw) =>
            {
                string url = PrepareUrl(path, query);


                if (cached)
                {
                    T cachedResult = Cache.Get(url) as T;
                    if (cachedResult != null)
                    {
                        Logger?.LogText(tw,$"Cached Response for {url}");
                        return cachedResult;
                    }
                }

                HttpRequestMessage message = PrepareMessage(HttpMethod.Get, url, true);
                await Logger?.LogRequestAsync(tw,message);
                var r = await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
                var content = await r.Content.ReadAsStringAsync();
                Logger?.LogResponse(tw, r,content);
                if ((int)r.StatusCode > 300)
                    throw new ServiceException(content, content);

                T item = ParseJson<T>(content);
                if (cached) {
                    Cache.Add(url, item);
                }
                return item;
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <param name="cached"></param>
        /// <returns></returns>
        protected virtual Task<byte[]> GetBytesAsync(string path, object query = null, bool cached = false)
        {
            return Run(async (tw) =>
            {
                string url = PrepareUrl(path, query);

                if (cached) {
                    byte[] cachedResult = Cache.Get(url) as byte[];
                    if (cachedResult != null)
                    {
                        Logger?.LogText(tw, $"Cached Response for {url}");
                        return cachedResult;
                    }
                }

                HttpRequestMessage message = PrepareMessage(HttpMethod.Post, url, true);
                await Logger?.LogRequestAsync(tw,message);
                using (var r = await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead))
                {

                    if ((int)r.StatusCode > 300)
                    {
                        var content = await r.Content.ReadAsStringAsync();
                        Logger?.LogResponse(tw, r, content);
                        throw new ServiceException(content, content);
                    }

                    var result = await r.Content.ReadAsByteArrayAsync();
                    Logger?.LogResponse(tw, r, result);
                    if (cached)
                    {
                        Cache.Add(url, result);
                    }

                    return result;
                }
            });
        }

    }

    public class ServiceException : Exception
    {

        public string Content
        {
            get;
            set;
        }

        public ServiceException(string message, string content) : base(message)
        {
            this.Content = content;
        }

        public ServiceException(string message, string content, Exception inner) : base(message, inner)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            string text = $"{Message}\r\nContent:\r\n\t\t{Content.Replace("\r\n", "\r\n\t\t\t")}\r\n\r\n";

            if (InnerException != null)
            {
                return text + "Inner Exception:\r\n" + InnerException.ToString().Replace("\r\n", "\r\n\t\t\t");
            }

            return text;
        }
    }


    public class ApiException : Exception
    {
        public ApiException(string msg, Exception ex) : base(msg, ex)
        {

        }

        public ApiException(string msg) : base(msg)
        {

        }
    }

    public class ApiException<T> : Exception
    {

        public ApiException(string msg, T content) : base(msg)
        {
            Error = content;
        }

        public T Error { get; set; }

        public override string ToString()
        {
            return Message + "\r\n" + JsonConvert.SerializeObject(Error) + "\r\n" + base.ToString();
        }
    }
}
