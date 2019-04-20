using NeuroSpeech.UIAtoms.DI;
using NeuroSpeech.UIAtoms.Pages;
using NeuroSpeech.UIAtoms.Validation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomComboBox: Frame
    {

        static readonly AtomPropertyValidator DefaultValidator;


        static AtomComboBox() {
             DefaultValidator = new AtomPropertyValidator
             {
                 BindableProperty = SelectedItemProperty,
                 ValidationRule = AtomUtils.Singleton<AtomSelectionValidationRule>()
             };
        }

        /// <summary>
        /// 
        /// </summary>
        public AtomComboBox()
        {

            AtomForm.SetValidator(this, DefaultValidator);

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new AtomCommand(async () => await OnContentTappedCommand())
            });

            this.ItemTemplate = new DataTemplate(typeof(AtomLabelTemplate));

            this.Padding = new Thickness(5);
            this.BorderColor = Color.Accent;

            OnSelectedItemChanged(null, null);
        }

        #region Property ItemsSource

        /// <summary>
        /// Bindable Property ItemsSource
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
          "ItemsSource",
          typeof(System.Collections.IEnumerable),
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
           // property changed, delegate
           (sender, oldValue, newValue) => ((AtomComboBox)sender).OnItemsSourceChanged(oldValue, newValue),
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
                inc.CollectionChanged -= Inc_CollectionChanged;
            }
            inc = newValue as INotifyCollectionChanged;
            if (inc != null) {
                inc.CollectionChanged += Inc_CollectionChanged;
            }
            Inc_CollectionChanged(null, null);
        }

        private void Inc_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UIAtomsApplication.Instance.TriggerOnce(SetSelectedItemFromValue);
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
          typeof(AtomComboBox),
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


        #region Property SelectCommand

        /// <summary>
        /// Bindable Property SelectCommand
        /// </summary>
        public static readonly BindableProperty SelectCommandProperty = BindableProperty.Create(
          "SelectCommand",
          typeof(System.Windows.Input.ICommand),
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomComboBox)sender).OnSelectCommandChanged(oldValue,newValue),
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

        #region Property SelectedItem

        /// <summary>
        /// Bindable Property SelectedItem
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
          "SelectedItem",
          typeof(object),
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
           (sender,oldValue,newValue) => ((AtomComboBox)sender).OnSelectedItemChanged(oldValue,newValue),
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
        protected virtual void OnSelectedItemChanged(object oldValue, object newValue)
        {
            SetValueFromSelectedItem();
            if (newValue == null)
            {
                this.Content = new Label { };
                Content.SetBinding(Label.TextProperty, new Binding { Source = this, Path = "EmptyLabel" });
            }
            else
            {
                var view = ItemTemplate.CreateContent() as View;
                if (view == null)
                {
                    throw new InvalidOperationException("ItemTemplate must be derived from View and not Cell");
                }
                view.BindingContext = newValue;
                this.Content = view;
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

        #region Property EmptyLabel

        /// <summary>
        /// Bindable Property EmptyLabel
        /// </summary>
        public static readonly BindableProperty EmptyLabelProperty = BindableProperty.Create(
          "EmptyLabel",
          typeof(string),
          typeof(AtomComboBox),
          "Choose Item",
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
        /// On EmptyLabel changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnEmptyLabelChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property EmptyLabel
        /// </summary>
        public string EmptyLabel
        {
            get
            {
                return (string)GetValue(EmptyLabelProperty);
            }
            set
            {
                SetValue(EmptyLabelProperty, value);
            }
        }
        #endregion

        #region Property AddNew

        /// <summary>
        /// Bindable Property AddNew
        /// </summary>
        public static readonly BindableProperty AddNewProperty = BindableProperty.Create(
          "AddNew",
          typeof(Func<object>),
          typeof(AtomComboBox),
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

        #region Property Filter

        /// <summary>
        /// Bindable Property Filter
        /// </summary>
        public static readonly BindableProperty FilterProperty = BindableProperty.Create(
          "Filter",
          typeof(Func<object,string,bool>),
          typeof(AtomComboBox),
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

        #region Property FilterPath

        /// <summary>
        /// Bindable Property FilterPath
        /// </summary>
        public static readonly BindableProperty FilterPathProperty = BindableProperty.Create(
          "FilterPath",
          typeof(string),
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
           (sender,oldValue,newValue) => ((AtomComboBox)sender).OnFilterPathChanged(oldValue,newValue),
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
        /// On FilterPath changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnFilterPathChanged(object oldValue, object newValue)
        {
            string path = newValue as string;
            if (path != null)
            {
                Filter = (a,s) => {
                    if (a == null)
                        return false;
                    var pl = path.Split(new char[] { ',' });
                    Type type = a.GetType();
                    foreach (var p in pl)
                    {
                        var prop = type.GetProperty(p);
                        if (prop == null)
                            continue;
                        var value = prop.GetValue(a);
                        if (value == null)
                            continue;
                        if (value.ToString().StartsWith(s, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                    }
                    return false;
                };
            }
        }


        /// <summary>
        /// Property FilterPath
        /// </summary>
        public string FilterPath
        {
            get
            {
                return (string)GetValue(FilterPathProperty);
            }
            set
            {
                SetValue(FilterPathProperty, value);
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
          typeof(AtomComboBox),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomComboBox)sender).OnIsGroupingEnabledChanged(oldValue,newValue),
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

        #region Property GroupDisplayBinding

        /// <summary>
        /// Bindable Property GroupDisplayBinding
        /// </summary>
        public static readonly BindableProperty GroupDisplayBindingProperty = BindableProperty.Create(
          "GroupDisplayBinding",
          typeof(BindingBase),
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomComboBox)sender).OnGroupDisplayBindingChanged(oldValue,newValue),
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

        #region Property GroupHeaderTemplate

        /// <summary>
        /// Bindable Property GroupHeaderTemplate
        /// </summary>
        public static readonly BindableProperty GroupHeaderTemplateProperty = BindableProperty.Create(
          "GroupHeaderTemplate",
          typeof(DataTemplate),
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomComboBox)sender).OnGroupHeaderTemplateChanged(oldValue,newValue),
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
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomComboBox)sender).OnGroupShortNameBindingChanged(oldValue,newValue),
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




        private async Task OnContentTappedCommand()
        {
            try
            {
                if (ItemsSource == null)
                {
                    await UIAtomsApplication.Instance.NotificationService.NotifyAsync("No items to show");
                    return;
                }

                INavigation nav = DependencyService.Get<INavigation>(DependencyFetchTarget.GlobalInstance);

                ItemSelectorPage selector = new ItemSelectorPage();
                selector.AddNew = AddNew;
                TaskCompletionSource<object> s = new TaskCompletionSource<object>();

                selector.CompletionSource = s;
                selector.ItemTemplate = this.ItemTemplate;
                selector.GroupDisplayBinding = this.GroupDisplayBinding;
                selector.GroupHeaderTemplate = this.GroupHeaderTemplate;
                selector.IsGroupingEnabled = this.IsGroupingEnabled;
                selector.GroupShortNameBinding = this.GroupShortNameBinding;

                // for performance reasons, set ItemsSource at the end..
                selector.ItemsSource = this.ItemsSource;

                Func<object, string, bool> f = Filter;
                if (f == null)
                {
                    f = (item, text) =>
                    {
                        return item.ToString().StartsWith(text);
                    };
                }

                selector.Filter = f;
                var si = selector.SelectedItem = this.SelectedItem;
                await nav.PushModalAsync(selector);
                var r = await s.Task;
                if (r != null && r != si)
                {
                    this.SelectedItem = r;

                    this.SelectCommand?.Execute(r);

                }
            }
            catch (TaskCanceledException) {                
            }
        }

        #region Property Value

        /// <summary>
        /// Bindable Property Value
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
          "Value",
          typeof(object),
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
           (sender,oldValue,newValue) => ((AtomComboBox)sender).OnValueChanged(oldValue,newValue),
          
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
        /// On Value changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnValueChanged(object oldValue, object newValue)
        {
            SetSelectedItemFromValue();
        }


        /// <summary>
        /// Property Value
        /// </summary>
        public object Value
        {
            get
            {
                return (object)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
        #endregion

        #region Property ValuePath

        /// <summary>
        /// Bindable Property ValuePath
        /// </summary>
        public static readonly BindableProperty ValuePathProperty = BindableProperty.Create(
          "ValuePath",
          typeof(string),
          typeof(AtomComboBox),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomComboBox)sender).OnValuePathChanged(oldValue,newValue),
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
        /// On ValuePath changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnValuePathChanged(object oldValue, object newValue)
        {
            if (newValue != null) {
                if (Value != null)
                {
                    SetSelectedItemFromValue();
                }
                else {
                    if (SelectedItem != null) {
                        SetValueFromSelectedItem();
                    }
                }
            }    
        }


        /// <summary>
        /// Property ValuePath
        /// </summary>
        public string ValuePath
        {
            get
            {
                return (string)GetValue(ValuePathProperty);
            }
            set
            {
                SetValue(ValuePathProperty, value);
            }
        }
        #endregion




        bool selectionRunning = false;
        private void SetSelectedItemFromValue() {
            if (selectionRunning)
                return;
            selectionRunning = true;
            try
            {
                var newValue = Value;
                string valuePath = ValuePath;
                var items = ItemsSource;
                if (valuePath == null || items == null || newValue == null)
                    return;
                var currentSelection = SelectedItem;
                foreach (var obj in items)
                {
                    object v = obj.GetPropertyValue(valuePath);
                    if (v!=null && v.Equals(newValue))
                    {
                        if (currentSelection==null || !currentSelection.Equals(obj))
                        {
                            SelectedItem = obj;
                        }
                        return;
                    }
                }
            }
            finally {
                selectionRunning = false;
            }
        }

        private void SetValueFromSelectedItem()
        {
            if (selectionRunning)
                return;
            selectionRunning = true;
            try
            {
                var newValue = SelectedItem;

                string valuePath = ValuePath;
                var items = ItemsSource;
                if (valuePath == null || items == null || newValue == null)
                    return;

                object currentValue = Value;

                foreach (var obj in items)
                {
                    if (obj == newValue)
                    {

                        object v = obj.GetPropertyValue(valuePath);
                        if (currentValue != v)
                        {
                            Value = v;
                        }
                        return;
                    }

                }
            }
            finally {
                selectionRunning = false;
            }
            
        }



    }

    /// <summary>
    /// 
    /// </summary>
    public class AtomLabelTemplate : Label {

        /// <summary>
        /// 
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            this.Text = BindingContext?.ToString();
        }

    }
}
