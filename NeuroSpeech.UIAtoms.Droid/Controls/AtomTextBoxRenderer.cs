using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NeuroSpeech.UIAtoms.Controls;

[assembly: ExportRenderer(typeof(AtomTextBox), typeof(AtomTextBoxRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomTextBoxRenderer: EntryRenderer
    {

        public AtomTextBoxRenderer(Context context): base(context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            UpdateSuggestions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == AtomTextBox.DisableSuggestionsProperty.PropertyName)
                UpdateSuggestions();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateSuggestions()
        {
            var atb = Element as AtomTextBox;
            if (atb == null)
                return;
            var c = Control;
            if (c == null)
                return;

            c.SetMaxLines(1);

            //c.ImeOptions = Android.Views.InputMethods.ImeAction.Next;

            

            if (atb.DisableSuggestions)
            {
                c.InputType = c.InputType | Android.Text.InputTypes.TextFlagNoSuggestions;
            }
            else {
                c.InputType = c.InputType & (~Android.Text.InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}