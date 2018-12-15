//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Xamarin.Forms.Platform.Android;
//using System.Collections.Specialized;
//using Java.Lang;
//using System.ComponentModel;
//using NeuroSpeech.UIAtoms.Controls;


//[assembly: Xamarin.Forms.ExportRenderer(typeof(AtomImage), typeof(AtomImageRenderer))]

//namespace NeuroSpeech.UIAtoms.Controls
//{
//    public class AtomItemsControlRenderer : ViewRenderer<AtomItemsControl, Android.Widget.ListView>
//    {

//        AtomItemsControlSource source = null;

//        protected override void OnElementChanged(ElementChangedEventArgs<AtomItemsControl> e)
//        {
//            base.OnElementChanged(e);

//            var c = new Android.Widget.ListView(Android.App.Application.Context);

//            source = new AtomItemsControlSource(Element);


//            Element.CollectionChanged += Element_CollectionChanged;

//            SetNativeControl(c);
//            source.Prepare();
//            Control.Adapter = source;


//        }

//        private void Element_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
//        {
//            source.Prepare();
//        }

//        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            base.OnElementPropertyChanged(sender, e);
//            if (
//                e.PropertyName == AtomItemsControl.ItemsSourceProperty.PropertyName ||
//                e.PropertyName == AtomItemsControl.ItemTemplateProperty.PropertyName ||
//                e.PropertyName == AtomItemsControl.ItemTemplateSelectorProperty.PropertyName ||
//                e.PropertyName == AtomItemsControl.IsGroupingEnabledProperty.PropertyName
//                )
//            {
//                source.Prepare();

//            }
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                if (source != null)
//                {
//                    if (Control != null)
//                    {
//                        Control.Adapter = null;
//                    }
//                    source = null;
//                }
//            }
//            base.Dispose(disposing);
//        }

//    }

//    public class AtomItem
//    {
//        public long id { get; set; }

//        public string Header { get; set; }

//        public object Value { get; set; }

//        public bool IsHeader { get; set; }

//    }

//    public class AtomItemsControlSource : BaseAdapter
//    {
//        private AtomItemsControl element;

//        public List<AtomItem> Items { get; set; }

//        public override int Count
//        {
//            get
//            {
//                return Items.Count;
//            }
//        }

//        private Dictionary<AtomItem, Android.Views.View> cachedViews = new Dictionary<AtomItem, Android.Views.View>();


//        public void Prepare()
//        {
//            cachedViews.Clear();
//            if (element.IsGroupingEnabled)
//            {
//                string propertyKey = ((Xamarin.Forms.Binding)element.GroupDisplayBinding).Path;
//                foreach (var item in element.ItemsSource)
//                {
//                    string key = item.GetType().GetProperty(propertyKey)?.GetValue(item)?.ToString();
//                    var a = new AtomItem { id = Items.Count, Header = key, IsHeader = true };
//                    Items.Add(a);
//                    foreach (var child in (System.Collections.IEnumerable)item)
//                    {
//                        Items.Add(new AtomItem { id = Items.Count, Header = key, Value = child });
//                    }
//                }
//            }
//            else
//            {
//                foreach (var item in element.ItemsSource)
//                {
//                    Items.Add(new AtomItem { id = Items.Count, Value = item });
//                }
//            }
//        }

//        public AtomItemsControlSource(AtomItemsControl element)
//        {
//            this.element = element;
//        }

//        public override Java.Lang.Object GetItem(int position)
//        {
//            return (Java.Lang.Object)((object)Items[position]);
//        }

//        public override long GetItemId(int position)
//        {
//            return Items[position].id;
//        }

//        public override View GetView(int position, View convertView, ViewGroup parent)
//        {
//            var item = Items[position];
//            View view = null;
//            if (cachedViews.TryGetValue(item, out view))
//            {
//                return view;
//            }

//            if (item.IsHeader)
//            {
//                var fl = new FrameLayout(Android.App.Application.Context)
//                {
//                    LayoutParameters = new ViewGroup.LayoutParams(
//                        ViewGroup.LayoutParams.MatchParent,
//                        ViewGroup.LayoutParams.WrapContent)
//                };
//                var label = new TextView(Android.App.Application.Context)
//                {
//                    LayoutParameters = new FrameLayout.LayoutParams(
//                        ViewGroup.LayoutParams.MatchParent,
//                        ViewGroup.LayoutParams.WrapContent)
//                    {
//                        LeftMargin = 5,
//                        RightMargin = 5,
//                        TopMargin = 5,
//                        BottomMargin = 5
//                    },
//                    Text = item.Header
//                };
//                fl.AddView(label);
//                view = fl;
//            }
//            else
//            {
//                var v = element.CreateElement(item.Value);

//                var r = Xamarin.Forms.Platform.Android.Platform.CreateRenderer(v.View);
//                view = r.ViewGroup;
//            }

//            cachedViews[item] = view;
//            return view;
//        }

//        //private class AtomUITableViewCell : UITableViewCell
//        //{
//        //    private View view;
//        //    private UIView uiView;

//        //    public AtomUITableViewCell(View view)
//        //    {
//        //        this.view = view;
//        //        this.uiView = Xamarin.Forms.Platform.iOS.Platform.CreateRenderer(view).NativeView;
//        //        view.SizeChanged += View_SizeChanged;
//        //    }

//        //    private void View_SizeChanged(object sender, EventArgs e)
//        //    {
//        //        this.InvalidateIntrinsicContentSize();
//        //    }

//        //    public override UIView ContentView
//        //    {
//        //        get
//        //        {
//        //            return uiView;
//        //        }
//        //    }

//        //    public override CGSize IntrinsicContentSize
//        //    {
//        //        get
//        //        {
//        //            return uiView.IntrinsicContentSize;
//        //        }
//        //    }
//        //}
//    }
//}