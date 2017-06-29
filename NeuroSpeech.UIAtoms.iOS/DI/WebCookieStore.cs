//using Foundation;
//using NeuroSpeech.UIAtoms.DI;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Text;

//[assembly: Xamarin.Forms.Dependency(typeof(WebCookieStore))]

//namespace NeuroSpeech.UIAtoms.DI
//{
//    public class WebCookieStore : IWebCookieStore
//    {
//        public WebCookieStore()
//        {
//            NSHttpCookieStorage.SharedStorage.AcceptPolicy = NSHttpCookieAcceptPolicy.Always;
//        }

//        private NSHttpCookie From(Cookie cookie)
//        {
//            NSHttpCookie c = new NSHttpCookie(cookie);

//            return c;
//        }

//        private Cookie From(NSHttpCookie cookie)
//        {
//            return new Cookie
//            {
//                Domain = cookie.Domain,
//                Secure = cookie.IsSecure,
//                Path = cookie.Path,
//                Name = cookie.Name,
//                Value = cookie.Value,
//                Expires = DateTime.SpecifyKind((DateTime)cookie.ExpiresDate, DateTimeKind.Unspecified)
//            };
//        }

//        #region IWebCookieStore implementation

//        public void Add(Cookie cookie)
//        {
//            NSHttpCookieStorage.SharedStorage.SetCookie(From(cookie));
//        }

//        public void Remove(Cookie cookie)
//        {
//            NSHttpCookieStorage.SharedStorage.DeleteCookie(From(cookie));
//        }

//        public void Clear()
//        {
//            NSHttpCookieStorage.SharedStorage.RemoveCookiesSinceDate(NSDate.FromTimeIntervalSince1970(0));
//        }

//        public IEnumerable<Cookie> GetCookies()
//        {
//            foreach (var c in NSHttpCookieStorage.SharedStorage.Cookies)
//            {
//                yield return From(c);
//            }

//        }

//        #endregion
//    }
//}
