using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// Hides automatically if text is null or empty
    /// </summary>
    public class AtomBubbleLabel : StackLayout
    {

        #region Property Text

        /// <summary>
        /// Bindable Property Text
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
          "Text",
          typeof(string),
          typeof(AtomBubbleLabel),
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
        /// On Text changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTextChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Text
        /// </summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        #endregion

        #region Property TextColor

        /// <summary>
        /// Bindable Property TextColor
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
          "TextColor",
          typeof(Color),
          typeof(AtomBubbleLabel),
          Color.Default,
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
        /// On TextColor changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTextColorChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property TextColor
        /// </summary>
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }
        #endregion

        #region Property CornerRadius

        /// <summary>
        /// Bindable Property CornerRadius
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
          nameof(CornerRadius),
          typeof(int),
          typeof(AtomBubbleLabel),
          15,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomBubbleLabel)sender).OnCornerRadiusChanged(oldValue,newValue),
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
        /// On CornerRadius changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCornerRadiusChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CornerRadius
        /// </summary>
        public int CornerRadius
        {
            get
            {
                return (int)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }
        #endregion



        private AtomRoundBorderEffect borderEffect = new AtomRoundBorderEffect {
            CornerRadius = 15,
            BackgroundColor = Color.Red,
            StrokeWidth = 0
        };

        /// <summary>
        /// 
        /// </summary>
        public AtomBubbleLabel()
        {
            //this.ControlTemplate = new ControlTemplate(typeof(AtomBubbuleLabelTemplate));
            BackgroundColor = Color.Red;
            TextColor = Color.White;
            this.Padding = new Thickness(5);

            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;

            this.Effects.Add(borderEffect);

            this.IsVisible = false;

            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding(Label.TextProperty.PropertyName) {
                Source = this
            });
            label.SetBinding(Label.TextColorProperty, new Binding(Label.TextColorProperty.PropertyName) {
                Source = this
            });

            Children.Add(label);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(Text):
                    IsVisible = !string.IsNullOrWhiteSpace(Text);
                    break;
                case nameof(BackgroundColor):
                case nameof(CornerRadius):
                    //Effects.Remove(borderEffect);
                    borderEffect.BackgroundColor = BackgroundColor;
                    borderEffect.CornerRadius = CornerRadius;
                    //Effects.Add(borderEffect);
                    this.InvalidateLayout();
                    break;
                default:
                    break;
            }

        }

    }

    //public class AtomBubbuleLabelTemplate : AtomFrame
    //{
    //    public AtomBubbuleLabelTemplate()
    //    {
    //        var label = new Label();
    //        this.Content = label;
    //        label.SetBinding(Label.TextProperty, new TemplateBinding(Label.TextProperty.PropertyName));
    //        label.SetBinding(Label.TextColorProperty, new TemplateBinding(Label.TextColorProperty.PropertyName));

    //        this.SetBinding(Frame.BackgroundColorProperty, new TemplateBinding(TemplatedView.BackgroundColorProperty.PropertyName));
    //        this.SetBinding(Frame.PaddingProperty, new TemplateBinding(TemplatedView.PaddingProperty.PropertyName));
    //        this.SetBinding(Frame.OpacityProperty, new TemplateBinding(TemplatedView.OpacityProperty.PropertyName));
    //        this.SetBinding(Frame.IsVisibleProperty, new TemplateBinding(TemplatedView.IsVisibleProperty.PropertyName));

    //    }
    //}
}
