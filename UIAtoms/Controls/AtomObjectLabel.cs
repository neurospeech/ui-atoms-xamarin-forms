using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomObjectLabel: Label
    {
        public AtomObjectLabel()
        {

        }


        #region Property TextPath

        /// <summary>
        /// Bindable Property TextPath
        /// </summary>
        public static readonly BindableProperty TextPathProperty = BindableProperty.Create(
          "TextPath",
          typeof(string),
          typeof(AtomObjectLabel),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomObjectLabel)sender).OnTextPathChanged(oldValue,newValue),
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
        /// On TextPath changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTextPathChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property TextPath
        /// </summary>
        public string TextPath
        {
            get
            {
                return (string)GetValue(TextPathProperty);
            }
            set
            {
                SetValue(TextPathProperty, value);
            }
        }
        #endregion



        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            UpdateLabel();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(TextPath)) {
                UpdateLabel();
            }
        }

        private void UpdateLabel()
        {
            object bc = this.BindingContext;
            if (bc == null)
                return;
            string tp = this.TextPath;
            if (tp == null)
                return;
            try {
                Text = bc.GetPropertyValue(tp)?.ToString() ?? ""; 
            } catch {
                Text = bc.ToString();
            }
        }
    }
}
