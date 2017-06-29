using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomRepeator: ContentView
    {

        WrapLayout layout = new WrapLayout();

        public AtomRepeator()
        {
            Content = layout;

            layout.SetBinding(WrapLayout.SpacingProperty, new Binding() { Path = "ItemGap", Source = this });
        }

        #region Property ItemWidth

        /// <summary>
        /// Bindable Property ItemWidth
        /// </summary>
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(
          "ItemWidth",
          typeof(double),
          typeof(AtomRepeator),
          (double)150.0,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomRepeator)sender).OnItemWidthChanged(oldValue,newValue),
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
          typeof(AtomRepeator),
          (double)150.0,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomRepeator)sender).OnItemHeightChanged(oldValue,newValue),
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

        #region Property ItemsSource

        /// <summary>
        /// Bindable Property ItemsSource
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
          "ItemsSource",
          typeof(System.Collections.IEnumerable),
          typeof(AtomRepeator),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomRepeator)sender).OnItemsSourceChanged(oldValue,newValue),
          
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
            var ice = (oldValue as INotifyCollectionChanged);
            if (ice != null) {
                ice.CollectionChanged -= OnCollectionChanged;
            }
            ice = (newValue as INotifyCollectionChanged);
            if (ice != null) {
                ice.CollectionChanged += OnCollectionChanged;
            }
            Recreate();
        }

        protected virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            UIAtomsApplication.Instance.TriggerOnce(Recreate);
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
          "ItemTemplate",
          typeof(DataTemplate),
          typeof(AtomRepeator),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomRepeator)sender).OnItemTemplateChanged(oldValue,newValue),
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

        #region Property ItemGap

        /// <summary>
        /// Bindable Property ItemGap
        /// </summary>
        public static readonly BindableProperty ItemGapProperty = BindableProperty.Create(
          "ItemGap",
          typeof(double),
          typeof(AtomRepeator),
          (double)3.0,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomRepeator)sender).OnItemGapChanged(oldValue,newValue),
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
        /// On ItemGap changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemGapChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemGap
        /// </summary>
        public double ItemGap
        {
            get
            {
                return (double)GetValue(ItemGapProperty);
            }
            set
            {
                SetValue(ItemGapProperty, value);
            }
        }
        #endregion

        #region Property SelectionPath

        /// <summary>
        /// Bindable Property SelectionPath
        /// </summary>
        public static readonly BindableProperty SelectionPathProperty = BindableProperty.Create(
          "SelectionPath",
          typeof(string),
          typeof(AtomRepeator),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomRepeator)sender).OnSelectionPathChanged(oldValue,newValue),
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
        /// On SelectionPath changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSelectionPathChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property SelectionPath
        /// </summary>
        public string SelectionPath
        {
            get
            {
                return (string)GetValue(SelectionPathProperty);
            }
            set
            {
                SetValue(SelectionPathProperty, value);
            }
        }
        #endregion

        #region Property SelectCommand

        /// <summary>
        /// Bindable Property SelectCommand
        /// </summary>
        public static readonly BindableProperty SelectCommandProperty = BindableProperty.Create(
          "SelectCommand",
          typeof(System.Windows.Input.ICommand),
          typeof(AtomRepeator),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomRepeator)sender).OnSelectCommandChanged(oldValue,newValue),
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
        /// On SelectCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSelectCommandChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property SelectCommand
        /// </summary>
        public System.Windows.Input.ICommand SelectCommand
        {
            get
            {
                return (System.Windows.Input.ICommand)GetValue(SelectCommandProperty);
            }
            set
            {
                SetValue(SelectCommandProperty, value);
            }
        }
        #endregion

        #region Property DeselectCommand

        /// <summary>
        /// Bindable Property DeselectCommand
        /// </summary>
        public static readonly BindableProperty DeselectCommandProperty = BindableProperty.Create(
          "DeselectCommand",
          typeof(System.Windows.Input.ICommand),
          typeof(AtomRepeator),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomRepeator)sender).OnDeselectCommandChanged(oldValue,newValue),
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
        /// On DeselectCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnDeselectCommandChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property DeselectCommand
        /// </summary>
        public System.Windows.Input.ICommand DeselectCommand
        {
            get
            {
                return (System.Windows.Input.ICommand)GetValue(DeselectCommandProperty);
            }
            set
            {
                SetValue(DeselectCommandProperty, value);
            }
        }
        #endregion




        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName) {
                case "ItemsSource":
                case "ItemTemplate":
                    Recreate();
                    break;
            }

        }

        private void Recreate()
        {

            if (ItemsSource == null || ItemTemplate == null)
                return;

            var children = this.layout.Children;
            this.layout.Spacing = ItemGap;
            children.Clear();
            var template = ItemTemplate;
            foreach (var item in ItemsSource)
            {
                View view = template.CreateContent() as View;
                if (view == null) {
                    throw new ArgumentException("ItemTemplate must be of type View and not Cell");
                }
                view.BindingContext = item;
                view.SetBinding(View.WidthRequestProperty, new Binding { Path = "ItemWidth", Source = this });
                view.SetBinding(View.HeightRequestProperty, new Binding { Path = "ItemHeight", Source = this });
                view.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new AtomCommand(c => OnItemTapped(item))
                });
                children.Add(view);
            }



            //if (Width <= 0) {
            //    UIAtomsApplication.Instance.SetTimeout(Recreate, TimeSpan.FromMilliseconds(100));
            //    return;
            //}

            //if (ItemTemplate == null || ItemsSource==null) {
            //    return;
            //}

            //this.Children.Clear();

            //double gap = ItemGap;

            //double x = gap;
            //double y = gap;

            //double width = ItemWidth;
            //double height = ItemHeight;



            //if (width <= 0) {
            //    width = Width;
            //    width -= 2 * gap;
            //}

            //double totalWidth = width + 2 * gap;

            //var template = ItemTemplate;

            //var rect = new Rectangle(gap, gap, width, height);

            //foreach (var item in ItemsSource) {

            //    View view = template.CreateContent() as View;
            //    view.BindingContext = item;

            //    AbsoluteLayout.SetLayoutBounds(view, rect);
            //    this.Children.Add(view);

            //    if (rect.Width + totalWidth + 2 * gap > Width)
            //    {
            //        // goto next line..
            //        rect.X = gap;
            //        rect.Y += height + 2 * gap;
            //    }
            //    else {
            //        rect.X += width + gap;
            //    }

            //}

        }


        #region Property TapCommand

        /// <summary>
        /// Bindable Property TapCommand
        /// </summary>
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
          "TapCommand",
          typeof(System.Windows.Input.ICommand),
          typeof(AtomRepeator),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomRepeator)sender).OnTapCommandChanged(oldValue,newValue),
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
        /// On TapCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTapCommandChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property TapCommand
        /// </summary>
        public System.Windows.Input.ICommand TapCommand
        {
            get
            {
                return (System.Windows.Input.ICommand)GetValue(TapCommandProperty);
            }
            set
            {
                SetValue(TapCommandProperty, value);
            }
        }
        #endregion



        private Task OnItemTapped(object item)
        {

            TapCommand?.Execute(item);

            string path = SelectionPath;
            if (path == null)
                return Task.CompletedTask; ;

            object data = null;

            VisualElement ve = item as VisualElement;
            if (ve != null)
            {
                data = ve.BindingContext;
            }
            else
            {
                data = item;
            }

            if (data == null)
                return Task.CompletedTask;



            var isSelected = (bool)data.GetPropertyValue(path);
            data.SetPropertyValue(path, !isSelected);
            if (!isSelected)
            {
                SelectCommand?.Execute(data);
            }
            else
            {
                DeselectCommand?.Execute(data);
            }
            return Task.CompletedTask;
        }



        /// <summary>
        /// New WrapLayout
        /// </summary>
        /// <author>Jason Smith</author>
        public class WrapLayout : Layout<View>
        {
            Dictionary<View, SizeRequest> layoutCache = new Dictionary<View, SizeRequest>();

            #region Property Spacing

            /// <summary>
            /// Bindable Property Spacing
            /// </summary>
            public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
              "Spacing",
              typeof(double),
              typeof(WrapLayout),
              (double)5.0,
              BindingMode.OneWay,
              // validate value delegate
              // (sender,value) => true
              null,
              // property changed, delegate
              //(sender,oldValue,newValue) => ((WrapLayout)sender).OnSpacingChanged(oldValue,newValue),
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
            /// On Spacing changed
            /// </summary>
            /// <param name="oldValue">Old Value</param>
            /// <param name="newValue">New Value</param>
            protected virtual void OnSpacingChanged(object oldValue, object newValue)
            {

            }*/


            /// <summary>
            /// Property Spacing
            /// </summary>
            public double Spacing
            {
                get
                {
                    return (double)GetValue(SpacingProperty);
                }
                set
                {
                    SetValue(SpacingProperty, value);
                }
            }
            #endregion




            public WrapLayout()
            {
                VerticalOptions = HorizontalOptions = LayoutOptions.FillAndExpand;
            }

            protected override void OnChildMeasureInvalidated()
            {
                base.OnChildMeasureInvalidated();
                layoutCache.Clear();
            }

            protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
            {

                double lastX;
                double lastY;
                var layout = NaiveLayout(widthConstraint, heightConstraint, out lastX, out lastY);

                return new SizeRequest(new Size(lastX, lastY));
            }

            protected override void LayoutChildren(double x, double y, double width, double height)
            {
                double lastX, lastY;
                var layout = NaiveLayout(width, height, out lastX, out lastY);

                foreach (var t in layout)
                {
                    var offset = (int)((width - t.Last().Item2.Right) / 2);
                    foreach (var dingus in t)
                    {
                        var location = new Rectangle(dingus.Item2.X + x + offset, dingus.Item2.Y + y, dingus.Item2.Width, dingus.Item2.Height);
                        LayoutChildIntoBoundingRegion(dingus.Item1, location);
                    }
                }
            }

            private List<List<Tuple<View, Rectangle>>> NaiveLayout(double width, double height, out double lastX, out double lastY)
            {
                double startX = 0;
                double startY = 0;
                double right = width;
                double nextY = 0;

                lastX = 0;
                lastY = 0;

                var result = new List<List<Tuple<View, Rectangle>>>();
                var currentList = new List<Tuple<View, Rectangle>>();

                foreach (var child in Children)
                {
                    SizeRequest sizeRequest;
                    if (!layoutCache.TryGetValue(child, out sizeRequest))
                    {
                        layoutCache[child] = sizeRequest = child.GetSizeRequest(double.PositiveInfinity, double.PositiveInfinity);
                    }

                    var paddedWidth = sizeRequest.Request.Width + Spacing;
                    var paddedHeight = sizeRequest.Request.Height + Spacing;

                    if (startX + paddedWidth > right)
                    {
                        startX = 0;
                        startY += nextY;

                        if (currentList.Count > 0)
                        {
                            result.Add(currentList);
                            currentList = new List<Tuple<View, Rectangle>>();
                        }
                    }

                    currentList.Add(new Tuple<View, Rectangle>(child, new Rectangle(startX, startY, sizeRequest.Request.Width, sizeRequest.Request.Height)));

                    lastX = Math.Max(lastX, startX + paddedWidth);
                    lastY = Math.Max(lastY, startY + paddedHeight);

                    nextY = Math.Max(nextY, paddedHeight);
                    startX += paddedWidth;
                }
                result.Add(currentList);
                return result;
            }
        }
    }
}
