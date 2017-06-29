using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Validation
{
    /// <summary>
    /// AtomValidationRule is base class for Advanced Validation Rules used in UIAtoms Validation Framework.
    /// </summary>
    /// <remarks>
    /// AtomValidationRule provides an abstract method 
    /// <see cref="M:NeuroSpeech.UIAtoms.Validation.AtomValidationRule.Validate"/>
    /// that must be implemented by the child class that provides the validation logic. In case of no error, 
    /// the method returns null as return value, otherwise it returns 
    /// <see cref="T:NeuroSpeech.UIAtoms.Validation.AtomValidationError"/> object
    /// which contains the details of the Control, the error message and the dependency property that was
    /// validated.
    /// </remarks>
    public abstract class AtomValidationRule
    {

        public abstract AtomValidationError Validate(View view, BindableProperty property, object value);

        internal static AtomValidationError Validate(View content)
        {
            var validator = AtomForm.GetValidator(content);
            if (validator == null)
                return null;

            var rule = validator.ValidationRule;
            if (rule == null) {
                return null;
            }

            object value = null;

            var property = validator.BindableProperty;
            if (property != null)
            {
                value = content.GetValue(property);
            }
            else {
                if (validator.Property == null) {
                    throw new ArgumentNullException($"Both Property and BindableProperty are null for {validator.GetType().FullName} for {content}");
                }
                PropertyInfo px = content.GetType().GetProperty(validator.Property);
                value = px.GetValue(content);
            }

            var error = validator.ValidationRule.Validate(content, property, value);

            AtomForm.SetError(content, error?.Message);
            return error;
        }

    }

}
