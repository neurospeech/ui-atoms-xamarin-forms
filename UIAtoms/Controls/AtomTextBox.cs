using NeuroSpeech.UIAtoms.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    
    public class AtomTextBox : Entry
    {

        private static readonly AtomPropertyValidator DefaultValidator =
            new AtomPropertyValidator {
                BindableProperty = TextProperty,
                ValidationRule = AtomUtils.Singleton<AtomStringValidationRule>()
            };


        public AtomTextBox()
        {
            SetDefaultValidator();
        }


        protected virtual void SetDefaultValidator()
        {
            AtomForm.SetValidator(this,DefaultValidator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "IsFocused") {
                if (!this.IsFocused) {
                    // we just lost the focus??
                    // lets validate this textbox...
                    if (AtomForm.GetError(this) != null)
                    {
                        AtomValidationRule.Validate(this);
                    }
                    
                }
            }
        }


        /// <summary>
        /// Minimum length of text for validation
        /// </summary>
        public int MinimumLength {
            get {
                return AtomStringValidationRule.GetMinimumLength(this);
            }
            set {
                AtomStringValidationRule.SetMinimumLength(this, value);
            }
        }

        
        /// <summary>
        /// Maximum length of text for validation
        /// </summary>
        public int MaximumLength {
            get {
                return AtomStringValidationRule.GetMinimumLength(this);
            }
            set {
                AtomStringValidationRule.SetMaximumLength(this, value);
            }
        }


        /// <summary>
        /// Error messge displayed when text is too short
        /// </summary>
        [DefaultValue("Text is too short")]
        public string MinimumLengthErrorMessage {
            get {
                return AtomStringValidationRule.GetMinimumLengthErrorMessage(this);
            }set {
                AtomStringValidationRule.SetMinimumLengthErrorMessage(this, value);
            }
        }

        /// <summary>
        /// Error messge displayed when text is too long
        /// </summary>
        [DefaultValue("Text is too long")]
        public string MaximumLengthErrorMessage
        {
            get
            {
                return AtomStringValidationRule.GetMaximumLengthErrorMessage(this);
            }
            set
            {
                AtomStringValidationRule.SetMaximumLengthErrorMessage(this, value);
            }
        }


        #region Property DisableSuggestions

        /// <summary>
        /// Bindable Property DisableSuggestions
        /// </summary>
        public static readonly BindableProperty DisableSuggestionsProperty = BindableProperty.Create(
          "DisableSuggestions",
          typeof(bool),
          typeof(AtomTextBox),
          true,
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
        /// On DisableSuggestions changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnDisableSuggestionsChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property DisableSuggestions
        /// </summary>
        public bool DisableSuggestions
        {
            get
            {
                return (bool)GetValue(DisableSuggestionsProperty);
            }
            set
            {
                SetValue(DisableSuggestionsProperty, value);
            }
        }
        #endregion



    }
}
