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
    public class AtomPasswordBoxAgain: AtomTextBox
    {

        private static readonly AtomPropertyValidator DefaultValidator =
            new AtomPropertyValidator {
                BindableProperty = TextProperty,
                ValidationRule = AtomUtils.Singleton<AtomPasswordAgainValidationRule>()
            };

        public string Password {
            get {
                return AtomPasswordAgainValidationRule.GetPassword(this);
            }set {
                AtomPasswordAgainValidationRule.SetPassword(this, value);
            }
        }

        public string PasswordMismatchMessage {
            get {
                return AtomPasswordAgainValidationRule.GetPasswordMismatchError(this);
            }
            set {
                AtomPasswordAgainValidationRule.SetPasswordMismatchError(this, value);
            }

        }

        protected override void SetDefaultValidator()
        {
            AtomForm.SetValidator(this, DefaultValidator);
        }

    }
}
