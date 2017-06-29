using NeuroSpeech.UIAtoms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UIAtomsDemo.Forms.Models;
using Xamarin.Forms;

namespace UIAtomsDemo.Forms.Services
{

    public class BaseService : IDisposable
    {

        public BaseService()
        {
            /*if (Device.OS == TargetPlatform.iOS) {
				UserAgent = "Audition-800 iOS";
			}*/

            switch (Device.OS)
            {
                case TargetPlatform.Other:
                    break;
                case TargetPlatform.iOS:
                    UserAgent = "Audition-800 iOS";
                    break;
                case TargetPlatform.Android:
                    UserAgent = "Audition-800 Android";
                    break;
                case TargetPlatform.WinPhone:
                    break;
                case TargetPlatform.Windows:
                    break;
                default:
                    break;
            }

            CreateClient();
        }

        protected virtual void CreateClient()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = true;
            //handler.CookieContainer = AppCookieStore.CookieContainer;
            Client = new HttpClient(handler);
            Client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            BaseUrl = UIAtomsApplication.Instance.BaseUrl;
        }

        protected HttpClient Client
        {
            get;
            private set;
        }

        #region IDisposable implementation

        public void Dispose()
        {

        }

        #endregion

        protected string UserAgent
        {
            get;
            set;
        }

        protected string BaseUrl
        {
            get;
            set;
        }

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

        protected virtual async Task<HttpResponseMessage> PostAsync(string path, object body)
        {

            string url = BaseUrl + path;
            HttpContent content = body as HttpContent;
            if (content == null)
            {
                if (body is string)
                {
                    content = new StringContent((string)body);
                }
                else
                {
                    content = new StringContent(JsonConvert.SerializeObject(body));
                }
            }
            var result = await Client.PostAsync(url, content);
            if ((int)result.StatusCode > 300)
            {
                var r = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(result.ReasonPhrase);
                Debug.WriteLine(r);
                throw new ServiceException(result.ReasonPhrase, r);
            }
            return result;


        }

        protected virtual async Task<HttpResponseMessage> GetAsync(string path, object p = null)
        {



            string url = BaseUrl + path;
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

            Debug.WriteLine($"Loading: {url}");

            var result = await Client.GetAsync(url);
            if ((int)result.StatusCode > 300)
            {
                var r = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(result.ReasonPhrase);
                Debug.WriteLine(r);
                throw new ServiceException(result.ReasonPhrase, r);
            }
            Debug.WriteLine($"Loaded: {url}");
            return result;


        }

        protected virtual async Task<T> JsonPostAsync<T>(string path, object body)
        {
            var r = await PostAsync(path, body);
            var o = await r.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<T>(o);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        protected virtual async Task<T> JsonGetAsync<T>(string path, object p = null)
        {
            var r = await GetAsync(path, p);
            var o = await r.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<T>(o);
            }
            catch (Exception ex)
            {
                Console.WriteLine("URL:" + path);
                Console.WriteLine(o);
                Console.WriteLine(ex);
                throw ex;
            }
        }

        protected virtual async Task<byte[]> GetBytesAsync(string path, object p = null)
        {
            var r = await GetAsync(path, p);
            return await r.Content.ReadAsByteArrayAsync();
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

    }

    public class JsonService: BaseService
    {

        public static JsonService Instance = new JsonService();

        public JsonService()
        {
            BaseUrl = UIAtomsApplication.Instance.BaseUrl;
        }

        public virtual Task<Country[]> Countries() {
            return JsonGetAsync<Country[]>("/json-config/countries/countries.json");
        }

    }
}
