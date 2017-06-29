using NeuroSpeech.UIAtoms.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomPasswordBox: AtomTextBox
    {

        private static readonly AtomPropertyValidator DefaultValidator =
            new AtomPropertyValidator
            {
                BindableProperty = TextProperty,
                ValidationRule = AtomUtils.Singleton<AtomPasswordValidationRule>()
            };

        private static readonly BindablePropertyKey HasUpperCasePropertyKey;
        private static readonly BindablePropertyKey HasLowerCasePropertyKey;
        private static readonly BindablePropertyKey HasNumberPropertyKey;
        private static readonly BindablePropertyKey HasSymbolPropertyKey;

        public static readonly BindableProperty HasUpperCaseProperty;
        public static readonly BindableProperty HasLowerCaseProperty;
        public static readonly BindableProperty HasNumberProperty;
        public static readonly BindableProperty HasSymbolProperty;

        static AtomPasswordBox(){

            HasUpperCasePropertyKey = BindableProperty.CreateReadOnly("HasUpperCase", typeof(bool), typeof(AtomPasswordBox), false, BindingMode.OneWayToSource);
            HasUpperCaseProperty = HasUpperCasePropertyKey.BindableProperty;

            HasLowerCasePropertyKey = BindableProperty.CreateReadOnly("HasLowercase", typeof(bool), typeof(AtomPasswordBox), false, BindingMode.OneWayToSource);
            HasLowerCaseProperty = HasLowerCasePropertyKey.BindableProperty;

            HasNumberPropertyKey = BindableProperty.CreateReadOnly("HasNumber", typeof(bool), typeof(AtomPasswordBox), false);
            HasNumberProperty = HasNumberPropertyKey.BindableProperty;

            HasSymbolPropertyKey = BindableProperty.CreateReadOnly("HasSymbol", typeof(bool), typeof(AtomPasswordBox), false);
            HasSymbolProperty = HasSymbolPropertyKey.BindableProperty;
        }

        protected override void SetDefaultValidator()
        {

            AtomForm.SetValidator(this, DefaultValidator);

            this.IsPassword = true;

            this.MinimumLength = 5;
            this.MaximumLength = 16;
        }

        public bool HasUpperCase {
            get {
                return (bool)GetValue(HasUpperCaseProperty);
            }
            private set {
                SetValue(HasUpperCasePropertyKey, value);
            }
        }

        public bool HasLowerCase
        {
            get
            {
                return (bool)GetValue(HasLowerCaseProperty);
            }
            private set
            {
                SetValue(HasLowerCasePropertyKey, value);
            }
        }

        public bool HasSymbolCase
        {
            get
            {
                return (bool)GetValue(HasSymbolProperty);
            }
            private set
            {
                SetValue(HasSymbolPropertyKey, value);
            }
        }

        public bool HasNumber {
            get {
                return (bool)GetValue(HasNumberProperty);
            }
            private set {
                SetValue(HasNumberPropertyKey, value);
            }
        }
        
        public bool RequiresUpperCase {
            get {
                return AtomPasswordValidationRule.GetRequiresUpperCase(this);
            }
            set {
                AtomPasswordValidationRule.SetRequiresUpperCase(this,value);
            }
        }

        public bool RequiresLowerCase {
            get {
                return AtomPasswordValidationRule.GetRequiresLowerCase(this);
            }set {
                AtomPasswordValidationRule.SetRequiresLowerCase(this, value);
            }
        }

        public bool RequiresNumber {
            get {
                return AtomPasswordValidationRule.GetRequiresNumber(this);
            }set {
                AtomPasswordValidationRule.SetRequiresNumber(this, value);
            }
        }

        public bool RequiresSymbol {
            get {
                return AtomPasswordValidationRule.GetRequiresSymbol(this);
            }
            set {
                AtomPasswordValidationRule.SetRequiresSymbol(this, value);
            }
        }

        public string InvalidPasswordMessage {
            get {
                return AtomPasswordValidationRule.GetInvalidPasswordMessage(this);
            }set {
                AtomPasswordValidationRule.SetInvalidPasswordMessage(this, value);
            }
        }
    }
}
