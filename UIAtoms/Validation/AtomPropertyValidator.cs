using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Validation
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomPropertyValidator: BindableObject
    {

        #region Property Property

        /// <summary>
        /// Bindable Property Property
        /// </summary>
        public static readonly BindableProperty PropertyProperty = BindableProperty.Create(
          "Property",
          typeof(string),
          typeof(AtomPropertyValidator),
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
        /// On Property changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnPropertyChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Property
        /// </summary>
        public string Property
        {
            get
            {
                return (string)GetValue(PropertyProperty);
            }
            set
            {
                SetValue(PropertyProperty, value);
            }
        }
        #endregion

        #region Property ValidationRule

        /// <summary>
        /// Bindable Property ValidationRule
        /// </summary>
        public static readonly BindableProperty ValidationRuleProperty = BindableProperty.Create(
          "ValidationRule",
          typeof(AtomValidationRule),
          typeof(AtomPropertyValidator),
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
        /// On ValidationRule changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnValidationRuleChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ValidationRule
        /// </summary>
        public AtomValidationRule ValidationRule
        {
            get
            {
                return (AtomValidationRule)GetValue(ValidationRuleProperty);
            }
            set
            {
                SetValue(ValidationRuleProperty, value);
            }
        }
        #endregion

        #region Property BindableProperty

        /// <summary>
        /// Bindable Property BindableProperty
        /// </summary>
        public static readonly BindableProperty BindablePropertyProperty = BindableProperty.Create(
          "BindableProperty",
          typeof(BindableProperty),
          typeof(AtomPropertyValidator),
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
        /// On BindableProperty changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnBindablePropertyChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property BindableProperty
        /// </summary>
        public BindableProperty BindableProperty
        {
            get
            {
                return (BindableProperty)GetValue(BindablePropertyProperty);
            }
            set
            {
                SetValue(BindablePropertyProperty, value);
            }
        }
        #endregion



    }
}
