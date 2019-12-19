using NeuroSpeech.UIAtoms.Pages;
using NeuroSpeech.UIAtoms.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    //[ContentProperty("Content")]
    public class AtomChooser: AtomContentGrid
    {


        static readonly AtomPropertyValidator DefaultValidator;


        static AtomChooser()
        {
            DefaultValidator = new AtomPropertyValidator
            {
                BindableProperty = ValueProperty,
                ValidationRule = AtomUtils.Singleton<AtomSelectionValidationRule>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public AtomChooser()
        {

            AtomForm.SetValidator(this, DefaultValidator);

            this.ChangeCommand = new AtomCommand(async () => await OnTapCommandAsync());

            this.Padding = new Thickness(5);



            ItemTemplate = new DataTemplate(() => {
                var l = new AtomLabel();
                l.SetBinding(AtomLabel.TextProperty, new Binding()
                {
                    Path = LabelPath
                });
                return l;
            });

            this.Label = EmptyLabel;
        }


        private AtomList<object> selectedItems = new AtomList<object>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected View CreateLabelContent()
        {

            Children.Clear();

            //this.MinimumHeightRequest = 30;

            ColumnDefinitionCollection cdc = new ColumnDefinitionCollection();
            ColumnDefinitions = cdc;
            cdc.Add(new ColumnDefinition { });
            cdc.Add(new ColumnDefinition
            {
                Width = 30
            });

            AtomImage img = new AtomImage() { HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
            img.Source = AtomStockImages.DropDownImageUrl; //"static://NeuroSpeech.UIAtoms/NeuroSpeech.UIAtoms.Controls.AtomStockImages.DropDownImage";

            SetColumn(img, 1);
            Children.Add(img);


            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ChangeCommand
            });

            Label label = new Xamarin.Forms.Label()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };

            //this.Content = label;

            label.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding()
            {
                Path = "Label",
                Source = this
            });
            return label;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ChangeCommand { get; }


        public bool ShowAsPopupButton { get; set; }


        private async Task OnTapCommandAsync() {

            try {


                var nav = DependencyService.Get<INavigation>();

                var currentPage = this.GetParentOfType<Page>();

                ChooserView cv = new Pages.ChooserView(this);

                if (ShowAsPopupButton || currentPage is AtomPopupPage)
                {
                    AtomPopupPage p = new Controls.AtomPopupPage() {
                        Content = cv,
                        Title = this.PromptText ?? "Select"
                    };
                    cv.Popup = true;
                    await DependencyService.Get<INavigation>().PushModalAsync(p);
                }
                else
                {
                    //AtomChooserPage page = new AtomChooserPage(this);
                    ContentPage p = new ContentPage {
                        Content = cv,
                        Title = this.PromptText ?? "Select"
                    };
                    

                    await DependencyService.Get<INavigation>().PushAsync(p);
                }


            } catch (Exception ex) {
                UIAtomsApplication.Logger?.Invoke(ex.ToString());
            }

        }

        #region Property SelectedItems

        /// <summary>
        /// Bindable Property SelectedItems
        /// </summary>
        private static readonly BindablePropertyKey SelectedItemsPropertyKey = BindableProperty.CreateReadOnly(
          nameof(SelectedItems),
          typeof(System.Collections.IEnumerable),
          typeof(AtomChooser),
          null,
          BindingMode.OneWayToSource,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnSelectedItemsChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           (s) => ((AtomChooser)s).selectedItems
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
        public System.Collections.IEnumerable SelectedItems
        {
            get
            {
                return (System.Collections.IEnumerable)GetValue(SelectedItemsProperty);
            }
            private set
            {
                SetValue(SelectedItemsPropertyKey, value);
            }
        }
        #endregion

        #region Property AddNewCommand

        /// <summary>
        /// Bindable Property AddNewCommand
        /// </summary>
        public static readonly BindableProperty AddNewCommandProperty = BindableProperty.Create(
          nameof(AddNewCommand),
          typeof(System.Windows.Input.ICommand),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnAddNewCommandChanged(oldValue,newValue),
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
        /// On AddNewCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAddNewCommandChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AddNewCommand
        /// </summary>
        public System.Windows.Input.ICommand AddNewCommand
        {
            get
            {
                return (System.Windows.Input.ICommand)GetValue(AddNewCommandProperty);
            }
            set
            {
                SetValue(AddNewCommandProperty, value);
            }
        }
        #endregion

        #region Property AddNewLabel

        /// <summary>
        /// Bindable Property AddNewLabel
        /// </summary>
        public static readonly BindableProperty AddNewLabelProperty = BindableProperty.Create(
          nameof(AddNewLabel),
          typeof(string),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnAddNewLabelChanged(oldValue,newValue),
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
        /// On AddNewLabel changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAddNewLabelChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AddNewLabel
        /// </summary>
        public string AddNewLabel
        {
            get
            {
                return (string)GetValue(AddNewLabelProperty);
            }
            set
            {
                SetValue(AddNewLabelProperty, value);
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
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnItemTemplateChanged(oldValue,newValue),
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

        #region Property IsGroupingEnabled

        /// <summary>
        /// Bindable Property IsGroupingEnabled
        /// </summary>
        public static readonly BindableProperty IsGroupingEnabledProperty = BindableProperty.Create(
          "IsGroupingEnabled",
          typeof(bool),
          typeof(AtomChooser),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnIsGroupingEnabledChanged(oldValue,newValue),
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
          typeof(Binding),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnGroupDisplayBindingChanged(oldValue,newValue),
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
        public Binding GroupDisplayBinding
        {
            get
            {
                return (Binding)GetValue(GroupDisplayBindingProperty);
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
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnGroupHeaderTemplateChanged(oldValue,newValue),
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
          typeof(Binding),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnGroupShortNameBindingChanged(oldValue,newValue),
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
        public Binding GroupShortNameBinding
        {
            get
            {
                return (Binding)GetValue(GroupShortNameBindingProperty);
            }
            set
            {
                SetValue(GroupShortNameBindingProperty, value);
            }
        }
        #endregion

        #region Property AllowMultipleSelection

        /// <summary>
        /// Bindable Property AllowMultipleSelection
        /// </summary>
        public static readonly BindableProperty AllowMultipleSelectionProperty = BindableProperty.Create(
          "AllowMultipleSelection",
          typeof(bool),
          typeof(AtomChooser),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnAllowMultipleSelectionChanged(oldValue,newValue),
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

        #region Property ItemsSource

        /// <summary>
        /// Bindable Property ItemsSource
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
          "ItemsSource",
          typeof(System.Collections.IEnumerable),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomChooser)sender).OnItemsSourceChanged(oldValue,newValue),
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
                inc.CollectionChanged -= ItemCollectionChanged;
            }
            inc = newValue as INotifyCollectionChanged;
            if (inc != null) {
                inc.CollectionChanged += ItemCollectionChanged;
            }
            //UpdateLabel();
            ItemCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));            
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

        #region Property LabelPath

        /// <summary>
        /// Bindable Property LabelPath
        /// </summary>
        public static readonly BindableProperty LabelPathProperty = BindableProperty.Create(
          "LabelPath",
          typeof(string),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCheckBoxList)sender).OnLabelPathChanged(oldValue,newValue),
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
        /// On LabelPath changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnLabelPathChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property LabelPath
        /// </summary>
        public string LabelPath
        {
            get
            {
                return (string)GetValue(LabelPathProperty);
            }
            set
            {
                SetValue(LabelPathProperty, value);
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
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCheckBoxList)sender).OnValuePathChanged(oldValue,newValue),
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
        /// On ValuePath changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnValuePathChanged(object oldValue, object newValue)
        {
            
        }*/


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

        #region Property Value

        /// <summary>
        /// Bindable Property Value
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
          "Value",
          typeof(object),
          typeof(AtomChooser),
          null,
          BindingMode.TwoWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomChooser)sender).OnValueChanged(oldValue,newValue),
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
        /// On Value changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnValueChanged(object oldValue, object newValue)
        {
            UpdateSelectedItems();
        }


        /// <summary>
        /// Property Value
        /// </summary>
        public object Value
        {
            get
            {
                return GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
        #endregion

        #region Property ValueSeparator

        /// <summary>
        /// Bindable Property ValueSeparator
        /// </summary>
        public static readonly BindableProperty ValueSeparatorProperty = BindableProperty.Create(
          "ValueSeparator",
          typeof(string),
          typeof(AtomChooser),
          ",",
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCheckBoxList)sender).OnValueSeparatorChanged(oldValue,newValue),
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
        /// On ValueSeparator changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnValueSeparatorChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ValueSeparator
        /// </summary>
        public string ValueSeparator
        {
            get
            {
                return (string)GetValue(ValueSeparatorProperty);
            }
            set
            {
                SetValue(ValueSeparatorProperty, value);
            }
        }
        #endregion

        #region Property Label

        /// <summary>
        /// Bindable Property Label
        /// </summary>
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
          "Label",
          typeof(string),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomChooser)sender).OnLabelChanged(oldValue,newValue),
          //null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           //(sender) => ((AtomChooser)sender).EmptyLabel
           null
          
        );


        
        /// <summary>
        /// On Label changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnLabelChanged(object oldValue, object newValue)
        {
            if (Content == null) {
                Content = CreateLabelContent();
            }
            UpdateCellSize();
        }


        /// <summary>
        /// Property Label
        /// </summary>
        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
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
          typeof(AtomChooser),
          "Select",
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnEmptyLabelChanged(oldValue,newValue),
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

        #region Property ShowSearch

        /// <summary>
        /// Bindable Property ShowSearch
        /// </summary>
        public static readonly BindableProperty ShowSearchProperty = BindableProperty.Create(
          nameof(ShowSearch),
          typeof(bool),
          typeof(AtomChooser),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnShowSearchChanged(oldValue,newValue),
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
        /// On ShowSearch changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnShowSearchChanged(object oldValue, object newValue)
        {
            
        }*/


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

        #region Property PromptText

        /// <summary>
        /// Bindable Property PromptText
        /// </summary>
        public static readonly BindableProperty PromptTextProperty = BindableProperty.Create(
          nameof(PromptText),
          typeof(string),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnPromptTextChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           (s) => "Choose " + (AtomForm.GetLabel(s) ?? "Choose")
          //null
        );

        /*
        /// <summary>
        /// On PromptText changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnPromptTextChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property PromptText
        /// </summary>
        public string PromptText
        {
            get
            {
                return (string)GetValue(PromptTextProperty);
            }
            set
            {
                SetValue(PromptTextProperty, value);
            }
        }
        #endregion

        #region Property FilterPath

        /// <summary>
        /// Bindable Property FilterPath
        /// </summary>
        public static readonly BindableProperty FilterPathProperty = BindableProperty.Create(
          nameof(FilterPath),
          typeof(string),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnFilterPathChanged(oldValue,newValue),
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
        /// On FilterPath changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnFilterPathChanged(object oldValue, object newValue)
        {
            
        }*/


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

        #region Property SearchText

        /// <summary>
        /// Bindable Property SearchText
        /// </summary>
        public static readonly BindableProperty SearchTextProperty = BindableProperty.Create(
          nameof(SearchText),
          typeof(string),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnSearchTextChanged(oldValue,newValue),
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
        /// On SearchText changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSearchTextChanged(object oldValue, object newValue)
        {
            
        }*/


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

        #region Property TapCommand

        /// <summary>
        /// Bindable Property TapCommand
        /// </summary>
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
          nameof(TapCommand),
          typeof(System.Windows.Input.ICommand),
          typeof(AtomChooser),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnTapCommandChanged(oldValue,newValue),
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

        #region Property EnableSelection

        /// <summary>
        /// Bindable Property EnableSelection
        /// </summary>
        public static readonly BindableProperty EnableSelectionProperty = BindableProperty.Create(
          nameof(EnableSelection),
          typeof(bool),
          typeof(AtomChooser),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnEnableSelectionChanged(oldValue,newValue),
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
        /// On EnableSelection changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnEnableSelectionChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property EnableSelection
        /// </summary>
        public bool EnableSelection
        {
            get
            {
                return (bool)GetValue(EnableSelectionProperty);
            }
            set
            {
                SetValue(EnableSelectionProperty, value);
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
          typeof(AtomChooser),
          -1,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnRowHeightChanged(oldValue,newValue),
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
        /// On RowHeight changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnRowHeightChanged(object oldValue, object newValue)
        {
            
        }*/


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



        #region Property HasUnevenRows

        /// <summary>
        /// Bindable Property HasUnevenRows
        /// </summary>
        public static readonly BindableProperty HasUnevenRowsProperty = BindableProperty.Create(
          nameof(HasUnevenRows),
          typeof(bool),
          typeof(AtomChooser),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomChooser)sender).OnHasUnevenRowsChanged(oldValue,newValue),
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
        /// On HasUnevenRows changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnHasUnevenRowsChanged(object oldValue, object newValue)
        {
            
        }*/


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



        private void UpdateCellSize()
        {
            var cell = this.GetParentOfType<AtomFieldGrid>();
            if (cell != null)
            {
                UIAtomsApplication.Instance.TriggerOnce(() =>
                {
                    this.InvalidateMeasure();
                    cell.ForceUpdateSize();

                });
            }
        }


        private void ItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSelectedItems();

            
        }

        private void UpdateSelectedItems()
        {
            try
            {
                if (selectedItems.IsChanging)
                {
                    return;
                }

                string valuePath = ValuePath;
                if (valuePath == null)
                    return;
                var items = ItemsSource?.Cast<object>();
                if (items == null)
                    return;
                if (Value == null)
                    return;
                List<string> values = null;
                if (this.AllowMultipleSelection)
                {
                    values = Value.ToString().Split(ValueSeparator.ToCharArray()).Select(x => x.Trim()).ToList();
                } else
                {
                    values = new List<string>() { Value.ToString() };
                }

                System.Collections.IList convertedValues = null;

                Func<object, bool> f = (item) =>
                {
                    object v = item.GetPropertyValue(valuePath);
                    if (v == null)
                        return false;
                    if (v is long)
                    {
                        if (convertedValues == null)
                        {
                            convertedValues = values.Select(x => long.Parse(x)).ToList();
                        }
                        return convertedValues.Contains((long)v);
                    }
                    if (v is int)
                    {
                        if (convertedValues == null)
                        {
                            convertedValues = values.Select(x => int.Parse(x)).ToList();
                        }
                        return convertedValues.Contains((int)v);
                    }
                    if (v is double)
                    {
                        if (convertedValues == null)
                        {
                            convertedValues = values.Select(x => double.Parse(x)).ToList();
                        }
                        return convertedValues.Contains((double)v);
                    }
                    if (v is float)
                    {
                        if (convertedValues == null)
                        {
                            convertedValues = values.Select(x => float.Parse(x)).ToList();
                        }
                        return convertedValues.Contains((float)v);
                    }
                    if (v is decimal)
                    {
                        if (convertedValues == null)
                        {
                            convertedValues = values.Select(x => decimal.Parse(x)).ToList();
                        }
                        return convertedValues.Contains((decimal)v);
                    }
                    if (v is bool)
                    {
                        if (convertedValues == null)
                        {
                            convertedValues = values.Select(x => x.Equals("true", StringComparison.OrdinalIgnoreCase)).ToList();
                        }
                        return convertedValues.Contains((bool)v);
                    }
                    return values.Contains(v?.ToString());
                };

                var si = items.Where(f).ToList();

                selectedItems.Replace(si);
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.Fail(ex.Message, ex.ToString());
                throw;
            }
            finally
            {
                UpdateLabel();
                AtomDevice.Instance.TriggerOnce(() => {
                    // go up...

                    this.GetParentOfType<AtomFormItemsControl>()
                        ?.UpdateLayout();

                    return Task.CompletedTask;
                });                
            }
        }

        private void UpdateLabel()
        {


            object value = Value;

            if (ItemsSource == null 
                || !ItemsSource.Cast<object>().Any()) {
                var l = value?.ToString();
                this.Label =  string.IsNullOrWhiteSpace(l) ? EmptyLabel : l;
                return;
            }


            if (value == null)
            {
                this.Label = EmptyLabel;
                return;
            }


            string labelPath = string.IsNullOrWhiteSpace( LabelPath) ? null : LabelPath;
            string valuePath = string.IsNullOrWhiteSpace(ValuePath) ? null : ValuePath;
            Func<object, string> toLabel = (o) => {
                if (labelPath == null)
                    return o.GetPropertyValue(valuePath)?.ToString() ?? "";
                return o.GetPropertyValue(labelPath)?.ToString() ?? "";
            };

            string label = string.Join(", ", selectedItems.Select(x => toLabel(x))).Trim();

            if (string.IsNullOrWhiteSpace(label)) {
                label = EmptyLabel;
            }

            this.Label = label;

            /*if (!AllowMultipleSelection) {

                foreach (var item in ItemsSource) {
                    object v = item.GetPropertyValue(ValuePath);
                    if (v == null)
                        continue;
                    if (v == value || v.Equals(value)) {
                        this.Label = toLabel(item, v);
                        break;
                    }
                }
                return;
            }

            if (value == null)
            {
                this.Label = EmptyLabel;
                return;
            }

            var values = Value.ToString().Split(ValueSeparator.ToCharArray()).Select(x=>x.Trim()).ToList();



            List<string> newLabels = new List<string>();

            foreach (var item in ItemsSource) {
                object v = item.GetPropertyValue(ValuePath);
                if (v == null)
                    continue;
                string sv = values.FirstOrDefault(x => String.Equals(v.ToString(), x, StringComparison.OrdinalIgnoreCase));
                if (sv != null) {
                    newLabels.Add(toLabel(item,sv));
                }
            }

            value = string.Join(", ", newLabels);

            this.Label = value.ToString();*/
        }
    }
}
