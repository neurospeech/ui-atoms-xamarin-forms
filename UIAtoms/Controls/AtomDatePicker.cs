using NeuroSpeech.UIAtoms.DI;
using NeuroSpeech.UIAtoms.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomDatePicker : ContentView {


        static readonly AtomPropertyValidator DefaultValidator;


        static AtomDatePicker()
        {
            DefaultValidator = new AtomPropertyValidator
            {
                BindableProperty = ValueProperty,
                ValidationRule = AtomUtils.Singleton<AtomDateValidationRule>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public AtomDatePicker()
        {
            AtomForm.SetValidator(this, DefaultValidator);

            contentLabel = new Label {

            };
            contentLabel.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new AtomCommand(async ()=> await OnTapCommandAsync())
            });

            Content = contentLabel;

            this.ControlTemplate = new ControlTemplate(typeof(AtomDatePickerTemplate));

            contentLabel.Text = EmptyLabel;
        }

        private async Task OnTapCommandAsync()
        {
            try
            {

                var nav = Xamarin.Forms.DependencyService.Get<INavigation>();

                var calendar = new AtomCalendar();

                //calendar.BackgroundColor = Color.White;
                calendar.SelectedDate = this.Value;
                calendar.TapCommand = new AtomCommand<AtomDateModel>(async (model) =>
                {

                    await Task.Delay(100);
                    Value = model.Value;

                    await nav.PopModalAsync(true);

                });


                Page page = new AtomPopupPage
                {
                    Content = calendar,
                    Title = "Select Date"
                };


                //await Navigation.PushAsync(page);
                await nav.PushModalAsync(page);
            }
            catch (Exception ex) {
                UIAtomsApplication.Logger?.Invoke(ex.ToString());
            }
        }



        #region Property Value

        /// <summary>
        /// Bindable Property Value
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
          nameof(Value),
          typeof(DateTime?),
          typeof(AtomDatePicker),
          null,
          BindingMode.TwoWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomDatePicker)sender).OnValueChanged(oldValue,newValue),
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
            DateTime? v = newValue as DateTime?;
            if (v == null)
            {
                contentLabel.Text = EmptyLabel;
            }
            else {
                contentLabel.Text = string.Format(DateFormat ?? "{0:dd MMM yyyy}", v.Value);
            }
        }


        /// <summary>
        /// Property Value
        /// </summary>
        public DateTime? Value
        {
            get
            {
                return (DateTime?)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
        #endregion





        #region Property DateFormat

        /// <summary>
        /// Bindable Property DateFormat
        /// </summary>
        public static readonly BindableProperty DateFormatProperty = BindableProperty.Create(
          nameof(DateFormat),
          typeof(string),
          typeof(AtomDatePicker),
          "{0:dd MMM yyyy}",
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomDatePicker)sender).OnDateFormatChanged(oldValue,newValue),
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
        /// On DateFormat changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnDateFormatChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property DateFormat
        /// </summary>
        public string DateFormat
        {
            get
            {
                return (string)GetValue(DateFormatProperty);
            }
            set
            {
                SetValue(DateFormatProperty, value);
            }
        }
        #endregion





        #region Property EmptyLabel

        /// <summary>
        /// Bindable Property EmptyLabel
        /// </summary>
        public static readonly BindableProperty EmptyLabelProperty = BindableProperty.Create(
          nameof(EmptyLabel),
          typeof(string),
          typeof(AtomDatePicker),
          "(Choose Date)",
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomDatePicker)sender).OnEmptyLabelChanged(oldValue,newValue),
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
        private Label contentLabel;

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


        



    }


    /// <summary>
    /// 
    /// </summary>
    public class AtomDatePickerTemplate: Grid {

        /// <summary>
        /// 
        /// </summary>
        public AtomDatePickerTemplate()
        {
            this.ColumnSpacing = 5;
            this.ColumnDefinitions.Add(new ColumnDefinition { });
            this.ColumnDefinitions.Add(new ColumnDefinition {
                Width = GridLength.Auto
            });

            this.Children.Add(new ContentPresenter());

            AtomImage clearButton = new AtomImage {
                Source = AtomStockImages.DeleteImageUrl
            };

            clearButton.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new AtomCommand(() =>
                {
                    var a = this.Parent as AtomDatePicker;
                    a.Value = null;
                    return Task.CompletedTask;
                })
            });

            

            SetColumn(clearButton, 1);

            Children.Add(clearButton);

            clearButton.WidthRequest = 25;
            clearButton.HeightRequest = 25;

            clearButton.SetBinding(
                Image.IsVisibleProperty, 
                new TemplateBinding(
                    nameof(AtomDatePicker.Value), 
                    BindingMode.OneWay, 
                    NotNullVisibilityConverter.Instance
                    ));

        }
    }
}
