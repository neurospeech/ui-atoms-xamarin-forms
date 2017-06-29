using NeuroSpeech.UIAtoms.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// An Extension of AtomTextBox, this contains validation for regular expression functionality.
    /// </summary>
    public class AtomTextBoxWithRegEx: AtomTextBox
    {

        private static readonly AtomPropertyValidator DefaultValidator =
                            new AtomPropertyValidator
                            {
                                BindableProperty = TextProperty,
                                ValidationRule = AtomUtils.Singleton<AtomRegExValidationRule>()
                            };

        protected override void SetDefaultValidator()
        {
            AtomForm.SetValidator(this,DefaultValidator);
        }

        /// <summary>
        /// ValidationRegEx
        /// </summary>
        /// <remarks>
        /// 	<list type="bullet">
        /// 		<listheader>
        /// ValidationRegEx
        /// </listheader>
        /// 		<item>
        /// This property needs to be defined. It will be the string used to validate the input received in the TextBox.
        /// </item>
        /// 	</list>
        /// </remarks>
        public string ValidationRegEx
        {
            get { return (string)GetValue(AtomRegExValidationRule.ValidationRegExProperty); }
            set { SetValue(AtomRegExValidationRule.ValidationRegExProperty, value); }
        }


    }
}
