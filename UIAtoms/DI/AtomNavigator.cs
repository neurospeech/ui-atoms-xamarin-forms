using NeuroSpeech.UIAtoms.Controls;
using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppNavigator))]


namespace NeuroSpeech.UIAtoms.DI
{
    public interface IAppNavigator
    {

        void Register<T>(string pageName = null, bool overwrite = false);

        Task NavigateAsync(string uri);
        Type GetPageType(string pageName, bool throwError = false);

        Task PushModalAsync<T>(object parameters = null, bool animate = true);
        Task PushAsync<T>(object parameters = null, bool animate = true);

        Task<Page> NewPage<T>(object parameters = null);
        Task<Page> NewPage(Type type, object parameters = null);


        Task PopModalAsync(bool animate = true);
        Task PopAsync(bool animate = true);
        Task PopToRootAsync(bool animate = true);


        Task<TResult> PushModalForResultAsync<T,TResult>(object parameters = null, bool animate = true);
        Task<TResult> PushForResultAsync<T, TResult>(object parameters = null, bool animate = true);



    }


    public class AppNavigator : IAppNavigator
    {

        public AppNavigator()
        {
            this.nav = DependencyService.Get<INavigation>(DependencyFetchTarget.GlobalInstance);
        }

        Dictionary<string, Type> _viewModelCache = new Dictionary<string, Type>();
        private INavigation nav;

        public async Task PushAsync<T>(object parameters, bool animate)
        {
            Page view = await NewPage<T>(parameters);
            await nav.PushAsync(view, animate);
        }

        public object CallToDependencyService(Type targetType)
        {
            MethodInfo method = typeof(DependencyService).GetTypeInfo().GetDeclaredMethod("Get");
            MethodInfo genericMethod = method.MakeGenericMethod(targetType);
            return genericMethod.Invoke(null, new object[] { DependencyFetchTarget.GlobalInstance });
        }

        private bool IsPage(Type type) {
            Type pageType = typeof(Page);
            if (pageType == type || type.IsSubclassOf(pageType))
                return true;
            pageType = typeof(AtomPopupPage);
            return pageType == type || type.IsSubclassOf(pageType);
        }

        public async Task<Page> NewPage(Type type, object parameters) 
        {
            Type pageType = type;
            Type viewModelType = null;

            if (!IsPage(type))
            {
                pageType = await GetRelatedType(type, type.Name.Substring(0, type.Name.Length - "ViewModel".Length));
                viewModelType = type;
            }


            object v = Activator.CreateInstance(pageType);

            Page view = v as Page;

            if (viewModelType == null)
            {
                viewModelType = await GetRelatedType(type, pageType.Name + "ViewModel");
            }

            if (viewModelType != null)
            {
                object model = NewModel(viewModelType);

                if (parameters != null)
                {
                    foreach (var p in parameters.GetType().GetProperties())
                    {
                        if (!p.CanRead)
                            continue;
                        object value = p.GetValue(parameters);
                        if (value == null)
                            continue;
                        var pm = viewModelType.GetProperty(p.Name);
                        if (pm == null)
                            continue;
                        if (pm.PropertyType != value.GetType())
                        {
                            Type n = Nullable.GetUnderlyingType(pm.PropertyType);
                            if(n==null || n != value.GetType())
                            {
                                value = Convert.ChangeType(value, pm.PropertyType);
                            }
                        }
                        pm.SetValue(model, value);
                    }
                }
                view.BindingContext = model;

                // check if viewmodel has IsBusy property...

                /*var atomViewModel = model as IAtomViewModel;
                if (atomViewModel != null) {
                    view.SetBinding(Page.IsBusyProperty, new Binding("IsBusy"));
                }*/

                var lifeCycleModel = model as IAtomViewLifeCycleModel;
                if (lifeCycleModel != null) {

                    view.Appearing += View_Appearing;
                    view.Disappearing += View_Disappearing;

                    //view.Appearing += (s, e) =>
                    //{
                    //    lifeCycleModel.OnAppearing();
                    //};
                    //view.Disappearing += async (s, e) =>
                    //{


                    //    lifeCycleModel.OnDisappearing();

                    //    await Task.Delay(100);

                    //    bool exists = !nav.NavigationStack.Contains(view) && !nav.ModalStack.Contains(view);
                    //    if (exists)
                    //    {
                    //        lifeCycleModel.OnRemoved();
                    //    }

                    //};
                }
            }

            // find coachmark....
            var ct = await GetRelatedType(pageType, "CoachMarks");
            if (ct != null) {
                AtomCoachMarks.SetCoachMark(view, ct);
            }

            return view;
        }

        private void View_Disappearing(object sender, EventArgs e)
        {
            Page view = sender as Page;
            if (view == null)
                return;
            var lifeCycleModel = view.BindingContext as IAtomViewLifeCycleModel;
            lifeCycleModel?.OnDisappearing();

            Device.BeginInvokeOnMainThread(async () => {
                try
                {
                    await Task.Delay(1000);
                    bool exists = !nav.NavigationStack.Contains(view) && !nav.ModalStack.Contains(view);
                    if (exists)
                    {
                        lifeCycleModel?.OnRemoved();

                        // also remove...
                        view.Appearing -= View_Appearing;
                        view.Disappearing -= View_Disappearing;
                        AtomCoachMarks.SetCoachMark(view, null);
                    }
                }
                catch (Exception ex) {
                    UIAtomsApplication.Instance.LogException?.Invoke(ex);
                }
            });
        }

        private void View_Appearing(object sender, EventArgs e)
        {
            Page view = sender as Page;
            if (view == null)
                return;
            var lifeCycleModel = view.BindingContext as IAtomViewLifeCycleModel;
            lifeCycleModel.OnAppearing();
        }

        private object NewModel(Type type)
        {
            var c = type.GetConstructors().FirstOrDefault(x => x.IsPublic);

            List<object> args = new List<object>();

            var pms = c.GetParameters();
            if (pms != null)
            {
                foreach (var p in pms)
                {
                    args.Add(CallToDependencyService(p.ParameterType));
                }
            }

            var model = c.Invoke(args.ToArray());
            return model;
        }

        public Task<Page> NewPage<T>(object parameters = null) {
            return NewPage(typeof(T), parameters);
        }

        private Task<Type> GetRelatedType(Type type, string fullTypeName)
        {
            return Task.Run(() =>
            {
                Type result = null;
                string vmName = fullTypeName;
                if (!_viewModelCache.TryGetValue(vmName, out result))
                {
                    var q = type.Assembly.GetTypes().Where(x => string.Equals(x.Name, vmName, StringComparison.OrdinalIgnoreCase));
                    if (q.Count() > 1)
                    {
                        throw new InvalidOperationException($"Type {vmName} is defined multiple times in assembly {type.Assembly.FullName}");
                    }
                    result = q.FirstOrDefault();
                    if (result == null)
                    {
                        //throw new InvalidOperationException($"Type {vmName} not found in assembly {type.Assembly.FullName}");
                        System.Diagnostics.Debug.WriteLine($"Type {vmName} not found in assembly {type.Assembly.FullName}");
                    }
                    _viewModelCache[vmName] = result;
                }
                return result;
            });
        }

        public async Task PushModalAsync<T>(object parameters, bool animated)
        {
            var page = await NewPage<T>(parameters);
            await nav.PushModalAsync(page, animated);
        }

        public async Task PopModalAsync(bool animated)
        {
            await nav.PopModalAsync(animated);
        }

        public async Task PopAsync(bool animated)
        {
            await nav.PopAsync(animated);
        }

        public async Task PopToRootAsync(bool animated)
        {
            await nav.PopToRootAsync(animated);
        }


        private Dictionary<Type, PropertyInfo> complectionSourceProperties 
            = new Dictionary<Type, PropertyInfo>();

        private async Task<TResult> InternalPushForResultAsync<T, TResult>(object parameters, bool animated, bool modal)
        {
            PageResult<TResult> source = Activator.CreateInstance<PageResult<TResult>>();

            source.Modal = modal;
            source.Animated = animated;

            var page = await NewPage<T>(parameters);

            var model = page.BindingContext as IPageResultViewModel<TResult>;
            if (model == null)
                throw new ArgumentNullException("View Model of Page cannot be null");

            Type sourceType = source.GetType();
            Type modelType = model.GetType();

            model.PageResult = source;

            if (modal)
            {
                page.Disappearing += OnForResultModalDisappearing<TResult>;
            }
            else {
                page.Disappearing += OnForResultDisappearing<TResult>;
            }

            if (nav == null)
                throw new ArgumentException("nav is null");

            if (modal)
            {
                await nav.PushModalAsync( page is AtomPopupPage ? page : new NavigationPage(page), animated);
            }
            else
            {
                await nav.PushAsync(page, animated);
            }

            try
            {
                return await source.Task;
            }
            catch (TaskCanceledException) {
                return default(TResult);
            }
            
        }

        private void OnForResultDisappearing<TResult>(object sender, EventArgs e)
        {
            var page = sender as Page;
            var model = page.BindingContext as IPageResultViewModel<TResult>;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var stack = nav.NavigationStack;
                await Task.Delay(1000);
                if (!stack.Contains(page))
                {
                    await model.PageResult.CancelAsync(false);
                    page.Disappearing -= OnForResultDisappearing<TResult>;
                }                
            });
        }

        private void OnForResultModalDisappearing<TResult>(object sender, EventArgs e)
        {
            var page = sender as Page;
            var model = page.BindingContext as IPageResultViewModel<TResult>;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var stack = nav.ModalStack;
                await Task.Delay(1000);
                if (!stack.Contains(page))
                {
                    await model.PageResult.CancelAsync(false);
                    page.Disappearing -= OnForResultModalDisappearing<TResult>;
                }
            });
        }

        public Task<TResult> PushModalForResultAsync<T, TResult>(object parameters = null, bool animated = true)
        {
            return InternalPushForResultAsync<T,TResult>(parameters, animated, true);
        }

        public Task<TResult> PushForResultAsync<T, TResult>(object parameters = null, bool animated = true)
        {
            return InternalPushForResultAsync<T, TResult>(parameters, animated, false);
        }

        Dictionary<string, Type> pages = new Dictionary<string, Type>();

        public void Register<T>(string pageName = null, bool overwrite = false)
        {

            if (pageName == null)
            {
                pageName = typeof(T).Name;
            }
            if (!overwrite) {
                if (pages.ContainsKey(pageName))
                    throw new ArgumentException($"Page {pageName} is already registered");
            }
            
            pages.Add(pageName, typeof(T));
        }

        public Type GetPageType(string pageName, bool throwError = false) {
            Type pageType = null;
            if (!pages.TryGetValue(pageName, out pageType)) {
                if (throwError) {
                    throw new ArgumentException($"No type registered for page {pageName}");
                }
            }
            return pageType;
        }
        

        public async Task NavigateAsync(string uri)
        {
            NavigationQueue queue = new NavigationQueue(uri);
            var top = queue.Top;
            if (top == null)
                throw new ArgumentNullException($"Cannot accept empty/null uri {uri}");

            

            top = queue.Take();

            Type pageType = GetPageType(top.Page, true);
            var mainPage = Application.Current.MainPage;
            if (mainPage != null) {
                if (mainPage.GetType() != pageType) {
                    mainPage = null;
                }
            }

            if (mainPage == null) {
                mainPage = await NewPage(pageType, null);
                Application.Current.MainPage = mainPage;
            }

            var model = (mainPage.BindingContext as IAtomNavigationAware) ?? (mainPage as IAtomNavigationAware);
            if (model != null) {
                await model.OnNavigatedAsync(top, queue);
            }
        }

    }

}
