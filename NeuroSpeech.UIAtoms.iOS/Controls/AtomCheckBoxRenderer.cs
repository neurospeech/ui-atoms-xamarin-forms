using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AtomCheckBox),typeof(AtomCheckBoxRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomCheckBoxRenderer : ViewRenderer<AtomCheckBox,UIButton>
    {

        public AtomCheckBoxRenderer()
        {
            
        }


        protected override void OnElementChanged(ElementChangedEventArgs<AtomCheckBox> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            var button = new UIButton(UIButtonType.RoundedRect);
        }

    }
}
