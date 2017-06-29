using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Validation
{
    public class AtomStringValidationRule : AtomValidationRule
    {

        /// <summary>
        /// This class implements Validation rule to validate required text.
        /// </summary>
        /// <remarks>This validation is rule used by <see cref="T:NeuroSpeech.UIAtoms.Controls.AtomTextBox"/> 
        /// control to validate property <see cref="P:System.Windows.Controls.TextElement.Text"/>. 
        /// You can use this rule to validate any string property on the control. It returns 
        /// <see cref="T:NeuroSpeech.UIAtoms.Validations.AtomValidationError"/> if the string is empty.
        /// 
        /// The child classes implement more specific validation by overriding method 
        /// <see cref="M:NeuroSpeech.UIAtoms.Validations.AtomStringValidationRule.IsStringValid"/>
        /// to do specific business logic validation.
        /// </remarks>
        /// <param name="view">Control to validate</param>
        /// <param name="property">Dependency Property to validate</param>
        /// <param name="value">Text Value to validate</param>
        /// <returns></returns>
        public override AtomValidationError Validate(View e, BindableProperty property, object value)
        {
            //AtomTrace.WriteLine("AtomTextBox.Validate called...");

            string text = value as string;
            bool isRequired = AtomForm.GetIsRequired(e);
            if (isRequired && string.IsNullOrEmpty(text))
                return new AtomValidationError
                {
                    Property = property,
                    Message = AtomForm.GetMissingValueMessage(e),
                    Source = e
                };

            int min = GetMinimumLength(e);
            if (min != -1 && min != 0)
            {
                if (text == null || text.Length < min)
                {
                    return new AtomValidationError
                    {
                        Property = property,
                        Message = string.Format(AtomStringValidationRule.GetMinimumLengthErrorMessage(e), min),
                        Source = e
                    };
                }
            }

            int max = GetMaximumLength(e);
            if (max != -1 && max != 0 && max != int.MaxValue)
            {
                if (text.Length > max)
                {
                    return new AtomValidationError
                    {
                        Property = property,
                        Message = string.Format(AtomStringValidationRule.GetMaximumLengthErrorMessage(e), min),
                        Source = e
                    };
                }
            }

            return IsStringValid(value as string, property, e);
        }

        protected virtual AtomValidationError IsStringValid(string v, BindableProperty property, View e)
        {
            return null;
        }

        public static AtomStringValidationRule Instance = new AtomStringValidationRule();


        #region MinimumLength Attached Property
        /// <summary>
        /// MinimumLength Attached property
        /// </summary>
        public static readonly BindableProperty MinimumLengthProperty =
            BindableProperty.CreateAttached("MinimumLength", typeof(int),
            typeof(AtomStringValidationRule),
            0,
            BindingMode.OneWay,
            null,
            OnMinimumLengthChanged);

        private static void OnMinimumLengthChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set MinimumLength for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetMinimumLength(BindableObject bindable, int newValue)
        {
            bindable.SetValue(MinimumLengthProperty, newValue);
        }

        /// <summary>
        /// Get MinimumLength for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static int GetMinimumLength(BindableObject bindable)
        {
            return (int)bindable.GetValue(MinimumLengthProperty);
        }
        #endregion

        #region MaximumLength Attached Property
        /// <summary>
        /// MaximumLength Attached property
        /// </summary>
        public static readonly BindableProperty MaximumLengthProperty =
            BindableProperty.CreateAttached("MaximumLength", typeof(int),
            typeof(AtomStringValidationRule),
            int.MaxValue,
            BindingMode.OneWay,
            null,
            OnMaximumLengthChanged);

        private static void OnMaximumLengthChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set MaximumLength for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetMaximumLength(BindableObject bindable, int newValue)
        {
            bindable.SetValue(MaximumLengthProperty, newValue);
        }

        /// <summary>
        /// Get MaximumLength for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static int GetMaximumLength(BindableObject bindable)
        {
            return (int)bindable.GetValue(MaximumLengthProperty);
        }
        #endregion

        #region MinimumLengthErrorMessage Attached Property
        /// <summary>
        /// MinimumLengthErrorMessage Attached property
        /// </summary>
        public static readonly BindableProperty MinimumLengthErrorMessageProperty =
            BindableProperty.CreateAttached("MinimumLengthErrorMessage", typeof(string),
            typeof(AtomStringValidationRule),
            "Text is too short",
            BindingMode.OneWay,
            null,
            OnMinimumLengthErrorMessageChanged);

        private static void OnMinimumLengthErrorMessageChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set MinimumLengthErrorMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetMinimumLengthErrorMessage(BindableObject bindable, string newValue)
        {
            bindable.SetValue(MinimumLengthErrorMessageProperty, newValue);
        }

        /// <summary>
        /// Get MinimumLengthErrorMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetMinimumLengthErrorMessage(BindableObject bindable)
        {
            return (string)bindable.GetValue(MinimumLengthErrorMessageProperty);
        }
        #endregion

        #region MaximumLengthErrorMessage Attached Property
        /// <summary>
        /// MaximumLengthErrorMessage Attached property
        /// </summary>
        public static readonly BindableProperty MaximumLengthErrorMessageProperty =
            BindableProperty.CreateAttached("MaximumLengthErrorMessage", typeof(string),
            typeof(AtomStringValidationRule),
            "Text is too big",
            BindingMode.OneWay,
            null,
            OnMaximumLengthErrorMessageChanged);

        private static void OnMaximumLengthErrorMessageChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set MaximumLengthErrorMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetMaximumLengthErrorMessage(BindableObject bindable, string newValue)
        {
            bindable.SetValue(MaximumLengthErrorMessageProperty, newValue);
        }

        /// <summary>
        /// Get MaximumLengthErrorMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetMaximumLengthErrorMessage(BindableObject bindable)
        {
            return (string)bindable.GetValue(MaximumLengthErrorMessageProperty);
        }
        #endregion



    }
}
