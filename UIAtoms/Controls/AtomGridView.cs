using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomGridView : AtomListView 
    {

        #region Property ItemWidth

        /// <summary>
        /// Bindable Property ItemWidth
        /// </summary>
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(
          "ItemWidth",
          typeof(double),
          typeof(AtomGridView),
          150.0,
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
        /// On ItemWidth changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemWidthChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemWidth
        /// </summary>
        public double ItemWidth
        {
            get
            {
                return (double)GetValue(ItemWidthProperty);
            }
            set
            {
                SetValue(ItemWidthProperty, value);
            }
        }
        #endregion

        #region Property ItemHeight

        /// <summary>
        /// Bindable Property ItemHeight
        /// </summary>
        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(
          "ItemHeight",
          typeof(double),
          typeof(AtomGridView),
          150.0,
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
        /// On ItemHeight changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemHeightChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemHeight
        /// </summary>
        public double ItemHeight
        {
            get
            {
                return (double)GetValue(ItemHeightProperty);
            }
            set
            {
                SetValue(ItemHeightProperty, value);
            }
        }
        #endregion


        #region Property ColumnSpacing

        /// <summary>
        /// Bindable Property ColumnSpacing
        /// </summary>
        public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(
          "ColumnSpacing",
          typeof(double),
          typeof(AtomGridView),
          5.0,
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
        /// On ColumnSpacing changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnColumnSpacingChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ColumnSpacing
        /// </summary>
        public double ColumnSpacing
        {
            get
            {
                return (double)GetValue(ColumnSpacingProperty);
            }
            set
            {
                SetValue(ColumnSpacingProperty, value);
            }
        }
        #endregion




        protected override void SetItemTemplate(DataTemplate newValue)
        {
            listView.ItemTemplate = 
                new DataTemplate(() => {
                    return new ViewCell {
                        Height = ItemHeight,
                        View = new GridCell(Slices, new DataTemplate(()=> {

                            var content = newValue.CreateContent();

                            var itemStyle = (AtomListViewCell)(ItemStyleTemplate?.CreateContent() ?? new AtomListViewCell());

                            itemStyle.Content = (content as View) ?? (content as ViewCell).View;
                            var c = itemStyle;
                            return c;

                        }), this)
                    };
                })
            ;
        }











        public AtomGridView()
        {



        }

        internal int Slices;

        protected override void SetItemsSource(IEnumerable newValue)
        {
            if (Width <= 0) {
                Device.BeginInvokeOnMainThread(async () => { await Task.Delay(500); SetItemsSource(newValue); });
                return;
            }

            if (newValue == null) {
                base.SetItemsSource(null);
                return;
            }

            var spacing = (int)ColumnSpacing;
            var width = (int)Width;
            var itemWidth = (int)ItemWidth;

            listView.RowHeight = -1;
            listView.HasUnevenRows = true;

            int noOfColumns = width / (itemWidth + spacing);
            // If possible add another row without spacing (because the number of columns will be one less than the number of spacings)
            if (width - (noOfColumns * (itemWidth + spacing)) >= itemWidth)
                noOfColumns++;

            Slices = noOfColumns;

            HasUnevenRows = true;

            if (IsGroupingEnabled)
            {
                var ig = newValue as IEnumerable<IGrouping<object, object>>;
                if (ig != null)
                {
                    newValue = ig.Slice(noOfColumns);
                    GroupDisplayBinding = new Binding { Path = "Key" };
                }
                else {
                    throw new InvalidOperationException("When grouping is enabled, please use IEnumerable<IGrouping<TKey,T>> as ItemsSource");
                }
            }
            else {
                var ig = newValue as IEnumerable<object>;
                newValue = ig.Slice(noOfColumns);
            }

            base.SetItemsSource(newValue);
            
        }

    }


    public class GridCell : Grid, IAtomListItem
    {



        public GridCell(int slices, DataTemplate dt, AtomGridView gridView)
        {
            this.Slices = slices;
            this.GridView = gridView;
            this.DataTemplate = dt;
        }

        private void Create()
        {

            this.HeightRequest = GridView.ItemHeight;

            var items = BindingContext as IEnumerable<object>;
            int n = items != null ? items.Count() : 0;
            for (int i = 0; i < Slices; i++)
            {

                object dataItem = null;
                if (i < n)
                {
                    dataItem = items.ElementAt(i);
                }

                View view = null;
                if (i >= Children.Count)
                {
                    view = DataTemplate.CreateContent() as View;
                    if (view == null)
                    {
                        throw new InvalidOperationException("DataTemplate of Grid must not be of type Cell");
                    }
                    view.BindingContext = dataItem;
                    ColumnDefinitions.Add(new ColumnDefinition());
                    Children.Add(view);
                    Grid.SetColumn(view, i);
                }
                else
                {
                    view = Children[i];
                    view.BindingContext = dataItem;
                    view.IsVisible = dataItem != null;
                }

            }

            int gridChildren = Children.Count;
            for (int i = n; i < gridChildren; i++)
            {
                Children[i].BindingContext = null;
                Children[i].IsVisible = false;
            }
        }

        public DataTemplate DataTemplate { get; private set; }
        public AtomGridView GridView { get; private set; }
        public int Slices { get; private set; }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Create();
        }

        void IAtomListItem.OnAppearing(AtomListView owner)
        {
            foreach (View child in Children) {
                if (child.BindingContext!=null) {
                    (child as IAtomListItem)?.OnAppearing(owner);
                }
            }
        }

        void IAtomListItem.OnDisappearing(AtomListView owner)
        {
            foreach (View child in Children)
            {
                if (child.BindingContext != null)
                {
                    (child as IAtomListItem)?.OnDisappearing(owner);
                }
            }
        }
    }

}
