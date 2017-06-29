using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using NeuroSpeech.UIAtoms.DI;
using System.Linq;
using NeuroSpeech.UIAtoms.Services;
using System.Threading;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomCoachMarks
    {

        public static AtomPreferences Preferences {
            get {
                return AtomPreferences.GetPreferences("CoachMarks");
            }
        }

        public static bool ShowCoachMarks {
            get {
                return Preferences.GetValue("Show", true);
            }
            set {
                Preferences.SetValue("Show", value);
            }
        }

        private static string GetShowKey(Page page) {
            return $"Displayed-{page.GetType().FullName}";
        }

        public static bool IsDisplayed(Page page) {
            return Preferences.GetValue(GetShowKey(page), false);
        }

        public static void SetIsDisplayed(Page page, bool v)
        {
            Preferences.SetValue(GetShowKey(page), v);
        }

        #region CoachMark Attached Property
        /// <summary>
        /// CoachMark Attached property
        /// </summary>
        public static readonly BindableProperty CoachMarkProperty =
            BindableProperty.CreateAttached("CoachMark", typeof(object),
            typeof(AtomCoachMarks),
            null,
            BindingMode.OneWay,
            null,
            OnCoachMarkChanged);

        private static void OnCoachMarkChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Page page = bindable as Page;
            if (page != null && newValue != null)
            {

                var nt = newValue as Type;
                if (nt != null)
                {
                    if (!typeof(AtomCoachMarkPage).IsAssignableFrom(nt))
                    {
                        throw new InvalidOperationException($"{nt.FullName} should be derived from AtomCoachMarkPage");
                    }
                }
                else {
                    var dt = newValue as DataTemplate;
                    if (dt == null) {
                        throw new InvalidOperationException($"Coachmark for page {page.GetType().FullName} should either be DataTemplate of AtomCoachMarkPage or derived from AtomCoachMarkPage");
                    }
                }

                page.Appearing += Page_Appearing;
            }
        }

        private static void Page_Appearing(object sender, EventArgs e)
        {
            Page page = sender as Page;
            page.Appearing -= Page_Appearing;

            //SetCoachMark(page, null);


            if (!ShowCoachMarks)
                return;

            if (IsDisplayed(page))
                return;


            Device.BeginInvokeOnMainThread(async () => await OnShowPopup(page));

        }

        private static async Task OnShowPopup(Page page)
        {

            var dt = GetCoachMark(page);
            if (dt == null) {
                return;
            }


            SetCoachMark(page, null);

            var nav = DependencyService.Get<INavigation>();

            AtomCoachMarkPage coachMark = CreatePage(dt);
            if (coachMark == null) {
                string error = "Coachmark should be derived from AtomCoachMarkPage";
                UIAtomsApplication.Instance.LogException?.Invoke(new InvalidOperationException(error) { });
                await DependencyService.Get<INotificationService>().NotifyAsync(error);
                return;
            }

            await nav.PushModalAsync(coachMark);

            SetIsDisplayed(page, true);

        }

        private static AtomCoachMarkPage CreatePage(object t)
        {
            AtomCoachMarkPage result = null;
            var dt = t as DataTemplate;
            if (dt != null)
            {
                result = dt.CreateContent() as AtomCoachMarkPage;
            }
            else {
                var type = t as Type;
                result = Activator.CreateInstance(type) as AtomCoachMarkPage;
            }

            if (result == null)
                throw new InvalidOperationException($"Unable to create AtomCoachMarkPage from object {t}");

            return result;
        }

        /// <summary>
        /// Set CoachMark for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetCoachMark(BindableObject bindable, object newValue)
        {
            bindable.SetValue(CoachMarkProperty, newValue);
        }

        /// <summary>
        /// Get CoachMark for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static object GetCoachMark(BindableObject bindable)
        {
            return (object)bindable.GetValue(CoachMarkProperty);
        }
        #endregion



    }

    [ContentProperty(nameof(CoachMarks))]
    public class AtomCoachMarkPage : Rg.Plugins.Popup.Pages.PopupPage {

        ContentView contentHolder = new ContentView();
        private TextToSpeechService tts;
        private CancellationTokenSource cancel;

        public AtomCoachMarkPage()
        {
            Content = contentHolder;

            contentHolder.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2,
                Command = new AtomCommand(async () => await OnDoubleTapped())
            });
            contentHolder.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new AtomCommand(async () => await OnTapped(true))
            });
        }

        private async Task OnDoubleTapped()
        {
            try
            {
                cancel?.Cancel();
                await DependencyService.Get<INavigation>().PopModalAsync();
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.Fail(ex.Message, ex.ToString());
            }
        }

        private async Task OnTapped(bool userTapped = false)
        {
            if (userTapped)
            {
                cancel?.Cancel();
                return;
            }
            cancel = null;

            var first = CoachMarks.FirstOrDefault();
            if (first == null) {
                await OnDoubleTapped();
                return;
            }

            CoachMarks.Remove(first);

            contentHolder.Content = first;

            cancel = new CancellationTokenSource();

            // speak.....
            try
            {
                await tts.Speak(first.Text, cancel.Token);
            }
            catch (TaskCanceledException)
            {

            }
            cancel = null;

            //await Task.Delay(1000);

            Device.BeginInvokeOnMainThread(async () => await OnTapped());

        }

        public List<AtomCoachMarkContent> CoachMarks { get; private set; }
            = new List<AtomCoachMarkContent>();



        protected async override Task OnAppearingAnimationEnd()
        {
            await base.OnAppearingAnimationEnd();

            if (tts != null)
                return;
            tts = DependencyService.Get<TextToSpeechService>(DependencyFetchTarget.NewInstance);
            await OnTapped();
        }

        protected override void OnDisappearing()
        {
            tts?.Dispose();
            tts = null;
            base.OnDisappearing();
        }

    }

    public class AtomCoachMarkContent : ContentView {



        #region Property Text

        /// <summary>
        /// Bindable Property Text
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
          nameof(Text),
          typeof(string),
          typeof(AtomCoachMarkContent),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCoachMarkContent)sender).OnTextChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          // () => Default(T)
          null
        );

        /*
        /// <summary>
        /// On Text changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTextChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Text
        /// </summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        #endregion



    }

    public class AtomCoachMarkImage : AtomCoachMarkContent {
        public AtomCoachMarkImage()
        {
            Content = new AtomImage();

            Content.SetBinding(AtomImage.SourceProperty, new Binding {
                Path = "Source",
                Source = this
            });
        }



        #region Property Source

        /// <summary>
        /// Bindable Property Source
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
          nameof(Source),
          typeof(string),
          typeof(AtomCoachMarkImage),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCoachMarkImage)sender).OnSourceChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          // () => Default(T)
          null
        );

        /*
        /// <summary>
        /// On Source changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSourceChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Source
        /// </summary>
        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        #endregion


    }

    public class AsyncDictionary<TKey, TValue> {

        private Dictionary<TKey, TValue> values =
            new Dictionary<TKey, TValue>();

        /*public Task<TValue> GetOrAddAsync(TKey key, TValue value) {
            return GetOrAddAsync(key, k => value);
        }*/

        public Task<TValue> GetOrAddAsync(TKey key, Func<TKey,TValue> func = null) {
            return Task.Run(()=> {
                TValue value = default(TValue);
                lock (values) {
                    if (!values.TryGetValue(key, out value)) {
                        if (func != null) {
                            value = func(key);
                            values[key] = value;
                        }
                    }
                }
                return value;
            });
        }

        public Task RemoveAsync(TKey key) {
            return Task.Run(()=> {
                lock (values)
                {
                    values.Remove(key);
                }
            });
        }

        public Task ClearAsync(Action<KeyValuePair<TKey, TValue>> clearAction = null) {
            return Task.Run(()=> {
                lock (values) {
                    if (clearAction != null)
                    {
                        foreach (var kp in values)
                        {
                            clearAction(kp);
                        }
                    }
                    values.Clear();
                }
            });
        }
    }
}
