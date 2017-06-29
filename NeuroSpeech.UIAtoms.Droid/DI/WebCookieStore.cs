//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.Webkit;
//using System.Net;
//using NeuroSpeech.UIAtoms.DI;

//[assembly: Xamarin.Forms.Dependency(typeof(WebCookieStore))]

//namespace NeuroSpeech.UIAtoms.DI
//{
//    public class WebCookieStore : IWebCookieStore
//    {

//        public WebCookieStore()
//        {
//            CookieManager.Instance.SetAcceptCookie(true);
//        }

//        public void Add(Cookie cookie)
//        {
//            CookieManager.Instance.SetCookie(UIAtomsApplication.Instance.BaseUrl, cookie.Name + "=" + cookie.Value);
//            CookieManager.Instance.Flush();
//        }

//        public void Clear()
//        {
//            CookieManager.Instance.RemoveAllCookie();
//        }

//        public IEnumerable<Cookie> GetCookies()
//        {
//            var baseUrl = UIAtomsApplication.Instance.BaseUrl;

//            // .GetCookie returns ALL cookies related to the URL as a single, long 
//            // string which we have to split and parse
//            var allCookiesForUrl = CookieManager.Instance.GetCookie(baseUrl);

//            if (string.IsNullOrWhiteSpace(allCookiesForUrl))
//            {
//                //LogDebug(string.Format("No cookies found for '{0}'. Exiting.", _url));
//                //yield return new Cookie("none", "none");
//                Console.WriteLine($"No cookies found for {baseUrl}");
//                yield break;
//            }
//            else
//            {
//                //LogDebug(string.Format("\r\n===== CookieHeader : '{0}'\r\n", allCookiesForUrl));

//                var cookiePairs = allCookiesForUrl.Split(' ');
//                foreach (var cookiePair in cookiePairs.Where(cp => cp.Contains("=")))
//                {
//                    // yeah, I know, but this is a quick-and-dirty, remember? ;)
//                    var cookiePieces = cookiePair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
//                    if (cookiePieces.Length >= 2)
//                    {
//                        cookiePieces[0] = cookiePieces[0].Contains(":")
//                          ? cookiePieces[0].Substring(0, cookiePieces[0].IndexOf(":"))
//                          : cookiePieces[0];

//                        // strip off trailing ';' if it's there (some implementations 
//                        // on droid have it, some do not)
//                        cookiePieces[1] = cookiePieces[1].EndsWith(";")
//                          ? cookiePieces[1].Substring(0, cookiePieces[1].Length - 1)
//                          : cookiePieces[1];

//                        var c = new Cookie()
//                        {
//                            Name = cookiePieces[0],
//                            Value = cookiePieces[1],
//                            Path = "/",
//                            Domain = new Uri(baseUrl).DnsSafeHost,
//                        };

//                        Console.WriteLine($"Found Cookie {c.Name} = {c.Value}");

//                        yield return c;
//                    }
//                }
//            }
//        }
//    }
//}