using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using System.Collections;
using System.Linq;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Specialized;
using CoreGraphics;
using System.Threading.Tasks;

//[assembly: Xamarin.Forms.ExportRenderer(typeof(AtomItemsControl), typeof(AtomItemsControlRenderer))]



namespace NeuroSpeech.UIAtoms.Controls
{
  //  public class AtomItemsControlRenderer: ViewRenderer<AtomItemsControl,UITableView>
  //  {

  //      AtomItemsControlSource source = null;

  //      protected override void OnElementChanged(ElementChangedEventArgs<AtomItemsControl> e)
  //      {
  //          base.OnElementChanged(e);

  //          var en = e.NewElement;
  //          if (en == null)
  //              return;

  //          var c = new UITableView();
		//	c.EstimatedRowHeight = UITableView.AutomaticDimension;
  //          source = new AtomItemsControlSource(en);
            

  //          en.CollectionChanged += Element_CollectionChanged;

		//	SetNativeControl(c);			
		//	Control.RowHeight = UITableView.AutomaticDimension;
  //          source.Prepare();
		//	Control.AllowsSelection = false;
		//	Control.AllowsMultipleSelection = false;
  //          Control.Source = source;

            
  //      }

  //      private void Element_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
  //      {
  //          source.Prepare();
  //          Control.ReloadData();
  //      }

  //      protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
  //      {
  //          base.OnElementPropertyChanged(sender, e);
  //          if (
  //              e.PropertyName == AtomItemsControl.ItemsSourceProperty.PropertyName ||
  //              e.PropertyName == AtomItemsControl.ItemTemplateProperty.PropertyName ||
  //              e.PropertyName == AtomItemsControl.ItemTemplateSelectorProperty.PropertyName ||
  //              e.PropertyName == AtomItemsControl.IsGroupingEnabledProperty.PropertyName
  //              ) {
  //              source.Prepare();
  //              Control.ReloadData();
  //          }
  //      }

  //      protected override void Dispose(bool disposing)
  //      {
  //          if (disposing) {
  //              if (source!=null) {
  //                  if (Control != null)
  //                  {
  //                      Control.Source = null;
  //                  }
  //                  source = null;
  //              }
  //          }
  //          base.Dispose(disposing);
  //      }

  //  }

  //  public class AtomList : List<object> {
  //      public string Header { get; set; }

  //      public AtomList(object source)
  //      {
  //          this.AddRange(((System.Collections.IEnumerable)source).Cast<object>());
  //      }
  //  }

  //  public class AtomItemsControlSource : UITableViewSource
  //  {
  //      private AtomItemsControl element;

  //      public List<AtomList> Items { get; set; } = new List<AtomList>();

  //      private Dictionary<object, UITableViewCell> cachedViews = new Dictionary<object, UITableViewCell>();
		//private Dictionary<object, ViewCell> formViews = new Dictionary<object, ViewCell>();

  //      public void Prepare() {
  //          cachedViews.Clear();
		//	formViews.Clear ();
  //          Items.Clear();
  //          if (element.ItemsSource == null)
  //              return;
  //          if (element.IsGroupingEnabled)
  //          {
  //              string propertyKey = ((Binding)element.GroupDisplayBinding).Path;
  //              foreach (var item in element.ItemsSource)
  //              {
  //                  string key = item.GetType().GetProperty(propertyKey)?.GetValue(item)?.ToString();
  //                  Items.Add(new AtomList(item) {
  //                      Header = key
  //                  });
  //              }
  //          }
  //          else {
  //              Items.Add(new AtomList(element.ItemsSource));
  //          }
  //      }

  //      public AtomItemsControlSource(AtomItemsControl element)
  //      {
  //          this.element = element;
  //      }

		//public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		//{
		//	var e = Items [(int)indexPath.Section] [(int)indexPath.Row];
		//	var v = CreateElement (e);
		//	//v.View.Layout
		//	var s = v.View.Measure (double.PositiveInfinity, double.PositiveInfinity);

		//	var n = ((nfloat)Math.Max(s.Request.Height,s.Minimum.Height) + 1f)
		//		* UIScreen.MainScreen.Scale;
		//	System.Diagnostics.Debug.WriteLine (string.Format("Height Request: {0}",n));
		//	return n;
		//}



  //      public override nint NumberOfSections(UITableView tableView)
  //      {
  //          return Items.Count;
  //      }

  //      public override string TitleForHeader(UITableView tableView, nint section)
  //      {
  //          if (!Items.Any())
  //              return null;
  //          return Items[(int)section].Header;
  //      }

		//private ViewCell CreateElement(object item){
		//	ViewCell view = null;
		//	if (!formViews.TryGetValue (item, out view)) {
		//		view = element.CreateElement (item);
		//		formViews [item] = view;
		//	}
		//	return view;
		//}

  //      public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
  //      {
  //          var section = Items[(int)indexPath.Section];
  //          var item = section[(int)indexPath.Row];

  //          UITableViewCell cell = null;

  //          if (cachedViews.TryGetValue(item, out cell))
  //              return cell;

  //          var view = CreateElement(item);
            

		//	//var r = new ViewCellRenderer ();
		//	//cell = r.GetCell (view, null, tableView);
		//	//cachedViews [item] = cell;
		//	//return cell;



		//	var acell = new AtomUITableViewCell(view.View);
		//	acell.onSizeChanged = () => {
		//		Device.BeginInvokeOnMainThread (() => {
		//			//await Task.Delay (TimeSpan.FromMilliseconds (5));
		//			var i = tableView.IndexPathForCell(acell);
		//			if(i!=null){
		//				//cachedViews.Remove(item);
		//				tableView.ReloadRows (new []{ i }, UITableViewRowAnimation.Automatic);
		//			}
		//		});
		//	};
		//	cell = acell;	
  //          cachedViews[item] = cell;

		//	//cell.InvalidateIntrinsicContentSize ();
  //          return cell;
            
  //      }

  //      public override nint RowsInSection(UITableView tableview, nint section)
  //      {
  //          if (!Items.Any())
  //              return 0;
  //          return Items[(int)section].Count;
  //      }

  //      private class AtomUITableViewCell : UITableViewCell
  //      {

  //          private UIView uiView;
		//	View view = null;
		//	WeakReference<IVisualElementRenderer> _rendererRef;

		//	public Action onSizeChanged;

		//	public AtomUITableViewCell(View view)
		//		:base(UITableViewCellStyle.Default,"AtomCell")
  //          {
		//		this.view = view;
		//		var renderer = Xamarin.Forms.Platform.iOS.Platform.CreateRenderer(view);
		//		Xamarin.Forms.Platform.iOS.Platform.SetRenderer(view,renderer);
		//		_rendererRef = new WeakReference<IVisualElementRenderer>(renderer);
  //              this.uiView = renderer.NativeView;
  //              view.SizeChanged += View_SizeChanged;           
		//		//this.ContentMode = uiView.ContentMode;
		//		//this.ContentScaleFactor = uiView.ContentScaleFactor;
		//		//this.ContentStretch = uiView.ContentStretch;
		//		this.ContentView.AddSubview(uiView);
		//		//this.UserInteractionEnabled = false;
		//		this.LayoutSubviews();
  //          }

		//	public override void LayoutSubviews ()
		//	{
		//		base.LayoutSubviews ();

		//		var contentFrame = ContentView.Frame;
		//		Layout.LayoutChildIntoBoundingRegion (view, contentFrame.ToRectangle ());
		//		if (_rendererRef == null)
		//			return;
		//		IVisualElementRenderer renderer;
		//		if (_rendererRef.TryGetTarget (out renderer)) {
		//			renderer.NativeView.Frame = contentFrame;
		//		}
		//	}

		//	public override CGSize SizeThatFits (CGSize size)
		//	{
		//		IVisualElementRenderer renderer = null;
		//		if (_rendererRef==null || !_rendererRef.TryGetTarget (out renderer)) {
		//			return base.SizeThatFits (size);
		//		}
		//		double width = size.Width;
		//		var height =  double.PositiveInfinity;
		//		var result = renderer.Element.Measure (width, height);

		//		return new CGSize (size.Width, ((float)result.Request.Height + 1f) / UIScreen.MainScreen.Scale);
		//	}

		//	protected override void Dispose (bool disposing)
		//	{
		//		if (disposing) {
		//			onSizeChanged = null;
		//			if (_rendererRef != null) {
		//				IVisualElementRenderer renderer;
		//				if (_rendererRef.TryGetTarget (out renderer)) {
		//					renderer.Dispose ();
		//				}
		//				_rendererRef = null;
		//			}
		//		}
		//		base.Dispose (disposing);
		//	}

  //          private void View_SizeChanged(object sender, EventArgs e)
  //          {
		//		//this.LayoutSubviews ();
		//		/*var frame = ContentView.Frame;
		//		CGSize s = new CGSize (frame.Width, double.PositiveInfinity);
		//		var r = view.Measure (s.Width, s.Height);
		//		frame.Height = (nfloat)Math.Max (r.Request.Height, r.Minimum.Height);
		//		this.InvalidateIntrinsicContentSize ();*/
		//		onSizeChanged?.Invoke();
  //          }

  //      }
  //  }
}
