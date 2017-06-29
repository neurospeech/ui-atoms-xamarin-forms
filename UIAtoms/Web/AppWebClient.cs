using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;
using NeuroSpeech.UIAtoms.Web.Impl;
using NeuroSpeech.UIAtoms.DI;
using Xamarin.Forms;

#if __DROID__
using Android.Webkit;
#endif

#if __IOS__
using Foundation;
#endif

[assembly: Xamarin.Forms.Dependency(typeof(AtomWebClient))]

namespace NeuroSpeech.UIAtoms.Web.Impl
{
    public class AtomWebClient : HttpClientHandler, IWebClient
    {



        public AtomWebClient()
        {
            this.UseCookies = false;

#if __DROID__
            CookieManager.Instance.SetAcceptCookie(true);
#endif

#if __IOS__
            NSHttpCookieStorage.SharedStorage.AcceptPolicy = NSHttpCookieAcceptPolicy.Always;
#endif

            Client = new HttpClient(this);


            

        }

        public HttpClient Client
        {
            get;
        }



        public string UserAgent { get; set; }

        private ConcurrentDictionary<string, string> CachedCookieStore = new ConcurrentDictionary<string, string>();

        ICacheProvider cacheProvider;


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
        /*    return async SendAsync(request, cancellationToken, true);
        }


        /// <summary>
        /// We want to make sure we do not send multiple requests for exactly same cached URL...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="cached"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken, bool cached)
        {
        */

            bool isGet = request.Method == HttpMethod.Get;

            string fullUri = request.RequestUri.ToString();

            if (isGet)
            {
                var shouldNotCache = request.Headers?.CacheControl?.NoCache ?? false;

                //if (cached && !shouldNotCache)
                //{
                //    return await AtomCacheProvider.Instance.GetAsync(fullUri, c => SendAsync(request, cancellationToken, false), cancellationToken);
                //}


                if (cacheProvider == null)
                {
                    cacheProvider = DependencyService.Get<ICacheProvider>();
                    if (cacheProvider == null)
                    {
                        System.Diagnostics.Debug.WriteLine("AppWebClient Warning !! No class registered for ICacheProvider ");
                    }
                    else {
                        System.Diagnostics.Debug.WriteLine($"AppWebClient: Class {cacheProvider.GetType().FullName} registered for ICacheProvider ");
                    }
                }

                if (cacheProvider != null)
                {

                    if (!(shouldNotCache))
                    {

                        var cr = await cacheProvider.GetAsync(fullUri, cancellationToken);
                        if (cr != null)
                        {
                            return cr;
                        }
                    }
                }
            }

            // load cookies..
            Uri uri = new Uri(fullUri.Split('?')[0]);

            string currentCookies = GetCookies(uri);
            if (!string.IsNullOrWhiteSpace(currentCookies))
            {
                request.Headers.Add("Cookie", currentCookies);
            }

            if (!string.IsNullOrWhiteSpace(UserAgent))
            {
                request.Headers.Add("User-Agent", UserAgent);
            }

            var r = await base.SendAsync(request, cancellationToken);

            

            // Set cookies...

            IEnumerable<string> cookies = null;

            if (!r.Headers.TryGetValues("Set-Cookie", out cookies))
            {
                r.Headers.TryGetValues("Set-Cookie2", out cookies);
            }

            if (cookies != null && cookies.Any())
            {

                // check if we have already stored all cookies or not...
                SetCookies(uri, cookies.FirstOrDefault());

            }

            if (isGet)
            {
                if (cacheProvider != null)
                {
                    return await cacheProvider.PutAsync(fullUri, r, cancellationToken);
                }
            }

            return r;
        }

        protected void SetCookies(Uri uri, string v)
        {
            CachedCookieStore.Clear();
#if __DROID__
            CookieManager.Instance.SetCookie(uri.ToString(), v);
            try
            {
                CookieManager.Instance.Flush();
            }
            catch {
            }
#endif
#if __IOS__
            var cookieParser = new CookieContainer();
            cookieParser.SetCookies(uri, v);

            foreach (System.Net.Cookie cookie in cookieParser.GetCookies(uri)) {
                NSHttpCookie nsc = new NSHttpCookie(cookie);
                NSHttpCookieStorage.SharedStorage.SetCookie(nsc);
            }
#endif

        }


#if __DROID__
        protected string GetCookies(Uri uri)
        {

            return CachedCookieStore.GetOrAdd(uri.ToString(), k => {

                CookieContainer cookieContainer = new CookieContainer();


                // load webkit cookie...

                var webKitCookies = CookieManager.Instance.GetCookie(k);
                if (webKitCookies == null)
                {
                    return "";
                }

                // current parsing...
                cookieContainer.SetCookies(uri, webKitCookies);

                List<string> cookies = new List<string>();
                foreach (System.Net.Cookie cookie in cookieContainer.GetCookies(uri))
                {
                    cookies.Add(cookie.Name + "=" + cookie.Value);
                }

                return string.Join(";", cookies);

            });
        }
#endif

#if __IOS__
        protected virtual string GetCookies(Uri url)
        {
            return CachedCookieStore.GetOrAdd(url.ToString(), k => {

                UriBuilder ub = new UriBuilder(k);
                ub.Query = null;


                var cookies = NSHttpCookieStorage.SharedStorage.CookiesForUrl(NSUrl.FromString(ub.ToString()));

                return string.Join(";", cookies.Select(x => $"{x.Name}={x.Value}"));


            });
        }
#endif



        public void ClearCookies() {
            Task.Run(() =>
            {
                CachedCookieStore.Clear();
#if __DROID__
                CookieManager.Instance.RemoveAllCookie();
#endif

#if __IOS__
                foreach (var cookie in Foundation.NSHttpCookieStorage.SharedStorage.Cookies) {
                    Foundation.NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
                }
#endif
            });
        }

    }


   
}
