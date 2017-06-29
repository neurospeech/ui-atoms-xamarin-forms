using NeuroSpeech.UIAtoms.Controls.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("UIAtomsEffects")]
[assembly: ExportEffect(typeof(AtomRoundBorderEffect), nameof(AtomRoundBorderEffect))]


namespace NeuroSpeech.UIAtoms.Controls.Effects
{
    public class AtomRoundBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var ef = Element.Effects.OfType<Controls.AtomRoundBorderEffect>().FirstOrDefault();
            var cg = ef.BackgroundColor.ToCGColor();
            var layer = Control?.Layer ?? Container?.Layer;
            layer.BackgroundColor = ef.BackgroundColor.ToCGColor();
            layer.CornerRadius = ef.CornerRadius;
            layer.BorderColor = ef.StrokeColor.ToCGColor();
            layer.BorderWidth = ef.StrokeWidth;
        }

        protected override void OnDetached()
        {
            
        }
    }
}