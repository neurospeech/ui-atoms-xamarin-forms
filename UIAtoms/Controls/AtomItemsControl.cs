
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomItemsControl : AtomListView
    {



        #region ItemHeader Attached Property
        /// <summary>
        /// ItemHeader Attached property
        /// </summary>
        public static readonly BindableProperty ItemModelProperty =
            BindableProperty.CreateAttached("ItemModel", typeof(HeaderedItem),
            typeof(AtomItemsControl),
            HeaderedItem.Default,
            BindingMode.OneWay,
            null,
            null);//OnItemHeaderChanged);

        /*private static void OnItemHeaderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// Set ItemHeader for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        private static void SetItemModel(BindableObject bindable, HeaderedItem newValue)
        {
            bindable.SetValue(ItemModelProperty, newValue);
        }

        /// <summary>
        /// Get ItemHeader for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        private static HeaderedItem GetItemModel(BindableObject bindable)
        {
            return bindable?.GetValue(ItemModelProperty) as HeaderedItem;
        }
        #endregion




        ScrollView scrollView;
        StackLayout layout;
        private List<HeaderedItem> cachedSource = new List<HeaderedItem>();

        /// <summary>
        /// 
        /// </summary>
        public bool Learning { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateContent()
        {
            //base.CreateContent();

            scrollView = new ScrollView();


            scrollView.Scrolled += ScrollView_Scrolled;
            scrollView.Content = layout = new StackLayout { };


            this.Content = scrollView;
        }


        /// <summary>
        /// Start learning if itemsSource has changed
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnItemsSourceChanged(object oldValue, object newValue)
        {
            Learning = oldValue != newValue;
            base.OnItemsSourceChanged(oldValue, newValue);
        }


        System.Threading.CancellationTokenSource cancel = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            System.Collections.IEnumerable source = sender as System.Collections.IEnumerable;

            if (source != null)
            {
                cancel?.Cancel();
                cancel = new System.Threading.CancellationTokenSource();
                Task.Run(() => {
                    try
                    {
                        cachedSource = GenerateItems(source);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            RecreateItems();
                        });
                        cancel = null;
                    }
                    catch {
                        // ignore everything...
                    }
                },cancel.Token);
                
            }
            else
            {
                layout.Children.Clear();
                this.Learning = true;
                RecreateItems();
            }
        }

        private List<HeaderedItem> GenerateItems(IEnumerable source)
        {
            var items = new List<HeaderedItem>();
            var s = (source as IList) ?? (new List<object>(source.Cast<object>()));
            if (IsGroupingEnabled)
            {
                int index = 0;
                // enumerate and create group....
                foreach (var gitem in s)
                {
                    var g = new HeaderedItem
                    {
                        Header = gitem,
                        Index = index++
                    };

                    items.Add(g);
                    foreach (var item in (IEnumerable)gitem)
                    {
                        items.Add(new HeaderedItem
                        {
                            Item = item,
                            Index = index++
                        });
                    }
                }
            }
            else
            {
                for (var i = 0; i < s.Count; i++)
                {
                    var item = s[i];
                    items.Add(new HeaderedItem
                    {
                        Item = item,
                        Index = i
                    });
                }
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTemplate"></param>
        protected override void SetItemTemplate(DataTemplate dataTemplate)
        {
            layout.Children.Clear();
            this.Learning = true;
            RecreateItems();
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            UIAtomsApplication.Instance.TriggerOnce(RecreateItems);
        }

        private bool isCreating = false;
        private double avgHeight;
        private double avgWidth;
        private int totalVisibleItems;

        private void RecreateItems()
        {
            try
            {
                if (isCreating) {
                    DispatchRecreateItems();
                }
                isCreating = true;
                if (this.Height <= 0)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Delay(100);
                        RecreateItems();
                    });
                }
                if (this.Learning)
                {
                    if (IsOutsideScrollView())
                    {
                        // lets add one item...


                        var last = GetItemModel(layout.Children.LastOrDefault());
                        int index = last?.Index + 1 ?? 0;

                        if (index == cachedSource.Count)
                            return;

                        var next = cachedSource[index];
                        var item = CreateView(next);

                        layout.Children.Add(item);

                    }
                    else
                    {
                        this.Learning = false;

                        // calculate average height and average width...
                        var width = 0.0;
                        var height = 0.0;

                        foreach (var item in layout.Children)
                        {
                            width += item.Width;
                            height += item.Height;
                        }

                        var total = layout.Children.Count - 1;
                        width /= total;
                        height /= total;

                        this.avgWidth = width;
                        this.avgHeight = height;

                        this.totalVisibleItems = total;


                        var p = layout.Padding;
                        var padding = (cachedSource.Count  - total + 1) * avgHeight;
                        layout.Padding = new Thickness(p.Left, p.Top, p.Right, p.Bottom + padding);


                    }
                    DispatchRecreateItems();
                    return;
                }


                var lastScrollTop = scrollView.ScrollY;



            }
            finally {
                isCreating = false;
            }
        }

        private bool IsOutsideScrollView()
        {
            return this.Height <= layout.Height;
        }

        private void DispatchRecreateItems()
        {
            Device.BeginInvokeOnMainThread(async () => {
                await Task.Delay(10);
                RecreateItems();
            });
        }

        private View CreateView(HeaderedItem next)
        {
            var h = next.Header;
            View view;

            if (h != null) {
                view = HeaderTemplate?.CreateContent() as View ?? new Label { };
                view.BindingContext = h;
                return view;
            }

            view = ItemTemplate?.CreateContent() as View ?? new Label { };
            view.BindingContext = next.Item;
            SetItemModel(view, next);
            return view;
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public class HeaderedItem {

        /// <summary>
        /// 
        /// </summary>
        public static HeaderedItem Default = new HeaderedItem() {
            Index = -1
        };

        /// <summary>
        /// 
        /// </summary>
        public int Index;

        /// <summary>
        /// 
        /// </summary>
        public object Header; 

        /// <summary>
        /// 
        /// </summary>
        public object Item;
    }
}