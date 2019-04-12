using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NeuroSpeech.UIAtoms.DI;
using Xamarin.Forms;
using System.Threading;

namespace NeuroSpeech.UIAtoms
{

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =true, Inherited =true)]
    public class AtomDependsOnAttribute : Attribute {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public AtomDependsOnAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }
    }

    internal class AtomSourceProperty: Dictionary<string,List<string>> {

        private static ConcurrentDictionary<Type, AtomSourceProperty> sources
            = new ConcurrentDictionary<Type, AtomSourceProperty>();
        

        internal AtomSourceProperty(Type type)
        {

            var list = type.GetProperties().SelectMany(x => 
                x.GetCustomAttributes<AtomDependsOnAttribute>()
                    .Select(a=> new { a.Name, DependsOn = x.Name })
            ).GroupBy(x=>x.DependsOn);

            foreach (var a in list) {
                this[a.Key] = a.Select(x=>x.Name).ToList();
            }
  
        }

        internal static AtomSourceProperty Get(Type type) {
            return sources.GetOrAdd(type, t => {

                var p = new AtomSourceProperty(t);
                if (p.Any())
                    return p;

                return null;
            });
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class AtomModel : INotifyPropertyChanged
    {

        /// <summary>
        /// 
        /// </summary>
        public AtomModel()
        {
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
        protected bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged implementation
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        private bool preventRecursive = false;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (preventRecursive)
            {
                System.Diagnostics.Debug.WriteLine("WARNING!!!! You should avoid recursive OnPropertyChanged method calls");
                return;
            }
            try
            {
                preventRecursive = true;
                var changed = PropertyChanged;
                if (changed == null)
                    return;


                changed(this, new PropertyChangedEventArgs(propertyName));


                var source = AtomSourceProperty.Get(GetType());
                if (source != null)
                {
                    List<string> dependents = null;
                    if (source.TryGetValue(propertyName, out dependents))
                    {
                        foreach (var n in dependents)
                        {
                            changed(this, new PropertyChangedEventArgs(n));
                        }
                    }
                }
            }
            finally {
                preventRecursive = false;
            }

        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class AtomViewModel : AtomModel, 
        IAtomNavigationAware, 
        IAtomViewLifeCycleModel
    {


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T Get<T>()
            where T : class
        {
            return DependencyService.Get<T>(DependencyFetchTarget.GlobalInstance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T New<T>()
            where T : class
        {
            return DependencyService.Get<T>(DependencyFetchTarget.NewInstance);
        }

        private List<Action> onAppearing = null;
        private List<Action> onDisappearing = null;
        private List<Action> onDisposing = null;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="action"></param>
        /// <param name="onUnsubscribe"></param>
        protected void OnBackgroundMessage<T>(string message, Action<T> action, Action onUnsubscribe = null)
            where T:class
        {
            if (onDisposing == null) {
                onDisposing = new List<Action>();
            }

            MessagingCenter.Subscribe<T>(this, message, s => {
                Device.BeginInvokeOnMainThread(() => {
                    try {
                        action(s);
                    } catch (Exception ex) {
                        UIAtomsApplication.Instance.LogException?.Invoke(ex);
                    }
                });
            });

            onDisposing.Add(onUnsubscribe ?? (() => {
                MessagingCenter.Unsubscribe<T>(this, message);
            }));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="action"></param>
        /// <param name="onUnsubscribe"></param>
        protected void OnBackgroundMessageWithArgs<T, TArgs>(string message, Action<T,TArgs> action, Action onUnsubscribe = null)
            where T : class
        {
            if (onDisposing == null)
            {
                onDisposing = new List<Action>();
            }

            MessagingCenter.Subscribe<T,TArgs>(this, message, (s,a) => {
                Device.BeginInvokeOnMainThread(() => {
                    try
                    {
                        action(s, a);
                    }
                    catch (Exception ex)
                    {
                        UIAtomsApplication.Instance.LogException?.Invoke(ex);
                    }
                });
            });

            onDisposing.Add(onUnsubscribe ?? (() => {
                MessagingCenter.Unsubscribe<T>(this, message);
            }));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="action"></param>
        /// <param name="onUnsubscribe"></param>
        protected void OnMessage<T>(string message, Action<T> action, Action onUnsubscribe = null)
            where T : class
        {
            if (onAppearing == null)
                onAppearing = new List<Action>();
            if (onDisappearing == null)
                onDisappearing = new List<Action>();
            onAppearing.Add(() => {
                MessagingCenter.Subscribe<T>(this, message,
                    a => Device.BeginInvokeOnMainThread(() => {
                        try
                        {
                            action(a);
                        }
                        catch (Exception ex) {
                            UIAtomsApplication.Instance.LogException?.Invoke(ex);
                        }
                    }));
            });

            onDisappearing.Add(onUnsubscribe ?? ( () => {
                MessagingCenter.Unsubscribe<T>(this, message);
            }));

        }

        /// <summary>
        /// 
        /// </summary>
        protected bool AutoRefreshOnAppear = false;

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnAppearing()
        {
            if (AutoRefreshOnAppear) {
                AutoRefreshOnAppear = false;

                Reload();
            }
            if (onAppearing != null)
            {
                foreach (var a in onAppearing)
                {
                    a();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnDisappearing()
        {
            if (onDisappearing != null)
            {
                foreach (var a in onDisappearing)
                {
                    a();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected readonly IAppNavigator appNavigator;

        /// <summary>
        /// 
        /// </summary>
        protected readonly INotificationService notificationService;

        /// <summary>
        /// 
        /// </summary>
        public AtomViewModel()
        {
            appNavigator = Get<IAppNavigator>();
            notificationService = Get<INotificationService>();
            Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual Task Reload()
        {
            UIAtomsApplication.Instance.TriggerOnce(async () =>
            {
                try
                {
                    await InitAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);                    
                }
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Task InitAsync() {
            return Task.CompletedTask;
        }



        Task IAtomNavigationAware.OnNavigatedAsync(NavigationItem current, NavigationQueue queue)
        {
            return OnNavigationAsync(current, queue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="queue"></param>
        /// <returns></returns>
        protected virtual Task OnNavigationAsync(NavigationItem current, NavigationQueue queue)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnRemoved()
        {
            if (onDisposing != null)
            {
                foreach (var a in onDisposing)
                {
                    a();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AtomListViewModel<T> : AtomViewModel {

        /// <summary>
        /// 
        /// </summary>
        public AtomList<T> Items { get; }
            = new AtomList<T>();

        /// <summary>
        /// 
        /// </summary>
        public ICommand RefreshCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand SelectCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public AtomCommand<IEnumerable<T>> DeleteCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public AtomCommand OverScrollCommand { get; }

        #region Property IsRefreshing

        private bool _IsRefreshing = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsRefreshing
        {
            get
            {
                return _IsRefreshing;
            }
            private set
            {
                SetProperty(ref _IsRefreshing, value);
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public AtomListViewModel()
        {

            RefreshCommand = new AtomCommand(Reload);

            SelectCommand = new AtomCommand<T>(async (item) =>
            {
                try {
                    await OnSelectCommandAsync(item);
                }
                catch (Exception ex)
                {
                    UIAtomsApplication.Instance.LogException?.Invoke(ex);
                }
            });

            DeleteCommand = new AtomCommand<IEnumerable<T>>(async (items) => await OnDeleteCommandAsync(items));

            OverScrollCommand = new AtomCommand(async () => {
                try
                {
                    await OnLoad();
                }
                catch (Exception ex)
                {
                    UIAtomsApplication.Instance.LogException?.Invoke(ex);
                }
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        protected virtual async Task OnDeleteCommandAsync(IEnumerable<T> items)
        {
            if (!items.Any())
            {
                await Application.Current.MainPage.DisplayAlert("Delete", "Please select items to delete", "Cancel");
                return;
            }

            await OnDeleteCommandAsync(items);
            foreach (var item in items)
            {
                Items.Remove(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual Task OnSelectCommandAsync(T item)
        {

            System.Diagnostics.Debug.WriteLine($"Warning {this.GetType().FullName}.OnSelectCommandAsync not overriden");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Refreshes entire list
        /// </summary>
        public void RefreshItems()
        {
            Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnRemoved()
        {
            base.OnRemoved();
            AutoRefreshOnAppear = true;
            Items.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Task InitAsync()
        {
            return OnLoad(true);
        }


        //private CancellationTokenSource cancellationToken = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        protected virtual async Task OnLoad(bool reset = false) {

            try
            {
                CancellationToken ct = new CancellationToken(false);
                IsRefreshing = true;
                IEnumerable<T> items = await OnLoadItemsAsync(reset ? 0 : Items.Count,ct);

                if (items != null)
                {
                    if (reset || Items.Count == 0)
                    {
                        Items.Replace(items);
                    }
                    else
                    {
                        Items.Merge(items);
                    }
                }
            }
            finally {
                IsRefreshing = false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        protected abstract Task<IEnumerable<T>> OnLoadItemsAsync(int start, CancellationToken token);
    }
}
