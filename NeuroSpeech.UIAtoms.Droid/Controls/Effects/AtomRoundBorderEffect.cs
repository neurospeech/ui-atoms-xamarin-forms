using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using NeuroSpeech.UIAtoms.Controls.Effects;

[assembly: ResolutionGroupName("UIAtomsEffects")]
[assembly: ExportEffect(typeof(AtomRoundBorderEffect), nameof(AtomRoundBorderEffect))]

namespace NeuroSpeech.UIAtoms.Controls.Effects
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomRoundBorderEffect : PlatformEffect
    {

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAttached()
        {

            var ef = Element.Effects.OfType<Controls.AtomRoundBorderEffect>().FirstOrDefault();

            GradientDrawable gd = new GradientDrawable();
            gd.SetColor( ef.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(ef.CornerRadius);
            gd.SetStroke(
                ef.StrokeWidth, 
                ef.StrokeColor.ToAndroid());
            if (Control != null)
            {
                Control.Background = gd;
            }
            if (Container != null) {
                Container.Background = gd;
            }
            //Control.SetBackgroundDrawable(gd);
            //this.Container.SetBackground(gd);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDetached()
        {
            
        }
    }
}