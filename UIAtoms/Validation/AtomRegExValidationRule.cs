using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Validation
{

    /// <summary>
    /// This class implements Validation rule to validate text against regular expression.
    /// </summary>
    /// <remarks>
    /// This rule is default validation rule in 
    /// <see cref="T:NeuroSpeech.UIAtoms.Controls.AtomTextBoxWithRegEx"/> to 
    /// validate the input text against specified regular expression. The property
    /// <see cref="P:NeuroSpeech.UIAtoms.Controls.AtomTextBoxWithRegEx.ValidationRegEx"/>
    /// specifies the regular expression to validate against.
    /// </remarks>
    public class AtomRegExValidationRule : AtomStringValidationRule
    {



        #region ValidationRegEx Attached Property
        /// <summary>
        /// ValidationRegEx Attached property
        /// </summary>
        public static readonly BindableProperty ValidationRegExProperty =
            BindableProperty.CreateAttached("ValidationRegEx", typeof(string),
            typeof(AtomRegExValidationRule),
            null,
            BindingMode.OneWay,
            null,
            OnValidationRegExChanged);

        private static void OnValidationRegExChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set ValidationRegEx for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetValidationRegEx(BindableObject bindable, string newValue)
        {
            bindable.SetValue(ValidationRegExProperty, newValue);
        }

        /// <summary>
        /// Get ValidationRegEx for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetValidationRegEx(BindableObject bindable)
        {
            return (string)bindable.GetValue(ValidationRegExProperty);
        }
        #endregion




        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="property"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override AtomValidationError IsStringValid(string p, BindableProperty property, View e)
        {
            string regex = GetValidationRegEx(e);
            if (string.IsNullOrEmpty(regex) || Regex.IsMatch(p, regex))
                return null;
            return new AtomValidationError
            {
                Source = e,
                Property = property,
                Message = AtomForm.GetInvalidValueMessage(e)
            };
        }

    }
}
