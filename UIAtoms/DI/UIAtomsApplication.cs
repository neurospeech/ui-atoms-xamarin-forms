using NeuroSpeech.UIAtoms;
using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(NavigationContainer))]

namespace NeuroSpeech.UIAtoms
{

    /// <summary>
    /// 
    /// </summary>
    public partial class UIAtomsApplication
    {

        /// <summary>
        /// 
        /// </summary>
        public static UIAtomsApplication Instance = new UIAtomsApplication();

        /// <summary>
        /// 
        /// </summary>
        public Action<Exception> LogException = e => {
            System.Diagnostics.Debug.Fail("Exception", e.ToString());
        };


        /// <summary>
        /// 
        /// </summary>
        public INotificationService NotificationService
        {
            get
            {
                return DependencyService.Get<INotificationService>(DependencyFetchTarget.GlobalInstance);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IAppNavigator Navigator
        {
            get
            {
                return DependencyService.Get<IAppNavigator>(DependencyFetchTarget.GlobalInstance);
            }
        }


        /// <summary>
        /// Trigger given action after given timeout, this guarentees single execution of event
        /// within specified timeout
        /// </summary>
        /// Example, 
        ///     If you call
        ///         
        ///         Trigger(ref id, ()=>Print(1), TimeSpan.FromMilliseconds(100));
        ///         // just after 2 milliseconds
        ///         Trigger(ref id, ()=>Print(2), TimeSpan.FromMilliseconds(100));
        ///         // just after 10 milliseconds
        ///         Trigger(ref id, ()=>Print(3), TimeSpan.FromMilliseconds(100));
        ///         
        /// Results,
        ///        
        ///         After 112 milliseconds, it will only print 3, 
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <param name="timeout"></param>
        public void Trigger(ref CancellationTokenSource id, Action action, TimeSpan timeout)
        {
            id?.Cancel();
            id = SetTimeout(action, timeout);
        }

        private static TimeSpan defaultTimeout = TimeSpan.FromMilliseconds(100);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        public void Trigger(ref CancellationTokenSource id, Action action)
        {
            Trigger(ref id, action, defaultTimeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void TriggerOnce(Action action)
        {
            TriggerOnce(action, defaultTimeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="timeout"></param>
        public void TriggerOnce(Action action, TimeSpan timeout)
        {
            Device.BeginInvokeOnMainThread( async () =>
            {
                //ActionKey key = new ActionKey(action);
                var key = action.Method;
                CancellationTokenSource ct = null;
                if (timeouts.TryGetValue(key, out ct)) {
                    ct.Cancel();
                }
                timeouts[key] = ct = new CancellationTokenSource();
                try
                {
                    await Task.Delay(timeout, ct.Token);
                    action();
                }
                catch (TaskCanceledException)
                {
                    return;
                }
                finally {
                    timeouts.Remove(key);
                }

                /*timeouts[key] = SetTimeout(() =>
                {
                    timeouts.Remove(key);
                    action();
                }, timeout);

                await Task.Delay(timeout);
                timeouts.Remove(key);*/

                //ct.Token.Register(() => {
                //    timeouts.TryRemove(key, out ct);
                //});

            });
        }


        //Dictionary<ActionKey, ActionKey> executionScope = new Dictionary<ActionKey, ActionKey>();
        //public void TriggerNonRecursive(Action action) {
        //    ActionKey key = new ActionKey(action);
        //    if (executionScope.ContainsKey(key))
        //        return;
        //    try {
        //        executionScope[key] = key;
        //        action();
        //    } finally {
        //        executionScope.Remove(key);
        //    }

        //}

        Dictionary<System.Reflection.MethodInfo, CancellationTokenSource> timeouts = new Dictionary<System.Reflection.MethodInfo, CancellationTokenSource>();

        //internal class ActionKey: IEquatable<ActionKey>
        //{
        //    private Action action;

        //    public ActionKey(Action action)
        //    {
        //        this.action = action;
        //    }

        //    public override int GetHashCode()
        //    {

        //        string key = (action.Target?.GetType()?.FullName ?? "") + "." + (action.Method.ToString());
        //        return key.GetHashCode();
        //    }

        //    public override bool Equals(object obj)
        //    {
        //        if (obj == null)
        //            return false;
        //        return Equals(obj as ActionKey);
        //    }

        //    public bool Equals(ActionKey other)
        //    {
        //        if (other == null)
        //            return false;
        //        if (action?.Method != other.action?.Method)
        //            return false;
        //        if (action?.Target != other.action?.Target)
        //            return false;
        //        return true;
        //    }
        //}


        //public void SyncCookies() {

        //    if (BaseUrl == null)
        //    {
        //        throw new InvalidOperationException("BaseUrl must be initialized before retriving CookieStore");
        //    }

        //    CookieStore.Clear();

        //    Uri uriBase = new Uri(BaseUrl);
        //    foreach (Cookie c in CookieContainer.GetCookies(uriBase))
        //    {
        //        CookieStore.Add(c);
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        public DirectoryInfo CacheDir { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DirectoryInfo DataDir { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string BaseUrl { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public Task InitAsync(UIAtomsConfig config)
        {
            var cacheDir = config.CacheDir;
            var dataDir = config.DataDir;
            BaseUrl = config.BaseUrl;
            CacheDir = new DirectoryInfo(cacheDir ?? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            DataDir = new DirectoryInfo(dataDir ?? Environment.GetFolderPath(Environment.SpecialFolder.Personal));

            InitPlatform();

            return Task.CompletedTask;

        }

        /// <summary>
        /// 
        /// </summary>
        public static Action<string> Logger { get; set; }

        partial void InitPlatform();


    }

    /// <summary>
    /// 
    /// </summary>
    public class UIAtomsConfig
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="licenseKey"></param>
        public UIAtomsConfig(string licenseKey = null)
        {
            //this.LicenseKey = licenseKey;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CacheDir { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DataDir { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class NavigationContainer : INavigation
    {
        INavigation inner
        {
            get
            {
                MasterDetailPage mdp = Application.Current.MainPage as MasterDetailPage;
                if (mdp != null)
                {
                    return mdp.Detail.Navigation;
                }
                return Application.Current.MainPage.Navigation;
            }
        }

        
        /// <summary>
        /// ModalStack will only display Popup Navigation Stacl 
        /// </summary>
        public IReadOnlyList<Page> ModalStack
        {
            get
            {
                return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<Page> NavigationStack
        {
            get
            {
                return inner.NavigationStack;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="before"></param>
        public void InsertPageBefore(Page page, Page before)
        {
            inner.InsertPageBefore(page, before);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<Page> PopAsync()
        {
            return inner.PopAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animated"></param>
        /// <returns></returns>
        public Task<Page> PopAsync(bool animated)
        {
            return inner.PopAsync(animated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<Page> PopModalAsync()
        {
            return PopModalAsync(true);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animated"></param>
        /// <returns></returns>
        public async Task<Page> PopModalAsync(bool animated)
        {
            var top = ModalStack.LastOrDefault();
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync(animated);
            return top;
            //return inner.PopModalAsync(animated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task PopToRootAsync()
        {
            return inner.PopToRootAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animated"></param>
        /// <returns></returns>
        public Task PopToRootAsync(bool animated)
        {
            return inner.PopToRootAsync(animated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task PushAsync(Page page)
        {
            return inner.PushAsync(page);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public Task PushAsync(Page page, bool animated)
        {
            return inner.PushAsync(page, animated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task PushModalAsync(Page page)
        {
            return PushModalAsync(page,true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public Task PushModalAsync(Page page, bool animated)
        {
            var p = page as Rg.Plugins.Popup.Pages.PopupPage;
            if (p != null)
            {
                return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(p, animated);
            }
            return inner.PushModalAsync(page, animated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public void RemovePage(Page page)
        {
            inner.RemovePage(page);
        }
    }



}
