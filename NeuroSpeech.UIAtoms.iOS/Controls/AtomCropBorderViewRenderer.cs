using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreAnimation;
using CoreGraphics;
using System.ComponentModel;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AtomMaskBox), typeof(AtomMaskBoxRenderer))]
namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomMaskBoxRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            UpdateMask();
            
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName) {
                case nameof(AtomMaskBox.MaskRect):
                    UpdateMask();
                    break;
                case nameof(AtomMaskBox.BackgroundColor):
                    UpdateMask();
                    break;
            }
        }

        private void UpdateMask()
        {
            if (this.NativeView == null)
                return;

            var mb = this.Element as AtomMaskBox;
            if (mb == null)
                return;

            //this.NativeView.BackgroundColor = mb.BackgroundColor.ToUIColor();
            //this.BackgroundColor = Color.Transparent.ToUIColor();

            var rect = mb.MaskRect;
            if (double.IsNaN(rect.Width) 
                || double.IsNaN(rect.Height)
                || double.IsInfinity(rect.Width)
                || double.IsInfinity(rect.Height)
                || rect.Width <= 0
                || rect.Height <= 0)
                return;

            //System.Diagnostics.Debug.WriteLine($"{rect.X},{rect.Y},{rect.Width},{rect.Height}");

            var mask = new CAShapeLayer();
            var path = CGPath.FromRect(this.NativeView.Bounds);
            path.AddRect(mb.MaskRect.ToRectangleF());

            mask.Path = path;
            //mask.BackgroundColor = Color.Transparent.ToCGColor();
            //mask.FillColor = mb.BackgroundColor.ToCGColor();
            mask.FillRule = CAShapeLayer.FillRuleEvenOdd;
            NativeView.Layer.Mask = mask;

            //this.NativeView.BackgroundColor = Color.Transparent.ToUIColor();
        }
    }
}
