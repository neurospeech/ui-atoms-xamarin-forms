using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// An Image with an option of True/False image with IsTrue property binding
    /// </summary>
    public class AtomBooleanImage: Image
    {

        #region Property TrueSource

        /// <summary>
        /// Bindable Property TrueSource
        /// </summary>
        public static readonly BindableProperty TrueSourceProperty = BindableProperty.Create(
          "TrueSource",
          typeof(ImageSource),
          typeof(AtomBooleanImage),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomBooleanImage)sender).OnTrueSourceChanged(oldValue,newValue),
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
        /// On TrueSource changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTrueSourceChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property TrueSource
        /// </summary>
        public ImageSource TrueSource
        {
            get
            {
                return (ImageSource)GetValue(TrueSourceProperty);
            }
            set
            {
                SetValue(TrueSourceProperty, value);
            }
        }
        #endregion

        #region Property FalseSource

        /// <summary>
        /// Bindable Property FalseSource
        /// </summary>
        public static readonly BindableProperty FalseSourceProperty = BindableProperty.Create(
          "FalseSource",
          typeof(ImageSource),
          typeof(AtomBooleanImage),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomBooleanImage)sender).OnFalseSourceChanged(oldValue,newValue),
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
        /// On FalseSource changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnFalseSourceChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property FalseSource
        /// </summary>
        public ImageSource FalseSource
        {
            get
            {
                return (ImageSource)GetValue(FalseSourceProperty);
            }
            set
            {
                SetValue(FalseSourceProperty, value);
            }
        }
        #endregion

        #region Property IsTrue

        /// <summary>
        /// Bindable Property IsTrue
        /// </summary>
        public static readonly BindableProperty IsTrueProperty = BindableProperty.Create(
          "IsTrue",
          typeof(bool),
          typeof(AtomBooleanImage),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomBooleanImage)sender).OnIsTrueChanged(oldValue,newValue),
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

        
        /// <summary>
        /// On IsTrue changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsTrueChanged(object oldValue, object newValue)
        {
            
        }


        /// <summary>
        /// Property IsTrue
        /// </summary>
        public bool IsTrue
        {
            get
            {
                return (bool)GetValue(IsTrueProperty);
            }
            set
            {
                SetValue(IsTrueProperty, value);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName) {
                case "TrueSource":
                case "FalseSource":
                case "IsTrue":

                    this.Source = IsTrue ? TrueSource : FalseSource;
                    break;
            }
        }



    }



}
