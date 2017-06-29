using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomLabel: Label
    {

        #region Property DefaultText

        /// <summary>
        /// Bindable Property DefaultText
        /// </summary>
        public static readonly BindableProperty DefaultTextProperty = BindableProperty.Create(
          "DefaultText",
          typeof(string),
          typeof(AtomLabel),
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
        /// On DefaultText changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnDefaultTextChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property DefaultText
        /// </summary>
        public string DefaultText
        {
            get
            {
                return (string)GetValue(DefaultTextProperty);
            }
            set
            {
                SetValue(DefaultTextProperty, value);
            }
        }
        #endregion



        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TextProperty.PropertyName || propertyName == DefaultTextProperty.PropertyName) {
                if (string.IsNullOrWhiteSpace(Text) && !string.IsNullOrWhiteSpace(DefaultText) && Text != DefaultText) {
                    Text = DefaultText;
                }
            }
        }

    }
}
