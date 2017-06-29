using CoreGraphics;
using Foundation;
using NeuroSpeech.UIAtoms.Controls;
using NeuroSpeech.UIAtoms.iOS.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(AtomGridView), typeof(AtomGridViewRenderer))]

namespace NeuroSpeech.UIAtoms.iOS.Controls
{
    //public class GridViewRenderer : ViewRenderer<AtomGridView, AtomGridCollectionView>
    //{
    //    /// <summary>
    //    /// The data source
    //    /// </summary>
    //    private GridDataSource _dataSource;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="GridViewRenderer"/> class.
    //    /// </summary>
    //    public GridViewRenderer()
    //    {
    //    }

    //    /// <summary>
    //    /// Rowses the in section.
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="section">The section.</param>
    //    /// <returns>System.Int32.</returns>
    //    public int RowsInSection(UICollectionView collectionView, nint section)
    //    {
    //        return ((ICollection)this.Element.ItemsSource).Count;
    //    }

    //    /// <summary>
    //    /// Items the selected.
    //    /// </summary>
    //    /// <param name="tableView">The table view.</param>
    //    /// <param name="indexPath">The index path.</param>
    //    public void ItemSelected(UICollectionView tableView, NSIndexPath indexPath)
    //    {
    //        var item = this.Element.ItemsSource.Cast<object>().ElementAt(indexPath.Row);
    //        this.Element.InvokeItemSelectedEvent(this, item);
    //    }

    //    /// <summary>
    //    /// Gets the cell.
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="indexPath">The index path.</param>
    //    /// <returns>UICollectionViewCell.</returns>
    //    public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    //    {
    //        var item = this.Element.ItemsSource.Cast<object>().ElementAt(indexPath.Row);
    //        var viewCellBinded = (this.Element.ItemTemplate.CreateContent() as ViewCell);
    //        if (viewCellBinded == null) return null;

    //        viewCellBinded.BindingContext = item;
    //        return this.GetCell(collectionView, viewCellBinded, indexPath);
    //    }

    //    /// <summary>
    //    /// Gets the cell.
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="item">The item.</param>
    //    /// <param name="indexPath">The index path.</param>
    //    /// <returns>UICollectionViewCell.</returns>
    //    protected virtual UICollectionViewCell GetCell(UICollectionView collectionView, ViewCell item, NSIndexPath indexPath)
    //    {
    //        var collectionCell = collectionView.DequeueReusableCell(new NSString(AtomGridViewCell.Key), indexPath) as AtomGridViewCell;

    //        if (collectionCell == null) return null;

    //        collectionCell.ViewCell = item;

    //        return collectionCell;
    //    }

    //    /// <summary>
    //    /// Called when [element changed].
    //    /// </summary>
    //    /// <param name="e">The e.</param>
    //    protected override void OnElementChanged(ElementChangedEventArgs<AtomGridView> e)
    //    {
    //        base.OnElementChanged(e);
    //        if (e.OldElement != null)
    //        {
    //            Unbind(e.OldElement);
    //        }
    //        if (e.NewElement != null)
    //        {
    //            if (Control == null)
    //            {
    //                var collectionView = new AtomGridCollectionView
    //                {
    //                    AllowsMultipleSelection = false,
    //                    SelectionEnable = e.NewElement.SelectionEnabled,
    //                    ContentInset = new UIEdgeInsets((float)this.Element.Padding.Top, (float)this.Element.Padding.Left, (float)this.Element.Padding.Bottom, (float)this.Element.Padding.Right),
    //                    BackgroundColor = this.Element.BackgroundColor.ToUIColor(),
    //                    ItemSize = new CoreGraphics.CGSize((float)this.Element.ItemWidth, (float)this.Element.ItemHeight),
    //                    RowSpacing = this.Element.RowSpacing,
    //                    ColumnSpacing = this.Element.ColumnSpacing
    //                };

    //                Bind(e.NewElement);

    //                collectionView.Source = this.DataSource;
    //                //collectionView.Delegate = this.GridViewDelegate;

    //                SetNativeControl(collectionView);
    //            }
    //        }


    //    }

    //    /// <summary>
    //    /// Unbinds the specified old element.
    //    /// </summary>
    //    /// <param name="oldElement">The old element.</param>
    //    private void Unbind(AtomGridView oldElement)
    //    {
    //        if (oldElement == null) return;

    //        oldElement.PropertyChanging -= this.ElementPropertyChanging;
    //        oldElement.PropertyChanged -= this.ElementPropertyChanged;

    //        var itemsSource = oldElement.ItemsSource as INotifyCollectionChanged;
    //        if (itemsSource != null)
    //        {
    //            itemsSource.CollectionChanged -= this.DataCollectionChanged;
    //        }
    //    }

    //    /// <summary>
    //    /// Binds the specified new element.
    //    /// </summary>
    //    /// <param name="newElement">The new element.</param>
    //    private void Bind(AtomGridView newElement)
    //    {
    //        if (newElement == null) return;

    //        newElement.PropertyChanging += this.ElementPropertyChanging;
    //        newElement.PropertyChanged += this.ElementPropertyChanged;

    //        var source = newElement.ItemsSource as INotifyCollectionChanged;
    //        if (source != null)
    //        {
    //            source.CollectionChanged += this.DataCollectionChanged;
    //        }
    //    }

    //    /// <summary>
    //    /// Elements the property changed.
    //    /// </summary>
    //    /// <param name="sender">The sender.</param>
    //    /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
    //    private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    //    {
    //        if (e.PropertyName == "ItemsSource")
    //        {
    //            var newItemsSource = this.Element.ItemsSource as INotifyCollectionChanged;
    //            if (newItemsSource != null)
    //            {
    //                newItemsSource.CollectionChanged += DataCollectionChanged;
    //                this.Control.ReloadData();
    //            }
    //        }
    //        else if (e.PropertyName == "ItemWidth" || e.PropertyName == "ItemHeight")
    //        {
    //            this.Control.ItemSize = new CoreGraphics.CGSize((float)this.Element.ItemWidth, (float)this.Element.ItemHeight);
    //        }
    //    }

    //    /// <summary>
    //    /// Elements the property changing.
    //    /// </summary>
    //    /// <param name="sender">The sender.</param>
    //    /// <param name="e">The <see cref="PropertyChangingEventArgs"/> instance containing the event data.</param>
    //    private void ElementPropertyChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
    //    {
    //        if (e.PropertyName == "ItemsSource")
    //        {
    //            var oldItemsSource = this.Element.ItemsSource as INotifyCollectionChanged;
    //            if (oldItemsSource != null)
    //            {
    //                oldItemsSource.CollectionChanged -= DataCollectionChanged;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Datas the collection changed.
    //    /// </summary>
    //    /// <param name="sender">The sender.</param>
    //    /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
    //    private void DataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    //    {
    //        InvokeOnMainThread(() => {
    //            try
    //            {
    //                if (this.Control == null) return;

    //                // try to handle add or remove operations gracefully, just reload the whole collection for other changes
    //                var indexes = new List<NSIndexPath>();
    //                switch (e.Action)
    //                {
    //                    case NotifyCollectionChangedAction.Add:
    //                        for (int i = 0; i < e.NewItems.Count; i++)
    //                        {
    //                            indexes.Add(NSIndexPath.FromRowSection((nint)(e.NewStartingIndex + i), 0));
    //                        }
    //                        this.Control.InsertItems(indexes.ToArray());
    //                        break;
    //                    case NotifyCollectionChangedAction.Remove:
    //                        for (int i = 0; i < e.OldItems.Count; i++)
    //                        {
    //                            indexes.Add(NSIndexPath.FromRowSection((nint)(e.OldStartingIndex + i), 0));
    //                        }
    //                        this.Control.DeleteItems(indexes.ToArray());
    //                        break;
    //                    default:
    //                        this.Control.ReloadData();
    //                        break;
    //                }
    //            }
    //            catch { } // todo: determine why we are hiding a possible exception here
    //        });
    //    }

    //    /// <summary>
    //    /// Gets the data source.
    //    /// </summary>
    //    /// <value>The data source.</value>
    //    private GridDataSource DataSource
    //    {
    //        get
    //        {
    //            return _dataSource ?? (_dataSource = new GridDataSource(GetCell, RowsInSection, ItemSelected));
    //        }
    //    }

    //    /// <summary>
    //    /// Releases unmanaged and - optionally - managed resources.
    //    /// </summary>
    //    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    //    protected override void Dispose(bool disposing)
    //    {
    //        base.Dispose(disposing);
    //        if (disposing && _dataSource != null)
    //        {
    //            Unbind(Element);
    //            _dataSource.Dispose();
    //            _dataSource = null;
    //        }
    //    }
    //}

    //public class GridDataSource : UICollectionViewSource
    //{
    //    /// <summary>
    //    /// Delegate OnGetCell
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="indexPath">The index path.</param>
    //    /// <returns>UICollectionViewCell.</returns>
    //    public delegate UICollectionViewCell OnGetCell(UICollectionView collectionView, NSIndexPath indexPath);

    //    /// <summary>
    //    /// Delegate OnRowsInSection
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="section">The section.</param>
    //    /// <returns>System.Int32.</returns>
    //    public delegate int OnRowsInSection(UICollectionView collectionView, nint section);

    //    /// <summary>
    //    /// Delegate OnItemSelected
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="indexPath">The index path.</param>
    //    public delegate void OnItemSelected(UICollectionView collectionView, NSIndexPath indexPath);

    //    /// <summary>
    //    /// The _on get cell
    //    /// </summary>
    //    private readonly OnGetCell _onGetCell;
    //    /// <summary>
    //    /// The _on rows in section
    //    /// </summary>
    //    private readonly OnRowsInSection _onRowsInSection;
    //    /// <summary>
    //    /// The _on item selected
    //    /// </summary>
    //    private readonly OnItemSelected _onItemSelected;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="GridDataSource"/> class.
    //    /// </summary>
    //    /// <param name="onGetCell">The on get cell.</param>
    //    /// <param name="onRowsInSection">The on rows in section.</param>
    //    /// <param name="onItemSelected">The on item selected.</param>
    //    public GridDataSource(OnGetCell onGetCell, OnRowsInSection onRowsInSection, OnItemSelected onItemSelected)
    //    {
    //        this._onGetCell = onGetCell;
    //        this._onRowsInSection = onRowsInSection;
    //        this._onItemSelected = onItemSelected;
    //    }

    //    #region implemented abstract members of UICollectionViewDataSource

    //    /// <summary>
    //    /// Gets the items count.
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="section">The section.</param>
    //    /// <returns>System.Int32.</returns>
    //    public override nint GetItemsCount(UICollectionView collectionView, nint section)
    //    {
    //        return this._onRowsInSection(collectionView, section);
    //    }

    //    /// <summary>
    //    /// Items the selected.
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="indexPath">The index path.</param>
    //    public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
    //    {
    //        this._onItemSelected(collectionView, indexPath);
    //    }
    //    /// <summary>
    //    /// Gets the cell.
    //    /// </summary>
    //    /// <param name="collectionView">The collection view.</param>
    //    /// <param name="indexPath">The index path.</param>
    //    /// <returns>UICollectionViewCell.</returns>
    //    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    //    {
    //        var cell = this._onGetCell(collectionView, indexPath);

    //        if (((AtomGridCollectionView)collectionView).SelectionEnable)
    //        {
    //            // todo: investigate whether this needs to be removed when cell is re-used
    //            cell.AddGestureRecognizer(new UITapGestureRecognizer(v =>
    //            {
    //                ItemSelected(collectionView, indexPath);
    //            }));
    //        }
    //        else
    //        {
    //            cell.SelectedBackgroundView = new UIView();
    //        }

    //        return cell;
    //    }

    //    #endregion
    //}

    //public class AtomGridCollectionView : UICollectionView
    //{

    //    public AtomGridCollectionView() : this(default(CGRect))
    //    {

    //    }

    //    public AtomGridCollectionView(CGRect rect)
    //        : base(rect, new UICollectionViewFlowLayout())
    //    {
    //        AutoresizingMask = UIViewAutoresizing.All;
    //        ContentMode = UIViewContentMode.ScaleToFill;
    //        RegisterClassForCell(typeof(AtomGridViewCell), new NSString(AtomGridViewCell.Key));

    //    }

    //    public double RowSpacing
    //    {
    //        get
    //        {
    //            return ((UICollectionViewFlowLayout)this.CollectionViewLayout).MinimumLineSpacing;
    //        }
    //        set
    //        {
    //            ((UICollectionViewFlowLayout)this.CollectionViewLayout).MinimumLineSpacing = (nfloat)value;
    //        }
    //    }

    //    public double ColumnSpacing
    //    {
    //        get
    //        {
    //            return ((UICollectionViewFlowLayout)this.CollectionViewLayout).MinimumInteritemSpacing;
    //        }
    //        set
    //        {
    //            ((UICollectionViewFlowLayout)this.CollectionViewLayout).MinimumInteritemSpacing = (nfloat)value;
    //        }
    //    }

    //    public CGSize ItemSize
    //    {
    //        get
    //        {
    //            return ((UICollectionViewFlowLayout)this.CollectionViewLayout).ItemSize;
    //        }
    //        set
    //        {
    //            ((UICollectionViewFlowLayout)this.CollectionViewLayout).ItemSize = value;
    //        }
    //    }

    //    public object SelectionEnable { get; internal set; }

    //    public override UICollectionViewCell CellForItem(NSIndexPath indexPath)
    //    {
    //        if (indexPath == null)
    //        {
    //            //calling base.CellForItem(indexPath) when indexPath is null causes an exception.
    //            //indexPath could be null in the following scenario:
    //            // - GridView is configured to show 2 cells per row and there are 3 items in ItemsSource collection
    //            // - you're trying to drag 4th cell (empty) like you're trying to scroll
    //            return null;
    //        }
    //        return base.CellForItem(indexPath);
    //    }

    //    public override void Draw(CGRect rect)
    //    {
    //        this.CollectionViewLayout.InvalidateLayout();

    //        base.Draw(rect);
    //    }
    //}
    

    //public class AtomGridViewCell : UICollectionViewCell
    //{
    //    public const string Key = "AtomGridViewCell";

    //    ViewCell _viewCell;

    //    UIView _view;


    //    [Export("initWithFrame:")]
    //    public AtomGridViewCell(CGRect rect):base(rect)
    //    {

    //    }

    //    public override void LayoutSubviews()
    //    {
    //        base.LayoutSubviews();
    //        var frame = this.ContentView.Frame;
    //        frame.X = (this.Bounds.Width - frame.Width) / 2;
    //        frame.Y = (this.Bounds.Height - frame.Height) / 2;
    //        this.ViewCell.View.Layout(frame.ToRectangle());
    //        this._view.Frame = frame;
    //    }

    //    public ViewCell ViewCell
    //    {
    //        get { return this._viewCell; }
    //        set
    //        {
    //            if (this._viewCell != value) {
    //                UpdateCell(value);
    //            }
    //        }
    //    }

    //    private void UpdateCell(ViewCell value)
    //    {
    //        if (this._viewCell != null)
    //        {
    //            //this.viewCell.SendDisappearing ();
    //            this._viewCell.PropertyChanged -= this.HandlePropertyChanged;
    //        }

    //        this._viewCell = value;
    //        this._viewCell.PropertyChanged += this.HandlePropertyChanged;
    //        //this.viewCell.SendAppearing ();
    //        UpdateView();
    //    }

    //    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    //    {
    //        UpdateView();
    //    }

    //    private void UpdateView()
    //    {
    //        if (this._view != null)
    //        {
    //            this._view.RemoveFromSuperview();
    //        }

            
            
    //        this._view = Platform.CreateRenderer(this._viewCell.View).NativeView;
    //        this._view.AutoresizingMask = UIViewAutoresizing.All;
    //        this._view.ContentMode = UIViewContentMode.ScaleToFill;

    //        AddSubview(this._view);
    //    }
    //}
}
