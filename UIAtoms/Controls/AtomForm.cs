using NeuroSpeech.UIAtoms.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [ContentProperty("Items")]
    public class AtomForm : ContentView
    {

        static AtomForm() {
            IsValidProperty = IsValidPropertyKey.BindableProperty;
        }




        #region Attached Properties

        #region Label Attached Property
        /// <summary>
        /// Label Attached property
        /// </summary>
        public static readonly BindableProperty LabelProperty =
            BindableProperty.CreateAttached("Label", typeof(string),
            typeof(AtomForm),
            null,
            BindingMode.OneWay,
            null,
            null);

        /*private static void OnLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }*/

        /// <summary>
        /// Set Label for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetLabel(BindableObject bindable, string newValue)
        {
            bindable.SetValue(LabelProperty, newValue);
        }

        /// <summary>
        /// Get Label for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetLabel(BindableObject bindable)
        {
            return (string)bindable.GetValue(LabelProperty);
        }
        #endregion

        #region LabelColor Attached Property
        /// <summary>
        /// LabelColor Attached property
        /// </summary>
        public static readonly BindableProperty LabelColorProperty =
            BindableProperty.CreateAttached("LabelColor", typeof(Color),
            typeof(Color),
            Color.Default,
            BindingMode.OneWay,
            null,
            null);//OnLabelColorChanged);

        /*private static void OnLabelColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// Set LabelColor for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetLabelColor(BindableObject bindable, Color newValue)
        {
            bindable.SetValue(LabelColorProperty, newValue);
        }

        /// <summary>
        /// Get LabelColor for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static Color GetLabelColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(LabelColorProperty);
        }
        #endregion

        #region Description Attached Property
        /// <summary>
        /// Description Attached property
        /// </summary>
        public static readonly BindableProperty DescriptionProperty =
            BindableProperty.CreateAttached("Description", typeof(object),
            typeof(AtomForm),
            null,
            BindingMode.OneWay,
            null,
            null);//OnDescriptionChanged);

        /*private static void OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// Set Description for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetDescription(BindableObject bindable, object newValue)
        {
            bindable.SetValue(DescriptionProperty, newValue);
        }

        /// <summary>
        /// Get Description for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static object GetDescription(BindableObject bindable)
        {
            return (object)bindable.GetValue(DescriptionProperty);
        }
        #endregion

        #region Error Attached Property
        /// <summary>
        /// Error Attached property
        /// </summary>
        public static readonly BindableProperty ErrorProperty =
            BindableProperty.CreateAttached("Error", typeof(string),
            typeof(AtomForm),
            null,
            BindingMode.OneWay,
            null,
            null);//OnErrorChanged);

        /*private static void OnErrorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// Set Error for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetError(BindableObject bindable, string newValue)
        {
            bindable.SetValue(ErrorProperty, newValue);
        }

        /// <summary>
        /// Get Error for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetError(BindableObject bindable)
        {
            return (string)bindable.GetValue(ErrorProperty);
        }
        #endregion

        #region Warning Attached Property
        /// <summary>
        /// Warning Attached property
        /// </summary>
        public static readonly BindableProperty WarningProperty =
            BindableProperty.CreateAttached("Warning", typeof(string),
            typeof(AtomForm),
            null,
            BindingMode.OneWay,
            null,
            null);//OnWarningChanged);

        /*private static void OnWarningChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// Set Warning for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetWarning(BindableObject bindable, string newValue)
        {
            bindable.SetValue(WarningProperty, newValue);
        }

        /// <summary>
        /// Get Warning for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetWarning(BindableObject bindable)
        {
            return (string)bindable.GetValue(WarningProperty);
        }
        #endregion

        #region IsRequired Attached Property
        /// <summary>
        /// IsRequired Attached property
        /// </summary>
        public static readonly BindableProperty IsRequiredProperty =
            BindableProperty.CreateAttached("IsRequired", typeof(bool),
            typeof(AtomForm),
            false,
            BindingMode.OneWay,
            null,
            null);//OnIsRequiredChanged);

        /*private static void OnIsRequiredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// Set IsRequired for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetIsRequired(BindableObject bindable, bool newValue)
        {
            bindable.SetValue(IsRequiredProperty, newValue);
        }

        /// <summary>
        /// Get IsRequired for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static bool GetIsRequired(BindableObject bindable)
        {
            return (bool)bindable.GetValue(IsRequiredProperty);
        }
        #endregion

        #region MissingValueMessage Attached Property
        /// <summary>
        /// MissingValueMessage Attached property
        /// </summary>
        public static readonly BindableProperty MissingValueMessageProperty =
            BindableProperty.CreateAttached("MissingValueMessage", typeof(string),
            typeof(AtomForm),
            "Field is required",
            BindingMode.OneWay,
            null,
            null);//OnMissingValueMessageChanged);

        /*private static void OnMissingValueMessageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// Set MissingValueMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetMissingValueMessage(BindableObject bindable, string newValue)
        {
            bindable.SetValue(MissingValueMessageProperty, newValue);
        }

        /// <summary>
        /// Get MissingValueMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetMissingValueMessage(BindableObject bindable)
        {
            return (string)bindable.GetValue(MissingValueMessageProperty);
        }
        #endregion

        #region InvalidValueMessage Attached Property
        /// <summary>
        /// InvalidValueMessage Attached property
        /// </summary>
        public static readonly BindableProperty InvalidValueMessageProperty =
            BindableProperty.CreateAttached("InvalidValueMessage", typeof(string),
            typeof(AtomForm),
            "Invalid",
            BindingMode.OneWay,
            null,
            null);//OnInvalidValueMessageChanged);

        /*private static void OnInvalidValueMessageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }*/

        /// <summary>
        /// Set InvalidValueMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetInvalidValueMessage(BindableObject bindable, string newValue)
        {
            bindable.SetValue(InvalidValueMessageProperty, newValue);
        }

        /// <summary>
        /// Get InvalidValueMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetInvalidValueMessage(BindableObject bindable)
        {
            return (string)bindable.GetValue(InvalidValueMessageProperty);
        }
        #endregion

        #region Validator Attached Property
        /// <summary>
        /// Validator Attached property
        /// </summary>
        public static readonly BindableProperty ValidatorProperty =
            BindableProperty.CreateAttached("Validator", typeof(AtomPropertyValidator),
            typeof(AtomForm),
            null,
            BindingMode.OneWay,
            null,
           OnValidatorChanged);

        private static void OnValidatorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AtomPropertyValidator v = newValue as AtomPropertyValidator;
            if (v == null)
                return;
            bindable.PropertyChanged += (s, e) => {
                var error = AtomForm.GetError(bindable);
                if (string.IsNullOrWhiteSpace(error))
                    return;
                if (e.PropertyName == v.BindableProperty?.PropertyName || e.PropertyName == v.Property) {
                    AtomValidationRule.Validate(bindable as View);
                }
            };
        }

        /// <summary>
        /// Set Validator for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetValidator(BindableObject bindable, AtomPropertyValidator newValue)
        {
            bindable.SetValue(ValidatorProperty, newValue);
        }

        /// <summary>
        /// Get Validator for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static AtomPropertyValidator GetValidator(BindableObject bindable)
        {
            return (AtomPropertyValidator)bindable.GetValue(ValidatorProperty);
        }
        #endregion

        #region FocusNext Attached Property
        /// <summary>
        /// FocusNext Attached property
        /// </summary>
        public static readonly BindableProperty FocusNextProperty =
            BindableProperty.CreateAttached("FocusNext", typeof(View),
            typeof(AtomForm),
            null,
            BindingMode.OneWay,
            null,
            null);

        //private static void OnFocusNextChanged(BindableObject bindable, object oldValue, object newValue)
        //{

        //}

        /// <summary>
        /// Set FocusNext for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetFocusNext(BindableObject bindable, View newValue)
        {
            bindable.SetValue(FocusNextProperty, newValue);
        }

        /// <summary>
        /// Get FocusNext for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static View GetFocusNext(BindableObject bindable)
        {
            return (View)bindable.GetValue(FocusNextProperty);
        }
        #endregion

        #region Category Attached Property
        /// <summary>
        /// Category Attached property
        /// </summary>
        public static readonly BindableProperty CategoryProperty =
            BindableProperty.CreateAttached("Category", typeof(string),
            typeof(AtomForm),
            null,
            BindingMode.OneWay,
            null,
            null);// OnCategoryChanged);

        /*private static void OnCategoryChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }*/

        /// <summary>
        /// Set Category for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetCategory(BindableObject bindable, string newValue)
        {
            bindable.SetValue(CategoryProperty, newValue);
        }

        /// <summary>
        /// Get Category for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetCategory(BindableObject bindable)
        {
            return (string)bindable.GetValue(CategoryProperty);
        }
        #endregion

        #endregion



        #region Property FieldStyle

        /// <summary>
        /// Bindable Property FieldStyle
        /// </summary>
        public static readonly BindableProperty FieldStyleProperty = BindableProperty.Create(
          nameof(FieldStyle),
          typeof(ControlTemplate),
          typeof(AtomForm),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomForm)sender).OnFieldStyleChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          (sender) => new ControlTemplate(typeof(AtomFieldTemplate))
          //null
        );

        /*
        /// <summary>
        /// On FieldStyle changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnFieldStyleChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property FieldStyle
        /// </summary>
        public ControlTemplate FieldStyle
        {
            get
            {
                return (ControlTemplate)GetValue(FieldStyleProperty);
            }
            set
            {
                SetValue(FieldStyleProperty, value);
            }
        }
        #endregion





        /// <summary>
        /// 
        /// </summary>
        public AtomForm()
        {
            Items = new FieldCollection<AtomFieldGroup>(this);
            this.ValidateCommand = new AtomCommand(OnValidateCommand);

            Content = listView = new AtomFormItemsControl(this);
            // UpdateItems();

            this.listView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        }        


        /// <summary>
        /// 
        /// </summary>
        protected Task OnValidateCommand(Object parameter) {

            UIAtomsApplication.Instance.TriggerOnce(() =>
            {

                UpdateIsValid();
                if (IsValid)
                {
                    SubmitCommand?.Execute(parameter ?? BindingContext);
                }
                else
                {
                    UIAtomsApplication.Instance.NotificationService.NotifyAsync(InvalidMessage);
                }
            });
            return Task.CompletedTask;
        }


        private readonly AtomFormItemsControl listView;
        //private SearchBar searchBar;

        internal void UpdateItems() {
            UIAtomsApplication.Instance.TriggerOnce(() =>
            {
                try
                {
                    // listView.ItemsSource = null;
                    if (Items.Count == 1 && HideSingleGroup)
                    {
                        //if (listView.IsGroupingEnabled)
                        //    listView.IsGroupingEnabled = false;
                        if (listView.IsGrouped)
                            listView.IsGrouped = false;
                        listView.ItemsSource = (Items as FieldCollection<AtomFieldGroup>).AllFields(true);
                    }
                    else
                    {
                        //if(!listView.IsGroupingEnabled)
                        //    listView.IsGroupingEnabled = true;
                        if (!listView.IsGrouped)
                            listView.IsGrouped = true;
                        listView.ItemsSource = (Items as FieldCollection<AtomFieldGroup>).AllFields();
                    }
                    // listView.Refresh();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }, TimeSpan.FromMilliseconds(500));
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ValidateCommand { get; }


        #region Property SubmitCommand

        /// <summary>
        /// Bindable Property SubmitCommand
        /// </summary>
        public static readonly BindableProperty SubmitCommandProperty = BindableProperty.Create(
          "SubmitCommand",
          typeof(ICommand),
          typeof(AtomForm),
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
        /// On SubmitCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSubmitCommandChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property SubmitCommand
        /// </summary>
        public ICommand SubmitCommand
        {
            get
            {
                return (ICommand)GetValue(SubmitCommandProperty);
            }
            set
            {
                SetValue(SubmitCommandProperty, value);
            }
        }
        #endregion



        #region Property HideSingleGroup

        /// <summary>
        /// Bindable Property HideSingleGroup
        /// </summary>
        public static readonly BindableProperty HideSingleGroupProperty = BindableProperty.Create(
          nameof(HideSingleGroup),
          typeof(bool),
          typeof(AtomForm),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomForm)sender).OnHideSingleGroupChanged(oldValue,newValue),
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
        /// On HideSingleGroup changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnHideSingleGroupChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property HideSingleGroup
        /// </summary>
        public bool HideSingleGroup
        {
            get
            {
                return (bool)GetValue(HideSingleGroupProperty);
            }
            set
            {
                SetValue(HideSingleGroupProperty, value);
            }
        }
        #endregion




        #region Property ShowSearch

        /// <summary>
        /// Bindable Property ShowSearch
        /// </summary>
        public static readonly BindableProperty ShowSearchProperty = BindableProperty.Create(
          "ShowSearch",
          typeof(bool),
          typeof(AtomForm),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
           // property changed, delegate
           (sender, oldValue, newValue) => ((AtomForm)sender).OnShowSearchChanged(oldValue, newValue),
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
        /// On ShowSearch changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnShowSearchChanged(object oldValue, object newValue)
        {
            UpdateItems();
        }


        /// <summary>
        /// Property ShowSearch
        /// </summary>
        public bool ShowSearch
        {
            get
            {
                return (bool)GetValue(ShowSearchProperty);
            }
            set
            {
                SetValue(ShowSearchProperty, value);
            }
        }
        #endregion

        #region Property SearchText

        /// <summary>
        /// Bindable Property SearchText
        /// </summary>
        public static readonly BindableProperty SearchTextProperty = BindableProperty.Create(
          "SearchText",
          typeof(string),
          typeof(AtomForm),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
           (sender,oldValue,newValue) => ((AtomForm)sender).OnSearchTextChanged(oldValue, newValue),
          
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
        /// On SearchText changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSearchTextChanged(object oldValue, object newValue)
        {
            UIAtomsApplication.Instance.TriggerOnce(() => {
                UpdateItems();
            }, TimeSpan.FromMilliseconds(500));
        }


        /// <summary>
        /// Property SearchText
        /// </summary>
        public string SearchText
        {
            get
            {
                return (string)GetValue(SearchTextProperty);
            }
            set
            {
                SetValue(SearchTextProperty, value);
            }
        }
        #endregion




        #region Property IsValid

        /// <summary>
        /// Bindable Property IsValid
        /// </summary>
        public static readonly BindableProperty IsValidProperty;

        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
          "IsValid",
          typeof(bool),
          typeof(AtomForm),
          false,
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
        /// On IsValid changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsValidChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property IsValid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return (bool)GetValue(IsValidProperty);
            }            
        }


        internal void UpdateIsValid() {
            
            var q = Items.SelectMany(x => x.Fields);
            var isValid = true;
            foreach (var item in q) {
                var error = AtomValidationRule.Validate(item);
                if (error != null)
                {
                    isValid = false;
                }
            }
            SetValue(IsValidPropertyKey, isValid);
        }

        #endregion

        #region Property InvalidMessage

        /// <summary>
        /// Bindable Property InvalidMessage
        /// </summary>
        public static readonly BindableProperty InvalidMessageProperty = BindableProperty.Create(
          "InvalidMessage",
          typeof(string),
          typeof(AtomForm),
          "Please complete all required fields",
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
        /// On InvalidMessage changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnInvalidMessageChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property InvalidMessage
        /// </summary>
        public string InvalidMessage
        {
            get
            {
                return (string)GetValue(InvalidMessageProperty);
            }
            set
            {
                SetValue(InvalidMessageProperty, value);
            }
        }
        #endregion




        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<AtomFieldGroup> Items { get; } 

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<View> Fields { get; }


        

    }

    internal class FieldCollection<T> : ObservableCollection<T>
        where T : AtomFieldGroup
    {
        private readonly AtomForm Form;

        public FieldCollection(AtomForm form)
        {
            this.Form = form;
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.CollectionChanged += Item_CollectionChanged;
        }

        private void Item_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Form.UpdateItems();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            Form.UpdateItems();
        }

        protected override void RemoveItem(int index)
        {
            T item = this.ElementAtOrDefault<T>(index);
            if (item != null) {
                item.CollectionChanged -= Item_CollectionChanged;
            }
            base.RemoveItem(index);
        }

        internal IEnumerable AllFields(bool flat = false)
        {
            System.Diagnostics.Debug.WriteLine("Enumerating Form Fields");
            // var list = new ArrayList();
            if (flat)
            {
                foreach (var item in Groups)
                {
                    // list.Add(item);
                    yield return item;
                }
            }
            else
            {
                foreach (object item in Groups.GroupBy(x => AtomForm.GetCategory(x)))
                {
                    // list.Add(item);
                    yield return item;
                }
            }
            //foreach (var item in list)
            //{
            //    System.Diagnostics.Debug.WriteLine(item.GetType().FullName);
            //}
            // return list;
        }

        internal IEnumerable<View> Groups {
            get {
                View last = null;
                foreach (var item in this) {
                    foreach (var f in item.Fields)
                    {
                        if (last != null)
                        {
                            AtomForm.SetFocusNext(last, f);
                        }
                        last = f;
                        AtomForm.SetCategory(f, item.Category);
                        yield return f;
                    }
                }
            }
        }
    }


#if N__IOS__

	public class AtomFormItemsControl: AtomItemsControl{
		public AtomFormItemsControl ()
		{
			//this.HasUnevenRows = true;

		}
	}
#else

    /// <summary>
    /// 
    /// </summary>
    public class AtomFormItemsControl : CollectionView
    {
        private readonly AtomForm Form;

        protected override void OnScrollToRequested(ScrollToRequestEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnScroll To Requested");
            base.OnScrollToRequested(e);
        }
        public void UpdateLayout()
        {
            this.InvalidateMeasure();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public AtomFormItemsControl(AtomForm form)
        {

            this.Form = form;

            //this.HasUnevenRows = true;

            this.Margin = 5;
            this.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepScrollOffset;
            this.BackgroundColor = Color.Transparent;
            //this.GroupDisplayBinding = new Binding { Path = "Key" };
            //this.SeparatorVisibility = SeparatorVisibility.None;
            //this.IsGroupingEnabled = true;
            // this.ItemTemplate = new DataTemplate(typeof(AtomFieldGroup));
            this.ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems;
            this.ItemTemplate = new DataTemplate(() =>
            {
                // Grid grid = new Grid();
                var afg = Form.FieldStyle.CreateContent() as AtomFieldGrid;
                if (afg == null)
                {
                    throw new InvalidOperationException("FieldStyle must contain root element of type AtomFieldGrid");
                }
                afg.Form = this.Form;
                return afg;
            });



            this.IsGrouped = true;
            var layout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
            
            this.ItemsLayout = layout;


            //this.GroupHeaderTemplate = new DataTemplate(()=> {
            //    Label l = new Label();
            //    //l.Margin = new Thickness(5);
            //    //l.HorizontalTextAlignment = TextAlignment.Center;
            //    l.VerticalTextAlignment = TextAlignment.Center;
            //    l.BackgroundColor = Color.FromRgb(0.8,0.8,0.8);
            //    //l.TextColor = Color.White;
            //    l.SetBinding(Label.TextProperty, new Binding {
            //        Path = "Key"
            //    });
                
            //    l.SetBinding(Label.IsVisibleProperty, new Binding {
            //        Path = "Key",
            //        Converter = StringToVisibilityConverter.Instance
            //    });
            //    return new ViewCell {
            //        View = l
            //    };
            //});

            //this.ItemTemplate = new DataTemplate(typeof(ViewCell));

            //this.ItemSelected += (s, e) =>
            //{
            //    this.SelectedItem = null;
            //};


            
            
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="content"></param>
        //protected override void UnhookContent(Cell content)
        //{
        //    base.UnhookContent(content);

        //    ViewCell vc = content as AtomFieldItemTemplate;
        //    if (vc != null && vc.View != null )
        //    {
        //        var afg = vc.View as AtomFieldGrid;
        //        if (afg != null) {
        //            afg.UnbindView();
        //        }
        //    }
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="content"></param>
        ///// <param name="index"></param>
        //protected override void SetupContent(Cell content, int index)
        //{
        //    base.SetupContent(content, index);

        //    ViewCell vc = content as AtomFieldItemTemplate;
        //    if (vc != null)
        //    {
        //        View v = content.BindingContext as View;
        //        if (v != null)
        //        {

        //            // create content...
        //            if (vc.View == null)
        //            {
        //                // inherit...
        //                v.SetBinding(View.BindingContextProperty, new Binding {
        //                    Path = "BindingContext",
        //                    Source = this.Form
        //                });

        //                var afg = Form.FieldStyle.CreateContent() as AtomFieldGrid;
        //                if (afg == null) {
        //                    throw new InvalidOperationException("FieldStyle must contain root element of type AtomFieldGrid");
        //                }

        //                afg.BindView(v);

        //                vc.View = afg;

        //            }
        //        }
        //    }

        //}


    }
    
    #endif



}
