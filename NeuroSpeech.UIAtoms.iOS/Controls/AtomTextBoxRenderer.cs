using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.Linq;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(AtomTextBox), typeof(AtomTextBoxRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomTextBoxRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            UpdateSuggestions();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == AtomTextBox.DisableSuggestionsProperty.PropertyName)
                UpdateSuggestions();
        }

        private void UpdateSuggestions()
        {
            var atb = Element as AtomTextBox;
            if (atb == null)
                return;
            var c = Control;
            if (c == null)
                return;

            //c.ReturnKeyType = UIKit.UIReturnKeyType.Next;
            //c.ShouldReturn = ShouldReturn;
            

            

            if (atb.DisableSuggestions)
            {
                c.SpellCheckingType = UIKit.UITextSpellCheckingType.No;
                c.AutocorrectionType = UIKit.UITextAutocorrectionType.No;
                c.AutocapitalizationType = UIKit.UITextAutocapitalizationType.None;
            }
            else {
                c.SpellCheckingType = UIKit.UITextSpellCheckingType.Default;
                c.AutocorrectionType = UIKit.UITextAutocorrectionType.Default;
                c.AutocapitalizationType = UIKit.UITextAutocapitalizationType.Words;
            }

        }

        private bool ShouldReturn(UITextField tf)
        {

            // lets find next available UITextField or UIButton...

            var root = tf.RootView();

            var list = root.AllChildren();

            var index = list.FindIndex(x => x == tf);

            list = list.Take(index - 1).ToList();

            foreach (var item in list) {
                if (item is UITextField) {
                    
                    var a = item;
                    a.BecomeFirstResponder();                    
                    return false;
                }
            }
            tf.ResignFirstResponder();
            return false;
        }

    }
}
