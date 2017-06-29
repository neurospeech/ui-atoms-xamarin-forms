using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Validation
{
    public class AtomPasswordAgainValidationRule: AtomStringValidationRule
    {

        #region Password Attached Property
        /// <summary>
        /// Password Attached property
        /// </summary>
        public static readonly BindableProperty PasswordProperty =
            BindableProperty.CreateAttached("Password", typeof(string),
            typeof(AtomPasswordAgainValidationRule),
            null,
            BindingMode.OneWay,
            null,
            OnPasswordChanged);

        private static void OnPasswordChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set Password for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetPassword(BindableObject bindable, string newValue)
        {
            bindable.SetValue(PasswordProperty, newValue);
        }

        /// <summary>
        /// Get Password for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetPassword(BindableObject bindable)
        {
            return (string)bindable.GetValue(PasswordProperty);
        }
        #endregion

        #region PasswordMismatchError Attached Property
        /// <summary>
        /// PasswordMismatchError Attached property
        /// </summary>
        public static readonly BindableProperty PasswordMismatchErrorProperty =
            BindableProperty.CreateAttached("PasswordMismatchError", typeof(string),
            typeof(AtomPasswordAgainValidationRule),
            "Password does not match",
            BindingMode.OneWay,
            null,
            OnPasswordMismatchErrorChanged);

        private static void OnPasswordMismatchErrorChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set PasswordMismatchError for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetPasswordMismatchError(BindableObject bindable, string newValue)
        {
            bindable.SetValue(PasswordMismatchErrorProperty, newValue);
        }

        /// <summary>
        /// Get PasswordMismatchError for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetPasswordMismatchError(BindableObject bindable)
        {
            return (string)bindable.GetValue(PasswordMismatchErrorProperty);
        }
        #endregion



        protected override AtomValidationError IsStringValid(string v, BindableProperty property, View e)
        {
            string password = GetPassword(e);

            if (string.IsNullOrWhiteSpace(password) || password != v) {
                return new AtomValidationError {
                    Message = GetPasswordMismatchError(e),
                    Property = property,
                    Source = e
                };
            }

            return null;
        }

    }
}
