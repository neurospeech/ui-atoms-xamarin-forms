using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Validation
{
    public class AtomPasswordValidationRule: AtomStringValidationRule
    {

        #region RequiresUpperCase Attached Property
        /// <summary>
        /// RequiresUpperCase Attached property
        /// </summary>
        public static readonly BindableProperty RequiresUpperCaseProperty =
            BindableProperty.CreateAttached("RequiresUpperCase", typeof(bool),
            typeof(AtomPasswordValidationRule),
            false,
            BindingMode.OneWay,
            null,
            OnRequiresUpperCaseChanged);

        private static void OnRequiresUpperCaseChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set RequiresUpperCase for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetRequiresUpperCase(BindableObject bindable, bool newValue)
        {
            bindable.SetValue(RequiresUpperCaseProperty, newValue);
        }

        /// <summary>
        /// Get RequiresUpperCase for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static bool GetRequiresUpperCase(BindableObject bindable)
        {
            return (bool)bindable.GetValue(RequiresUpperCaseProperty);
        }
        #endregion


        #region RequiresLowerCase Attached Property
        /// <summary>
        /// RequiresLowerCase Attached property
        /// </summary>
        public static readonly BindableProperty RequiresLowerCaseProperty =
            BindableProperty.CreateAttached("RequiresLowerCase", typeof(bool),
            typeof(AtomPasswordValidationRule),
            true,
            BindingMode.OneWay,
            null,
            OnRequiresLowerCaseChanged);

        private static void OnRequiresLowerCaseChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set RequiresLowerCase for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetRequiresLowerCase(BindableObject bindable, bool newValue)
        {
            bindable.SetValue(RequiresLowerCaseProperty, newValue);
        }

        /// <summary>
        /// Get RequiresLowerCase for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static bool GetRequiresLowerCase(BindableObject bindable)
        {
            return (bool)bindable.GetValue(RequiresLowerCaseProperty);
        }
        #endregion


        #region RequiresSymbol Attached Property
        /// <summary>
        /// RequiresSymbol Attached property
        /// </summary>
        public static readonly BindableProperty RequiresSymbolProperty =
            BindableProperty.CreateAttached("RequiresSymbol", typeof(bool),
            typeof(AtomPasswordValidationRule),
            false,
            BindingMode.OneWay,
            null,
            OnRequiresSymbolChanged);

        private static void OnRequiresSymbolChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set RequiresSymbol for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetRequiresSymbol(BindableObject bindable, bool newValue)
        {
            bindable.SetValue(RequiresSymbolProperty, newValue);
        }

        /// <summary>
        /// Get RequiresSymbol for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static bool GetRequiresSymbol(BindableObject bindable)
        {
            return (bool)bindable.GetValue(RequiresSymbolProperty);
        }
        #endregion


        #region RequiresNumber Attached Property
        /// <summary>
        /// RequiresNumber Attached property
        /// </summary>
        public static readonly BindableProperty RequiresNumberProperty =
            BindableProperty.CreateAttached("RequiresNumber", typeof(bool),
            typeof(AtomPasswordValidationRule),
            false,
            BindingMode.OneWay,
            null,
            OnRequiresNumberChanged);

        private static void OnRequiresNumberChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set RequiresNumber for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetRequiresNumber(BindableObject bindable, bool newValue)
        {
            bindable.SetValue(RequiresNumberProperty, newValue);
        }

        /// <summary>
        /// Get RequiresNumber for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static bool GetRequiresNumber(BindableObject bindable)
        {
            return (bool)bindable.GetValue(RequiresNumberProperty);
        }
        #endregion


        #region InvalidPasswordMessage Attached Property
        /// <summary>
        /// InvalidPasswordMessage Attached property
        /// </summary>
        public static readonly BindableProperty InvalidPasswordMessageProperty =
            BindableProperty.CreateAttached("InvalidPasswordMessage", typeof(string),
            typeof(AtomPasswordValidationRule),
            "Password must have atleast one small character",
            BindingMode.OneWay,
            null,
            OnInvalidPasswordMessageChanged);

        private static void OnInvalidPasswordMessageChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// Set InvalidPasswordMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="newValue"></param>
        public static void SetInvalidPasswordMessage(BindableObject bindable, string newValue)
        {
            bindable.SetValue(InvalidPasswordMessageProperty, newValue);
        }

        /// <summary>
        /// Get InvalidPasswordMessage for bindable object
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        public static string GetInvalidPasswordMessage(BindableObject bindable)
        {
            return (string)bindable.GetValue(InvalidPasswordMessageProperty);
        }
        #endregion




        protected override AtomValidationError IsStringValid(string v, BindableProperty property, View e)
        {
            var error = new AtomValidationError
            {
                Message = GetInvalidPasswordMessage(e),
                Property = property,
                Source = e
            };

            if (GetRequiresLowerCase(e)) {
                if (!v.Any(c => Char.IsLower(c))) {
                    return error;
                }
            }

            if (GetRequiresUpperCase(e)) {
                if (!v.Any(c => char.IsUpper(c)))
                    return error;
            }

            if (GetRequiresNumber(e)) {
                if (!v.Any(c => Char.IsNumber(c)))
                    return error;
            }

            if (GetRequiresSymbol(e)) {
                if (!v.Any(c => Char.IsSymbol(c)))
                    return error;
            }

            return null;
        }

    }
}
