using System;
using System.Linq;
using System.Collections.Generic;
using NeuroSpeech.UIAtoms.DI;
using NeuroSpeech.UIAtoms.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    public class AtomMaskBox : AtomFrame
    {

        public AtomMaskBox()
        {
            this.OutlineColor = Color.Transparent;
            this.BackgroundColor = Color.FromRgba(0, 0, 0, 0.5);
            this.InputTransparent = true;
            this.HasShadow = false;
        }

        #region Property MaskRect

        /// <summary>
        /// Bindable Property MaskRect
        /// </summary>
        public static readonly BindableProperty MaskRectProperty = BindableProperty.Create(
          nameof(MaskRect),
          typeof(Rectangle),
          typeof(AtomMaskBox),
          Rectangle.Zero,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomMaskBox)sender).OnMaskRectChanged(oldValue,newValue),
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
        /// On MaskRect changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnMaskRectChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property MaskRect
        /// </summary>
        public Rectangle MaskRect
        {
            get
            {
                return (Rectangle)GetValue(MaskRectProperty);
            }
            set
            {
                SetValue(MaskRectProperty, value);
            }
        }
        #endregion




    }

}