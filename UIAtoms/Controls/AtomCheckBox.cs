using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomCheckBox: View
    {

        /// <summary>
        /// 
        /// </summary>
        public AtomCheckBox()
        {

        }

        #region Property IsChecked

        /// <summary>
        /// Bindable Property IsChecked
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
          "IsChecked",
          typeof(bool),
          typeof(AtomCheckBox),
          false,
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
        /// On IsChecked changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsCheckedChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property IsChecked
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }
        #endregion



        #region Property Label

        /// <summary>
        /// Bindable Property Label
        /// </summary>
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
          "Label",
          typeof(string),
          typeof(AtomCheckBox),
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
        /// On Label changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnLabelChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Label
        /// </summary>
        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }
        #endregion




    }
}
