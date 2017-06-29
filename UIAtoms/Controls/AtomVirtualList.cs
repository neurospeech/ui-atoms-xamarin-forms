using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomVirtualList : ScrollView{

        private StackLayout layout;

        private List<object> cachedItems = new List<object>();

        public AtomVirtualList()
        {

            Content = layout = new StackLayout { Orientation = StackOrientation.Vertical };
        }


        


        #region Property ItemsSource

        /// <summary>
        /// Bindable Property ItemsSource
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
          nameof(ItemsSource),
          typeof(System.Collections.IEnumerable),
          typeof(AtomVirtualList),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomVirtualList)sender).OnItemsSourceChanged(oldValue,newValue),
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

        /// <summary>
        /// On ItemsSource changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemsSourceChanged(object oldValue, object newValue)
        {
            INotifyCollectionChanged inc = oldValue as INotifyCollectionChanged;
            if (inc != null) {
                inc.CollectionChanged += OnItemsChanged;
            }
            inc = newValue as INotifyCollectionChanged;
            if (inc != null) {
                inc.CollectionChanged += OnItemsChanged;
            }
            OnItemsChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }


        /// <summary>
        /// Property ItemsSource
        /// </summary>
        public System.Collections.IEnumerable ItemsSource
        {
            get
            {
                return (System.Collections.IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        #endregion



        #region Property ItemTemplate

        /// <summary>
        /// Bindable Property ItemTemplate
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
          nameof(ItemTemplate),
          typeof(DataTemplate),
          typeof(AtomVirtualList),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomVirtualList)sender).OnItemTemplateChanged(oldValue,newValue),
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
        /// On ItemTemplate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemTemplateChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemTemplate
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }
        #endregion




        protected virtual void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            // recreate items...

            var items = ItemsSource;
            cachedItems.Clear();
            if (items != null) {
                foreach (var item in items)
                {
                    cachedItems.Add(item);
                }
            }

            learning = true;

            pendingItems = cachedItems.GetEnumerator();

            Recreate();

            
        }

        IEnumerator<object> pendingItems;

        bool learning = false;

        double avgHeight = 0;

        private Queue<View> cache = new Queue<View>();

        private void Recreate()
        {
            if (Height <= 0) {
                UIAtomsApplication.Instance.SetTimeout(Recreate, TimeSpan.FromMilliseconds(100));
                return;
            }

            var padding = layout.Padding;

            if (learning) {
                // learning mode....
                if (Height < layout.Height)
                {
                    learning = false;
                    avgHeight = 0;
                    foreach (var view in layout.Children)
                    {
                        avgHeight += view.Height;
                    }
                    avgHeight /= layout.Children.Count;
                    Device.BeginInvokeOnMainThread(Recreate);
                    pendingItems = null;

                    double desiredHeight = avgHeight * cachedItems.Count;

                    padding.Bottom = desiredHeight - Height;

                    layout.Padding = padding;
                }
                else {
                    if (pendingItems.MoveNext()) {
                        var view = CreateNewItem(pendingItems.Current);
                        
                        layout.Children.Add(view);
                        Device.BeginInvokeOnMainThread(Recreate);
                    }
                }
                return;
            }



        }

        private View CreateNewItem(object data)
        {
            View item = ItemTemplate.CreateContent() as View;
            if (item == null)
            {
                throw new ArgumentException($"{nameof(ItemTemplate)} must contain child as View and not Cell");
            }
            item.BindingContext = data;
            return item;
        }

        private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            Recreate();
        }
    }


    public enum LayoutType {
        Vertical,
        Horizontal,
        Wrap
    }

    internal class ListItem {

        //internal int Index;

        //internal object Data;


        // work in progress...
        //internal object Header;

        

    }
}
