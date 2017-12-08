using NeuroSpeech.UIAtoms.Controls.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Collections;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAtomListItem {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        void OnAppearing(AtomListView owner);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        void OnDisappearing(AtomListView owner);
    }



    /// <summary>
    /// 
    /// </summary>
    public class AtomListView : ContentView
    {



        #region Property AutoScrollOnSelection

        /// <summary>
        /// Bindable Property AutoScrollOnSelection
        /// </summary>
        public static readonly BindableProperty AutoScrollOnSelectionProperty = BindableProperty.Create(
          nameof(AutoScrollOnSelection),
          typeof(bool),
          typeof(AtomListView),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnAutoScrollOnSelectionChanged(oldValue,newValue),
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
        /// On AutoScrollOnSelection changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAutoScrollOnSelectionChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AutoScrollOnSelection
        /// </summary>
        public bool AutoScrollOnSelection
        {
            get
            {
                return (bool)GetValue(AutoScrollOnSelectionProperty);
            }
            set
            {
                SetValue(AutoScrollOnSelectionProperty, value);
            }
        }
        #endregion



        private AtomList<object> selectedItems = new AtomList<object>();

        /// <summary>
        /// 
        /// </summary>
        protected ScrollableListView listView;


        /// <summary>
        /// 
        /// </summary>
        public AtomListView()
        {
            selectedItems.CollectionChanged += SelectedItems_CollectionChanged;

            this.ControlTemplate = new ControlTemplate(typeof(AtomListViewControlTemplate));

            CreateContent();

        }

        /// <summary>
        /// 
        /// </summary>
        public class AtomListViewControlTemplate: Grid {


            /// <summary>
            /// 
            /// </summary>
            public AtomListViewControlTemplate()
            {

                this.RowDefinitions.Add(new RowDefinition { });
                this.RowDefinitions.Add(new RowDefinition {
                    Height = GridLength.Auto
                });
                this.RowDefinitions.Add(new RowDefinition { });


                StackLayout sl = new StackLayout() {
                    Orientation = StackOrientation.Vertical
                };

                sl.SetBinding(StackLayout.IsVisibleProperty, new TemplateBinding {
                    Path = nameof(AtomListView.HasItems),
                    Converter = NegateBooleanConverter.Instance
                });
                

                var label = new Label {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };
                label.SetBinding(Label.TextProperty, new TemplateBinding
                {
                    
                    Path = nameof(AtomListView.NoItemsMessage)
                });

                Grid.SetRow(sl, 1);

                var button = new Button {
                    HorizontalOptions = LayoutOptions.Center
                };
                button.SetBinding(Button.CommandProperty, new TemplateBinding
                {
                    Path = nameof(AtomListView.AddButtonCommand)
                });
                button.SetBinding(Button.IsVisibleProperty, new TemplateBinding {
                    Path = nameof(AtomListView.AddButtonCommand),
                    Converter = NotNullVisibilityConverter.Instance
                });
                button.SetBinding(Button.TextProperty, new TemplateBinding
                {
                    
                    Path = nameof(AtomListView.AddButtonLabel)
                });
                button.SetBinding(Button.ImageProperty, new TemplateBinding
                {
                    
                    Path = nameof(AtomListView.AddButtonIcon)
                });
                

                var cp = new ContentPresenter { };
                Grid.SetRowSpan(cp, 3);
                cp.SetBinding(ContentPresenter.IsVisibleProperty, new TemplateBinding
                {
                    
                    Path = nameof(AtomListView.HasItems)
                });
                sl.Children.Add(label);
                sl.Children.Add(button);

                this.Children.Add(sl);
                this.Children.Add(cp);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void CreateContent()
        {
            listView = new ScrollableListView();
            

            listView.OnHookContent = (s, e) =>
            {
                IAtomListItem vc = (s as ViewCell)?.View as IAtomListItem;
                vc?.OnAppearing(this);
            };

            listView.OnUnhookContent = (s) =>
            {
                IAtomListItem vc = (s as ViewCell)?.View as IAtomListItem;
                vc?.OnDisappearing(this);
            };


            listView.ItemSelected += (s, e) =>
            {
                listView.SelectedItem = null;
            };



            Content = listView;
        }

        #region Property SeparatorVisibility

        /// <summary>
        /// Bindable Property SeparatorVisibility
        /// </summary>
        public static readonly BindableProperty SeparatorVisibilityProperty = BindableProperty.Create(
          nameof(SeparatorVisibility),
          typeof(SeparatorVisibility),
          typeof(AtomListView),
          SeparatorVisibility.None,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnSeparatorVisibilityChanged(oldValue,newValue),
          //null,
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
        /// On SeparatorVisibility changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSeparatorVisibilityChanged(object oldValue, object newValue)
        {
            listView.SeparatorVisibility = (SeparatorVisibility)newValue;
        }



        /// <summary>
        /// Property SeparatorVisibility
        /// </summary>
        public SeparatorVisibility SeparatorVisibility
        {
            get
            {
                return (SeparatorVisibility)GetValue(SeparatorVisibilityProperty);
            }
            set
            {
                SetValue(SeparatorVisibilityProperty, value);
            }
        }
        #endregion





        #region Property NoItemsMessage

        /// <summary>
        /// Bindable Property AddMessage
        /// </summary>
        public static readonly BindableProperty NoItemsMessageProperty = BindableProperty.Create(
          nameof(NoItemsMessage),
          typeof(string),
          typeof(AtomListView),
          "No items to display",
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnNoItemsMessageChanged(oldValue,newValue),
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
        /// On AddMessage changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnNoItemsMessageChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AddMessage
        /// </summary>
        public string NoItemsMessage
        {
            get
            {
                return (string)GetValue(NoItemsMessageProperty);
            }
            set
            {
                SetValue(NoItemsMessageProperty, value);
            }
        }
        #endregion

        #region Property AddButtonLabel

        /// <summary>
        /// Bindable Property AddButtonLabel
        /// </summary>
        public static readonly BindableProperty AddButtonLabelProperty = BindableProperty.Create(
          nameof(AddButtonLabel),
          typeof(string),
          typeof(AtomListView),
          "Add",
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnAddButtonLabelChanged(oldValue,newValue),
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
        /// On AddButtonLabel changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAddButtonLabelChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AddButtonLabel
        /// </summary>
        public string AddButtonLabel
        {
            get
            {
                return (string)GetValue(AddButtonLabelProperty);
            }
            set
            {
                SetValue(AddButtonLabelProperty, value);
            }
        }
        #endregion



        #region Property AddButtonIcon

        /// <summary>
        /// Bindable Property AddButtonIcon
        /// </summary>
        public static readonly BindableProperty AddButtonIconProperty = BindableProperty.Create(
          nameof(AddButtonIcon),
          typeof(FileImageSource),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnAddButtonIconChanged(oldValue,newValue),
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
        /// On AddButtonIcon changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAddButtonIconChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AddButtonIcon
        /// </summary>
        public FileImageSource AddButtonIcon
        {
            get
            {
                return (FileImageSource)GetValue(AddButtonIconProperty);
            }
            set
            {
                SetValue(AddButtonIconProperty, value);
            }
        }
        #endregion





        #region Property AddButtonCommand

        /// <summary>
        /// Bindable Property AddButtonCommand
        /// </summary>
        public static readonly BindableProperty AddButtonCommandProperty = BindableProperty.Create(
          nameof(AddButtonCommand),
          typeof(ICommand),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnAddButtonCommandChanged(oldValue,newValue),
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
        /// On AddButtonCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAddButtonCommandChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AddButtonCommand
        /// </summary>
        public ICommand AddButtonCommand
        {
            get
            {
                return (ICommand)GetValue(AddButtonCommandProperty);
            }
            set
            {
                SetValue(AddButtonCommandProperty, value);
            }
        }
        #endregion





        #region Property SeparatorColor

        /// <summary>
        /// Bindable Property SeparatorColor
        /// </summary>
        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create(
          nameof(SeparatorColor),
          typeof(Color),
          typeof(AtomListView),
          Color.Accent,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnSeparatorColorChanged(oldValue,newValue),
          //null,
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
        /// On SeparatorColor changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSeparatorColorChanged(object oldValue, object newValue)
        {
            if (listView != null)
            {
                listView.SeparatorColor = (Color)newValue;
            }
        }


        /// <summary>
        /// Property SeparatorColor
        /// </summary>
        public Color SeparatorColor
        {
            get
            {
                return (Color)GetValue(SeparatorColorProperty);
            }
            set
            {
                SetValue(SeparatorColorProperty, value);
            }
        }
        #endregion

        #region Property OverScrollCommand

        /// <summary>
        /// Bindable Property OverScrollCommand
        /// </summary>
        public static readonly BindableProperty OverScrollCommandProperty = BindableProperty.Create(
          nameof(OverScrollCommand),
          typeof(System.Windows.Input.ICommand),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnOverScrollCommandChanged(oldValue,newValue),
          //null,
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
        /// On OverScrollCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnOverScrollCommandChanged(object oldValue, object newValue)
        {
            listView.OverScrollCommand = (ICommand)newValue;
        }


        /// <summary>
        /// Property OverScrollCommand
        /// </summary>
        public System.Windows.Input.ICommand OverScrollCommand
        {
            get
            {
                return (System.Windows.Input.ICommand)GetValue(OverScrollCommandProperty);
            }
            set
            {
                SetValue(OverScrollCommandProperty, value);
            }
        }
        #endregion
        
        #region Property HasUnevenRows

        /// <summary>
        /// Bindable Property HasUnevenRows
        /// </summary>
        public static readonly BindableProperty HasUnevenRowsProperty = BindableProperty.Create(
          nameof(HasUnevenRows),
          typeof(bool),
          typeof(AtomListView),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnHasUnevenRowsChanged(oldValue,newValue),
          //null,
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
        /// On HasUnevenRows changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnHasUnevenRowsChanged(object oldValue, object newValue)
        {
            listView.HasUnevenRows = (bool)newValue;   
        }


        /// <summary>
        /// Property HasUnevenRows
        /// </summary>
        public bool HasUnevenRows
        {
            get
            {
                return (bool)GetValue(HasUnevenRowsProperty);
            }
            set
            {
                SetValue(HasUnevenRowsProperty, value);
            }
        }
        #endregion



        #region Property IsPullToRefreshEnabled

        /// <summary>
        /// Bindable Property IsPullToRefreshEnabled
        /// </summary>
        public static readonly BindableProperty IsPullToRefreshEnabledProperty = BindableProperty.Create(
          nameof(IsPullToRefreshEnabled),
          typeof(bool),
          typeof(AtomListView),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnIsPullToRefreshEnabledChanged(oldValue,newValue),
          //null,
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
        /// On IsPullToRefreshEnabled changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsPullToRefreshEnabledChanged(object oldValue, object newValue)
        {
            listView.IsPullToRefreshEnabled = (bool)newValue;   
        }


        /// <summary>
        /// Property IsPullToRefreshEnabled
        /// </summary>
        public bool IsPullToRefreshEnabled
        {
            get
            {
                return (bool)GetValue(IsPullToRefreshEnabledProperty);
            }
            set
            {
                SetValue(IsPullToRefreshEnabledProperty, value);
            }
        }
        #endregion

        #region Property IsRefreshing

        /// <summary>
        /// Bindable Property IsRefreshing
        /// </summary>
        public static readonly BindableProperty IsRefreshingProperty = BindableProperty.Create(
          nameof(IsRefreshing),
          typeof(bool),
          typeof(AtomListView),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnIsRefreshingChanged(oldValue,newValue),
          //null,
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
        /// On IsRefreshing changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsRefreshingChanged(object oldValue, object newValue)
        {
            listView.IsRefreshing = (bool)newValue;   
        }


        /// <summary>
        /// Property IsRefreshing
        /// </summary>
        public bool IsRefreshing
        {
            get
            {
                return (bool)GetValue(IsRefreshingProperty);
            }
            set
            {
                SetValue(IsRefreshingProperty, value);
            }
        }
        #endregion

        #region Property RefreshCommand

        /// <summary>
        /// Bindable Property RefreshCommand
        /// </summary>
        public static readonly BindableProperty RefreshCommandProperty = BindableProperty.Create(
          nameof(RefreshCommand),
          typeof(System.Windows.Input.ICommand),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnRefreshCommandChanged(oldValue,newValue),
          //null,
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
        /// On RefreshCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnRefreshCommandChanged(object oldValue, object newValue)
        {
            listView.RefreshCommand = (ICommand)newValue;   
        }


        /// <summary>
        /// Property RefreshCommand
        /// </summary>
        public System.Windows.Input.ICommand RefreshCommand
        {
            get
            {
                return (System.Windows.Input.ICommand)GetValue(RefreshCommandProperty);
            }
            set
            {
                SetValue(RefreshCommandProperty, value);
            }
        }
        #endregion

        #region Property RowHeight

        /// <summary>
        /// Bindable Property RowHeight
        /// </summary>
        public static readonly BindableProperty RowHeightProperty = BindableProperty.Create(
          nameof(RowHeight),
          typeof(int),
          typeof(AtomListView),
          -1,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnRowHeightChanged(oldValue,newValue),
          //null,
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
        /// On RowHeight changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnRowHeightChanged(object oldValue, object newValue)
        {
            listView.RowHeight = (int)newValue;   
        }


        /// <summary>
        /// Property RowHeight
        /// </summary>
        public int RowHeight
        {
            get
            {
                return (int)GetValue(RowHeightProperty);
            }
            set
            {
                SetValue(RowHeightProperty, value);
            }
        }
        #endregion






        #region Property ItemsSource

        /// <summary>
        /// Bindable Property ItemsSource
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
          nameof(ItemsSource),
          typeof(System.Collections.IEnumerable),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnItemsSourceChanged(oldValue,newValue),
          //null,
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
                inc.CollectionChanged -= OnItemsChanged;
            }
            if (newValue != null)
            {
                inc = newValue as INotifyCollectionChanged;
                if (inc != null) {
                    inc.CollectionChanged += OnItemsChanged;
                }


                SetItemsSource(newValue as System.Collections.IEnumerable);
            }
            OnItemsChanged(newValue, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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

        #region Property HasSelection

        /// <summary>
        /// Bindable Property HasSelection
        /// </summary>
        public static readonly BindablePropertyKey HasSelectionPropertyKey = BindableProperty.CreateReadOnly(
          "HasSelection",
          typeof(bool),
          typeof(AtomListView),
          false,
          BindingMode.OneWayToSource,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnHasSelectionChanged(oldValue,newValue),
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
        /// 
        /// </summary>
        public static BindableProperty HasSelectionProperty = HasSelectionPropertyKey.BindableProperty;

        /*
        /// <summary>
        /// On HasSelection changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnHasSelectionChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property HasSelection
        /// </summary>
        public bool HasSelection
        {
            get
            {
                return (bool)GetValue(HasSelectionProperty);
            }
            set
            {
                SetValue(HasSelectionProperty, value);
            }
        }
        #endregion



        #region Property HasItems

        /// <summary>
        /// Bindable Property HasItems
        /// </summary>
        private static readonly BindablePropertyKey HasItemsPropertyKey = BindableProperty.CreateReadOnly(
          nameof(HasItems),
          typeof(bool),
          typeof(AtomListView),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnHasItemsChanged(oldValue,newValue),
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
        /// On HasItems changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnHasItemsChanged(object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty HasItemsProperty = HasItemsPropertyKey.BindableProperty;


        /// <summary>
        /// Property HasItems
        /// </summary>
        public bool HasItems
        {
            get
            {
                return (bool)GetValue(HasItemsProperty);
            }
            set
            {
                SetValue(HasItemsPropertyKey, value);
            }
        }
        #endregion



        #region Property ItemIcon

        /// <summary>
        /// Bindable Property ItemIcon
        /// </summary>
        public static readonly BindableProperty ItemIconProperty = BindableProperty.Create(
          nameof(ItemIcon),
          typeof(object),
          typeof(AtomListView),
          AtomStockImages.EmptyCheckBoxImageUrl,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnItemIconChanged(oldValue,newValue),
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
        /// On ItemIcon changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemIconChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemIcon
        /// </summary>
        public object ItemIcon
        {
            get
            {
                return (object)GetValue(ItemIconProperty);
            }
            set
            {
                SetValue(ItemIconProperty, value);
            }
        }
        #endregion

        #region Property SelectedItemIcon

        /// <summary>
        /// Bindable Property SelectedItemIcon
        /// </summary>
        public static readonly BindableProperty SelectedItemIconProperty = BindableProperty.Create(
          nameof(SelectedItemIcon),
          typeof(object),
          typeof(AtomListView),
          AtomStockImages.CheckBoxImageUrl,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnSelectedItemIconChanged(oldValue,newValue),
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
        /// On SelectedItemIcon changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSelectedItemIconChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property SelectedItemIcon
        /// </summary>
        public object SelectedItemIcon
        {
            get
            {
                return (object)GetValue(SelectedItemIconProperty);
            }
            set
            {
                SetValue(SelectedItemIconProperty, value);
            }
        }
        #endregion

        #region Property ItemBackground

        /// <summary>
        /// Bindable Property ItemBackground
        /// </summary>
        public static readonly BindableProperty ItemBackgroundProperty = BindableProperty.Create(
          "ItemBackground",
          typeof(Color),
          typeof(AtomListView),
          Color.Transparent,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnItemBackgroundChanged(oldValue,newValue),
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
        /// On ItemBackground changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemBackgroundChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemBackground
        /// </summary>
        public Color ItemBackground
        {
            get
            {
                return (Color)GetValue(ItemBackgroundProperty);
            }
            set
            {
                SetValue(ItemBackgroundProperty, value);
            }
        }
        #endregion

        #region Property SelectedItemBackground

        /// <summary>
        /// Bindable Property SelectedItemBackground
        /// </summary>
        public static readonly BindableProperty SelectedItemBackgroundProperty = BindableProperty.Create(
          "SelectedItemBackground",
          typeof(Color),
          typeof(AtomListView),
          Color.Accent,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnSelectedItemBackgroundChanged(oldValue,newValue),
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
        /// On SelectedItemBackground changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSelectedItemBackgroundChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property SelectedItemBackground
        /// </summary>
        public Color SelectedItemBackground
        {
            get
            {
                return (Color)GetValue(SelectedItemBackgroundProperty);
            }
            set
            {
                SetValue(SelectedItemBackgroundProperty, value);
            }
        }
        #endregion

        #region Property SelectedItems

        /// <summary>
        /// Bindable Property SelectedItems
        /// </summary>
        public static readonly BindablePropertyKey SelectedItemsPropertyKey = BindableProperty.CreateReadOnly(
          "SelectedItems",
          typeof(IList),
          typeof(AtomListView),
          null,
          BindingMode.OneWayToSource,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnSelectedItemsChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           (s) => ((AtomListView)s).selectedItems
          //null
        );

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty SelectedItemsProperty = SelectedItemsPropertyKey.BindableProperty;

        /*
        /// <summary>
        /// On SelectedItems changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSelectedItemsChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property SelectedItems
        /// </summary>
        public IList SelectedItems
        {
            get
            {
                return (IList)GetValue(SelectedItemsProperty);
            }
            private set
            {
                SetValue(SelectedItemsPropertyKey, value);
            }
        }
        #endregion



        #region Property SelectedItem

        /// <summary>
        /// Bindable Property SelectedItem
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
          nameof(SelectedItem),
          typeof(object),
          typeof(AtomListView),
          null,
          BindingMode.TwoWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnSelectedItemChanged(oldValue,newValue),
          //null,
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
        /// On SelectedItem changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        private void OnSelectedItemChanged(object oldValue, object newValue)
        {
            if (selectedItems.IsChanging)
                return;
            using (selectedItems.BeginEdit()) { 
                //isSelectionChanging = true;
                SelectedItems.Clear();
                if (newValue != null)
                {
                    SelectedItems.Add(newValue);
                }
            }
        }


        /// <summary>
        /// Property SelectedItem
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }
        #endregion



        #region Property TapCommand

        /// <summary>
        /// Bindable Property TapCommand
        /// </summary>
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
          "TapCommand",
          typeof(System.Windows.Input.ICommand),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnTapCommandChanged(oldValue,newValue),
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

        #region Property ItemIconWidth

        /// <summary>
        /// Bindable Property ItemIconWidth
        /// </summary>
        public static readonly BindableProperty ItemIconWidthProperty = BindableProperty.Create(
          "ItemIconWidth",
          typeof(double),
          typeof(AtomListView),
          (double)30,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnItemIconWidthChanged(oldValue,newValue),
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
        /// On ItemIconWidth changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemIconWidthChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemIconWidth
        /// </summary>
        public double ItemIconWidth
        {
            get
            {
                return (double)GetValue(ItemIconWidthProperty);
            }
            set
            {
                SetValue(ItemIconWidthProperty, value);
            }
        }
        #endregion

        #region Property ItemIconMargin

        /// <summary>
        /// Bindable Property ItemIconMargin
        /// </summary>
        public static readonly BindableProperty ItemIconMarginProperty = BindableProperty.Create(
          "ItemIconMargin",
          typeof(Thickness),
          typeof(AtomListView),
          new Thickness(5),
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnItemIconMarginChanged(oldValue,newValue),
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
        /// On ItemIconMargin changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemIconMarginChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemIconMargin
        /// </summary>
        public Thickness ItemIconMargin
        {
            get
            {
                return (Thickness)GetValue(ItemIconMarginProperty);
            }
            set
            {
                SetValue(ItemIconMarginProperty, value);
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
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnItemTemplateChanged(oldValue,newValue),
          //null,
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
        /// On ItemTemplate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemTemplateChanged(object oldValue, object newValue)
        {
            SetItemTemplate(newValue as DataTemplate);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsGroupingEnabled)) {
                SetItemsSource(this.ItemsSource);
            }
        }

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

        #region Property ItemStyleTemplate

        /// <summary>
        /// Bindable Property ItemStyleTemplate
        /// </summary>
        public static readonly BindableProperty ItemStyleTemplateProperty = BindableProperty.Create(
          nameof(ItemStyleTemplate),
          typeof(ControlTemplate),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnItemStyleTemplateChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           (sender) => ((AtomListView)sender).GetDefaultItemStyleTemplate()
          
        );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual ControlTemplate GetDefaultItemStyleTemplate() => null;


        /*
        /// <summary>
        /// On ItemStyleTemplate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemStyleTemplateChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemStyleTemplate
        /// </summary>
        public ControlTemplate ItemStyleTemplate
        {
            get
            {
                return (ControlTemplate)GetValue(ItemStyleTemplateProperty);
            }
            set
            {
                SetValue(ItemStyleTemplateProperty, value);
            }
        }
        #endregion

        #region Property AllowMultipleSelection

        /// <summary>
        /// Bindable Property AllowMultipleSelection
        /// </summary>
        public static readonly BindableProperty AllowMultipleSelectionProperty = BindableProperty.Create(
          nameof(AllowMultipleSelection),
          typeof(bool),
          typeof(AtomListView),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnAllowMultipleSelectionChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          (sender) => ((AtomListView)sender).GetDefaultAllowMultipleSelection()
          
        );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual bool GetDefaultAllowMultipleSelection() => false;

        /*
        /// <summary>
        /// On AllowMultipleSelection changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAllowMultipleSelectionChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AllowMultipleSelection
        /// </summary>
        public bool AllowMultipleSelection
        {
            get
            {
                return (bool)GetValue(AllowMultipleSelectionProperty);
            }
            set
            {
                SetValue(AllowMultipleSelectionProperty, value);
            }
        }
        #endregion

        #region Property TapOnClick

        /// <summary>
        /// Bindable Property TapOnClick
        /// </summary>
        public static readonly BindableProperty TapOnClickProperty = BindableProperty.Create(
          nameof(TapOnClick),
          typeof(bool),
          typeof(AtomListView),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomListView)sender).OnTapOnClickChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          (sender) => ((AtomListView)sender).GetDefaultTapOnClick()
        );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual bool GetDefaultTapOnClick() => true;

        /*
        /// <summary>
        /// On TapOnClick changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTapOnClickChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property TapOnClick
        /// </summary>
        public bool TapOnClick
        {
            get
            {
                return (bool)GetValue(TapOnClickProperty);
            }
            set
            {
                SetValue(TapOnClickProperty, value);
            }
        }
        #endregion

        #region Property IsGroupingEnabled

        /// <summary>
        /// Bindable Property IsGroupingEnabled
        /// </summary>
        public static readonly BindableProperty IsGroupingEnabledProperty = BindableProperty.Create(
          nameof(IsGroupingEnabled),
          typeof(bool),
          typeof(AtomListView),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnIsGroupingEnabledChanged(oldValue,newValue),
          //null,
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
        /// On IsGroupingEnabled changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsGroupingEnabledChanged(object oldValue, object newValue)
        {
            listView.IsGroupingEnabled = (bool)newValue;   
        }


        /// <summary>
        /// Property IsGroupingEnabled
        /// </summary>
        public bool IsGroupingEnabled
        {
            get
            {
                return (bool)GetValue(IsGroupingEnabledProperty);
            }
            set
            {
                SetValue(IsGroupingEnabledProperty, value);
            }
        }
        #endregion

        #region Property GroupHeaderTemplate

        /// <summary>
        /// Bindable Property GroupHeaderTemplate
        /// </summary>
        public static readonly BindableProperty GroupHeaderTemplateProperty = BindableProperty.Create(
          nameof(GroupHeaderTemplate),
          typeof(DataTemplate),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnGroupHeaderTemplateChanged(oldValue,newValue),
          //null,
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
        /// On GroupHeaderTemplate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnGroupHeaderTemplateChanged(object oldValue, object newValue)
        {
            listView.GroupHeaderTemplate = (DataTemplate)newValue;
        }


        /// <summary>
        /// Property GroupHeaderTemplate
        /// </summary>
        public DataTemplate GroupHeaderTemplate
        {
            get
            {
                return (DataTemplate)GetValue(GroupHeaderTemplateProperty);
            }
            set
            {
                SetValue(GroupHeaderTemplateProperty, value);
            }
        }
        #endregion



        #region Property Header

        /// <summary>
        /// Bindable Property Header
        /// </summary>
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(
          nameof(Header),
          typeof(object),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnHeaderChanged(oldValue,newValue),
          //null,
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
        /// On Header changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnHeaderChanged(object oldValue, object newValue)
        {
            listView.Header = newValue;   
        }


        /// <summary>
        /// Property Header
        /// </summary>
        public object Header
        {
            get
            {
                return GetValue(HeaderProperty);
            }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }
        #endregion

        #region Property HeaderTemplate

        /// <summary>
        /// Bindable Property HeaderTemplate
        /// </summary>
        public static readonly BindableProperty HeaderTemplateProperty = BindableProperty.Create(
          nameof(HeaderTemplate),
          typeof(DataTemplate),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnHeaderTemplateChanged(oldValue,newValue),
          //null,
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
        /// On HeaderTemplate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnHeaderTemplateChanged(object oldValue, object newValue)
        {
            listView.HeaderTemplate = (DataTemplate)newValue;   
        }


        /// <summary>
        /// Property HeaderTemplate
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get
            {
                return (DataTemplate)GetValue(HeaderTemplateProperty);
            }
            set
            {
                SetValue(HeaderTemplateProperty, value);
            }
        }
        #endregion



        #region Property Footer

        /// <summary>
        /// Bindable Property Footer
        /// </summary>
        public static readonly BindableProperty FooterProperty = BindableProperty.Create(
          nameof(Footer),
          typeof(object),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnFooterChanged(oldValue,newValue),
          //null,
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
        /// On Footer changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnFooterChanged(object oldValue, object newValue)
        {
            listView.Footer = newValue;
        }


        /// <summary>
        /// Property Footer
        /// </summary>
        public object Footer
        {
            get
            {
                return (object)GetValue(FooterProperty);
            }
            set
            {
                SetValue(FooterProperty, value);
            }
        }
        #endregion



        #region Property FooterTemplate

        /// <summary>
        /// Bindable Property FooterTemplate
        /// </summary>
        public static readonly BindableProperty FooterTemplateProperty = BindableProperty.Create(
          nameof(FooterTemplate),
          typeof(DataTemplate),
          typeof(AtomListView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomListView)sender).OnFooterTemplateChanged(oldValue,newValue),
          //null,
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
        /// On FooterTemplate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnFooterTemplateChanged(object oldValue, object newValue)
        {
            listView.FooterTemplate = (DataTemplate)newValue;   
        }


        /// <summary>
        /// Property FooterTemplate
        /// </summary>
        public DataTemplate FooterTemplate
        {
            get
            {
                return (DataTemplate)GetValue(FooterTemplateProperty);
            }
            set
            {
                SetValue(FooterTemplateProperty, value);
            }
        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        public virtual BindingBase GroupDisplayBinding
        {
            get { return listView.GroupDisplayBinding; }
            set
            {
                listView.GroupDisplayBinding = value;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        protected virtual void SetItemsSource(IEnumerable source)
        {

            listView.ItemsSource = source;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTemplate"></param>
        protected virtual void SetItemTemplate(DataTemplate dataTemplate)
        {
            listView.ItemTemplate = new DataTemplate(()=> {

                var content = dataTemplate.CreateContent();

                var itemStyle = (AtomListViewCell)(ItemStyleTemplate?.CreateContent() ?? new AtomListViewCell());

                itemStyle.Content = (content as View) ?? (content as ViewCell)?.View;

                return new ViewCell {
                    View = itemStyle
                };

            });
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ItemsChanged?.Invoke(this, EventArgs.Empty);

            HasItems = ItemsSource.Cast<object>().Any();

            if (!selectedItems.Any())
                return;
            //UIAtomsApplication.Instance.TriggerOnce(ResetSelections);
            ResetSelections();
        }


        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ItemsChanged;

        private void ResetSelections() {
            var items = ItemsSource;
            using (selectedItems.BeginEdit())
            {
                if (items == null)
                {
                    selectedItems.Clear();
                    return;
                }

                var ie = items.Cast<object>();

                foreach (var item in selectedItems.ToList())
                {
                    if (!ie.Contains(item))
                    {
                        selectedItems.Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectionChanged;

        private void SelectedItems_CollectionChanged(object sender, 
            NotifyCollectionChangedEventArgs e)
        {
            this.SetValue(HasSelectionPropertyKey, selectedItems.Any());
            SetValue(SelectedItemProperty, selectedItems.FirstOrDefault());
            SelectionChanged?.Invoke(this, EventArgs.Empty);

            EnsureSelectionVisible();

        }

        protected virtual void EnsureSelectionVisible()
        {
            if (!AutoScrollOnSelection)
                return;
            var notVisible = selectedItems.Where(x => !listView.VisibleItems.Contains(x)).FirstOrDefault();
            if (notVisible != null)
            {
                listView.ScrollTo(notVisible, ScrollToPosition.MakeVisible, false);
            }
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        internal protected virtual void OnItemTapped(object item) {

            if (TapOnClick) {

                // toggle selection....

                SetIsSelected(item, !IsSelected(item));
            }
            TapCommand?.Execute(item);
        }

        internal bool IsSelected(object item) =>
            selectedItems.Contains(item);

        //private bool isSelectionChanging = false;

        internal void SetIsSelected(object item, bool newValue)
        {
            using (selectedItems.BeginEdit())
            {
                if (newValue)
                {
                    if (AllowMultipleSelection)
                    {
                        selectedItems.Add(item);
                    }
                    else
                    {
                        if (selectedItems.Count == 1)
                        {
                            selectedItems[0] = item;
                        }
                        else
                        {
                            selectedItems.Add(item);
                        }
                    }
                }
                else
                {

                    if (selectedItems.Count > 1 || AllowMultipleSelection)
                    {
                        selectedItems.Remove(item);
                    }
                }
            }
        }
    }




    


    /// <summary>
    /// 
    /// </summary>
    public class AtomListViewCell : AtomContentGrid, IAtomListItem
    {

        #region Property IsSelected

        /// <summary>
        /// Bindable Property IsSelected
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
          "IsSelected",
          typeof(bool),
          typeof(AtomListViewCell),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender, oldValue, newValue) => ((AtomListViewCell)sender).OnIsSelectedChanged(oldValue, newValue),
          //null,
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
        /// On IsSelected changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsSelectedChanged(object oldValue, object newValue)
        {
            this.BackgroundColor = ((bool)newValue) ? OwnerListView.SelectedItemBackground : OwnerListView.ItemBackground;

            if (isSelectionChanging)
                return;
            OwnerListView.SetIsSelected(BindingContext, (bool)newValue);
        }


        /// <summary>
        /// Property IsSelected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }
        #endregion

        

        #region Property OwnerListView

        /// <summary>
        /// Bindable Property OwnerListView
        /// </summary>
        private static readonly BindablePropertyKey OwnerListViewPropertyKey = BindableProperty.CreateReadOnly(
          nameof(OwnerListView),
          typeof(AtomListView),
          typeof(AtomListViewCell),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomNavigatorViewCell)sender).OnOwnerListViewChanged(oldValue,newValue),
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
        /// 
        /// </summary>
        public static readonly BindableProperty OwnerListViewProperty = OwnerListViewPropertyKey.BindableProperty;

        /*
        /// <summary>
        /// On OwnerListView changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnOwnerListViewChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property OwnerListView
        /// </summary>
        public AtomListView OwnerListView
        {
            get
            {
                return (AtomListView)GetValue(OwnerListViewProperty);
            }
            private set
            {
                SetValue(OwnerListViewPropertyKey, value);
            }
        }
        #endregion

        

        /// <summary>
        /// 
        /// </summary>
        public ICommand ToggleSelectionCommand { get; }


        /// <summary>
        /// 
        /// </summary>
        public AtomListViewCell()
        {
            ToggleSelectionCommand = new AtomCommand(() =>
            {
                this.IsSelected = !this.IsSelected;
                return Task.CompletedTask;
            });

            tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = new AtomCommand(() =>
                {
                    this.OwnerListView?.OnItemTapped(this.BindingContext);
                    return Task.CompletedTask;
                })
            };

            /*GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new AtomCommand(()=> {
                    OwnerListView.OnItemTapped(this.BindingContext);
                })
            });*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnContentChanged(object oldValue, object newValue)
        {
            (oldValue as View)?.GestureRecognizers.Remove(tapGestureRecognizer);

            base.OnContentChanged(oldValue, newValue);

            
            (newValue as View)?.GestureRecognizers.Add(tapGestureRecognizer);

        }

        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();

        //    if (this.Content != null) {
        //        this.Content.BindingContext = this.BindingContext;
        //    }
        //}

        private bool isSelectionChanging = false;
        private TapGestureRecognizer tapGestureRecognizer;

        void IAtomListItem.OnAppearing(AtomListView owner)
        {
            OwnerListView = owner;
            //SelectedBackgroundColor = owner.SelectedItemBackground;
            // get selection...
            //BackgroundColor = owner.ItemBackground;
            try
            {
                isSelectionChanging = true;
                IsSelected = owner.IsSelected(BindingContext);
                BackgroundColor = IsSelected ? owner.SelectedItemBackground : owner.ItemBackground;
            }
            finally
            {
                isSelectionChanging = false;
            }
            owner.SelectionChanged += Owner_SelectionChanged;
        }

        void IAtomListItem.OnDisappearing(AtomListView owner)
        {
            owner.SelectionChanged -= Owner_SelectionChanged;
            OwnerListView = null;
        }

        private void Owner_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                isSelectionChanging = true;
                IsSelected = OwnerListView.IsSelected(this.BindingContext);
            }
            finally {
                isSelectionChanging = false;
            }
        }
    }





}
