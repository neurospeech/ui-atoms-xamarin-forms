using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    [ContentProperty("Children")]
    public class AtomFieldGroup :  BindableObject, IViewContainer<View>, INotifyCollectionChanged{


        /// <summary>
        /// 
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;


        #region Property Keywords

        /// <summary>
        /// Bindable Property Keywords
        /// </summary>
        public static readonly BindableProperty KeywordsProperty = BindableProperty.Create(
          "Keywords",
          typeof(string),
          typeof(AtomFieldGroup),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          // (sender,oldValue,newValue) => {}
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
        /// On Keywords changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnKeywordsChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Keywords
        /// </summary>
        public string Keywords
        {
            get
            {
                return (string)GetValue(KeywordsProperty);
            }
            set
            {
                SetValue(KeywordsProperty, value);
            }
        }
        #endregion

        #region Property Category

        /// <summary>
        /// Bindable Property Category
        /// </summary>
        public static readonly BindableProperty CategoryProperty = BindableProperty.Create(
          "Category",
          typeof(string),
          typeof(AtomFieldGroup),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          // (sender,oldValue,newValue) => {}
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
        /// On Category changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCategoryChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Category
        /// </summary>
        public string Category
        {
            get
            {
                return (string)GetValue(CategoryProperty);
            }
            set
            {
                SetValue(CategoryProperty, value);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<View> Items { get; }
            
        /// <summary>
        /// 
        /// </summary>
        public AtomFieldGroup()
        {
            this.Items = new AtomFieldCollection<View>(this);
            this.Items.CollectionChanged += Items_CollectionChanged;
        }

        

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        internal IEnumerable<View> Fields {
            get {
                foreach (var item in Items) {
                    if (!item.IsVisible)
                        continue;
                    AtomForm.SetCategory(item,this.Category);
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<View> Children => Items;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AtomFieldCollection<T> : ObservableCollection<T>
        where T:View
    {
        private readonly AtomFieldGroup Group;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public AtomFieldCollection(AtomFieldGroup g)
        {
            this.Group = g;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.PropertyChanged += Item_PropertyChanged;
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisible") {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            T item = this.ElementAtOrDefault(index);
            if (item != null) {
                item.PropertyChanged -= Item_PropertyChanged;
            }
            base.RemoveItem(index);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void ClearItems()
        {
            foreach (var item in this) {
                item.PropertyChanged -= Item_PropertyChanged;
            }
            base.ClearItems();
        }

    }


    //public class AtomConverterList<TSrc, TDest> : ObservableCollection<TSrc>
    //    where TDest : TSrc
    //    where TSrc: class
    //{
    //    private Func<TSrc, TDest> converter;

    //    public AtomConverterList(Func<TSrc,TDest> converter)
    //    {
    //        this.converter = converter;
    //    }

    //    protected override void InsertItem(int index, TSrc item)
    //    {
    //        if (!(item is TDest)) {
    //            item = converter(item);
    //        }

    //        base.InsertItem(index, item);


    //    }

    //}

}