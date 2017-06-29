using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Pages
{
	public partial class ItemSelectorPage : ContentPage
	{
		public ItemSelectorPage ()
		{
			InitializeComponent ();

            nav = DependencyService.Get<INavigation>(DependencyFetchTarget.GlobalInstance);


            this.BindingContext = this;

            selectButton.Clicked += async (s, e) =>  {
                if (valueSelected)
                    return;
                valueSelected = true;
                INavigation nav = DependencyService.Get<INavigation>(DependencyFetchTarget.GlobalInstance);
                await nav.PopModalAsync();
                CompletionSource.SetResult(SelectedItem);
            };

            cancelButton.Clicked += async (s, e) => {
                if (valueSelected)
                    return;
                valueSelected = true;
                await nav.PopModalAsync();
                CompletionSource.SetResult(previousSelection);
            };

            searchBar.TextChanged += (s, e) => {
                FilterItems();
            };

            Device.BeginInvokeOnMainThread(async () => {
                await Task.Delay(1);
                FilterItems();
                previousSelection = SelectedItem;
                if (ItemTemplate != null)
                {
                    listView.ItemTemplate = new DataTemplate(() =>
                    {
                        var c = ItemTemplate.CreateContent() as View;
                        return new ViewCell { View = c };
                    });
                }


                /* we don't want to look for ItemSelected event immediately
                 because SelectedItem is set by binding framework after initialization,
                 there is no way to detect human selection over default selection*/
                await Task.Delay(100);

                

                listView.ItemSelected += async (s, e) => {
                    if (valueSelected)
                        return;
                    valueSelected = true;
                    // delay to display little animation...
                    await Task.Delay(500);
                    await nav.PopModalAsync(true);
                    CompletionSource.SetResult(e.SelectedItem);
                };

            });

            if (AddNew != null)
            {
                selectButton.IsVisible = true;
                Grid.SetColumnSpan(cancelButton, 1);
            }
            else {
                selectButton.IsVisible = false;
                Grid.SetColumnSpan(cancelButton, 2);
            }

            


        }

        protected override void OnDisappearing()
        {
            if (!valueSelected) {
                CompletionSource.TrySetCanceled();
            }
            base.OnDisappearing();
        }

        private bool valueSelected = false;

        private object previousSelection;

        #region Property AddNew

        /// <summary>
        /// Bindable Property AddNew
        /// </summary>
        public static readonly BindableProperty AddNewProperty = BindableProperty.Create(
          "AddNew",
          typeof(Func<object>),
          typeof(ItemSelectorPage),
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
        /// On AddNew changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAddNewChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AddNew
        /// </summary>
        public Func<object> AddNew
        {
            get
            {
                return (Func<object>)GetValue(AddNewProperty);
            }
            set
            {
                SetValue(AddNewProperty, value);
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
          typeof(ItemSelectorPage),
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

        #region Property ItemsSource

        /// <summary>
        /// Bindable Property ItemsSource
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
          "ItemsSource",
          typeof(System.Collections.IEnumerable),
          typeof(ItemSelectorPage),
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
        /// On ItemsSource changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemsSourceChanged(object oldValue, object newValue)
        {
            
        }*/


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

        #region Property SelectedItem

        /// <summary>
        /// Bindable Property SelectedItem
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
          "SelectedItem",
          typeof(object),
          typeof(ItemSelectorPage),
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
        private INavigation nav;

        /*
        /// <summary>
        /// On SelectedItem changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSelectedItemChanged(object oldValue, object newValue)
        {
            
        }*/


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



        #region Property FilteredItems

        /// <summary>
        /// Bindable Property FilteredItems
        /// </summary>
        public static readonly BindableProperty FilteredItemsProperty = BindableProperty.Create(
          "FilteredItems",
          typeof(System.Collections.IEnumerable),
          typeof(ItemSelectorPage),
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
        /// On FilteredItems changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnFilteredItemsChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property FilteredItems
        /// </summary>
        public System.Collections.IEnumerable FilteredItems
        {
            get
            {
                return (System.Collections.IEnumerable)GetValue(FilteredItemsProperty);
            }
            set
            {
                SetValue(FilteredItemsProperty, value);
            }
        }
        #endregion

        #region Property Filter

        /// <summary>
        /// Bindable Property Filter
        /// </summary>
        public static readonly BindableProperty FilterProperty = BindableProperty.Create(
          "Filter",
          typeof(Func<object,string,bool>),
          typeof(ItemSelectorPage),
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
        /// On Filter changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnFilterChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Filter
        /// </summary>
        public Func<object,string,bool> Filter
        {
            get
            {
                return (Func<object,string,bool>)GetValue(FilterProperty);
            }
            set
            {
                SetValue(FilterProperty, value);
            }
        }
        #endregion


        #region Property GroupDisplayBinding

        /// <summary>
        /// Bindable Property GroupDisplayBinding
        /// </summary>
        public static readonly BindableProperty GroupDisplayBindingProperty = BindableProperty.Create(
          "GroupDisplayBinding",
          typeof(BindingBase),
          typeof(ItemSelectorPage),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((ItemSelectorPage)sender).OnGroupDisplayBindingChanged(oldValue,newValue),
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
        /// On GroupDisplayBinding changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnGroupDisplayBindingChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property GroupDisplayBinding
        /// </summary>
        public BindingBase GroupDisplayBinding
        {
            get
            {
                return (BindingBase)GetValue(GroupDisplayBindingProperty);
            }
            set
            {
                SetValue(GroupDisplayBindingProperty, value);
            }
        }
        #endregion

        #region Property IsGroupingEnabled

        /// <summary>
        /// Bindable Property IsGroupingEnabled
        /// </summary>
        public static readonly BindableProperty IsGroupingEnabledProperty = BindableProperty.Create(
          "IsGroupingEnabled",
          typeof(bool),
          typeof(ItemSelectorPage),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((ItemSelectorPage)sender).OnIsGroupingEnabledChanged(oldValue,newValue),
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
        /// On IsGroupingEnabled changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsGroupingEnabledChanged(object oldValue, object newValue)
        {
            
        }*/


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
          "GroupHeaderTemplate",
          typeof(DataTemplate),
          typeof(ItemSelectorPage),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((ItemSelectorPage)sender).OnGroupHeaderTemplateChanged(oldValue,newValue),
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
        /// On GroupHeaderTemplate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnGroupHeaderTemplateChanged(object oldValue, object newValue)
        {
            
        }*/


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

        #region Property GroupShortNameBinding

        /// <summary>
        /// Bindable Property GroupShortNameBinding
        /// </summary>
        public static readonly BindableProperty GroupShortNameBindingProperty = BindableProperty.Create(
          "GroupShortNameBinding",
          typeof(BindingBase),
          typeof(ItemSelectorPage),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((ItemSelectorPage)sender).OnGroupShortNameBindingChanged(oldValue,newValue),
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
        /// On GroupShortNameBinding changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnGroupShortNameBindingChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property GroupShortNameBinding
        /// </summary>
        public BindingBase GroupShortNameBinding
        {
            get
            {
                return (BindingBase)GetValue(GroupShortNameBindingProperty);
            }
            set
            {
                SetValue(GroupShortNameBindingProperty, value);
            }
        }
        #endregion




        public TaskCompletionSource<object> CompletionSource { get; set; }



        private void FilterItems()
        {
            string text = searchBar.Text;

            if ( string.IsNullOrWhiteSpace(text) || Filter == null)
            {
                FilteredItems = ItemsSource;
            }
            else
            {
                FilteredItems = ItemsSource.Cast<object>().Where(x => x != null && Filter(x, text)).ToList();
            }
        }
    }
}
