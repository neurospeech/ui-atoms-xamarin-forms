using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;

namespace NeuroSpeech.UIAtoms.Controls.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public interface IOverScrollView
    {

        /// <summary>
        /// 
        /// </summary>
        event EventHandler OverScrolled;

        /// <summary>
        /// 
        /// </summary>
        void InvokeOverScrolled();

        /// <summary>
        /// 
        /// </summary>
        ICommand OverScrollCommand { get; }

    }

    

    public class ScrollableListView : ListView, IOverScrollView
    {

        #region Property OverScrollCommand

        /// <summary>
        /// Bindable Property OverScrollCommand
        /// </summary>
        public static readonly BindableProperty OverScrollCommandProperty = BindableProperty.Create(
          "OverScrollCommand",
          typeof(ICommand),
          typeof(ScrollableListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((ScrollableListView)sender).OnOverScrollCommandChanged(oldValue,newValue),
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
        /// On OverScrollCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnOverScrollCommandChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property OverScrollCommand
        /// </summary>
        public ICommand OverScrollCommand
        {
            get
            {
                return (ICommand)GetValue(OverScrollCommandProperty);
            }
            set
            {
                SetValue(OverScrollCommandProperty, value);
            }
        }
        #endregion

        internal Action<Cell,int> OnHookContent;
        internal Action<Cell> OnUnhookContent;

        protected override void SetupContent(Cell content, int index)
        {
            base.SetupContent(content, index);
            OnHookContent?.Invoke(content, index);
        }

        protected override void UnhookContent(Cell content)
        {
            base.UnhookContent(content);
            OnUnhookContent?.Invoke(content);
        }


        private object LastItem = null;

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

            if (propertyName == ItemsSourceProperty.PropertyName) {
                INotifyCollectionChanged inc = ItemsSource as INotifyCollectionChanged;
                if (inc != null) {
                    inc.CollectionChanged -= OnCollectionChanged;
                }
                OnCollectionChanged(this, null);
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                INotifyCollectionChanged inc = ItemsSource as INotifyCollectionChanged;
                if (inc != null)
                {
                    inc.CollectionChanged += OnCollectionChanged;
                }
                OnCollectionChanged(this, null);
            }
        }

        private bool fireOverScroll = false;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            fireOverScroll = false;

            UIAtomsApplication.Instance.TriggerOnce(EnableFireOverScroll);
        }

        private void EnableFireOverScroll()
        {
            UIAtomsApplication.Instance.SetTimeout(() =>
            {
                fireOverScroll = true;
            }, TimeSpan.FromMilliseconds(100));
        }

        /// <summary>
        /// 
        /// </summary>
        public ScrollableListView():base(
#if __DROID__
            (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.N ? ListViewCachingStrategy.RecycleElement : ListViewCachingStrategy.RetainElement)
#else
            ListViewCachingStrategy.RecycleElement
#endif
        )
        {
            SeparatorColor = Color.FromHex("#eee");
            SeparatorVisibility = SeparatorVisibility.Default;

            //this.

            this.ItemAppearing += (s, e) =>
            {

                var currentItem = e.Item;
                object last = GetLastItem();
                if (last == null)
                    return;

                if (last == currentItem)
                {

                    if (last != LastItem)
                    {
                        LastItem = last;

                        if (fireOverScroll)
                        {
                            InvokeOverScrolled();
                        }
                    }
                }
                
            };

            //this.SetBinding(OverScrollCommandProperty, new Binding { Path = "OverScrollCommand" });

            
        }

        private object GetLastItem()
        {

            if (this.IsGroupingEnabled) {
                var items = ItemsSource?.Cast<object>();
                var last = items.LastOrDefault();
                if (last == null)
                    return null;
                return (last as IEnumerable<object>).LastOrDefault();
            }

            return ItemsSource?.Cast<object>()?.LastOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OverScrolled;


        /// <summary>
        /// 
        /// </summary>
        public void InvokeOverScrolled()
        {
            UIAtomsApplication.Instance.TriggerOnce(() => {

                OverScrolled?.Invoke(this, EventArgs.Empty);
                OverScrollCommand?.Execute(EventArgs.Empty);

            }, TimeSpan.FromMilliseconds(700));
        }
    }
}
