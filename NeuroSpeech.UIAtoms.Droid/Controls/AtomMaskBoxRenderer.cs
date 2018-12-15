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
using NeuroSpeech.UIAtoms.Controls;
using Xamarin.Forms;
using System.ComponentModel;
using Android.Graphics;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AtomMaskBox), typeof(AtomMaskBoxRenderer))]
namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomMaskBoxRenderer: FrameRenderer
    {

        public AtomMaskBoxRenderer(Context context) : base(context)
        {
            this.SetClipChildren(true);
            this.SetWillNotDraw(false);
        }

        

        private Path path;
        private Paint paint;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            UpdateMask();
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(AtomMaskBox.MaskRect):
                case nameof(AtomMaskBox.BackgroundColor):
                case nameof(AtomMaskBox.Width):
                case nameof(AtomMaskBox.Height):
                    UpdateMask();
                    break;
            }
        }


        private void UpdateMask() {

            

            var mb = Element as AtomMaskBox;
            if (mb == null)
                return;

            var r = mb.MaskRect;
            if (double.IsNaN(r.Width)
                || double.IsInfinity(r.Width)
                || r.Width <= 0
                || double.IsNaN(r.Height)
                || double.IsInfinity(r.Height)
                || r.Height <= 0)
                return;

            path = new Path();

            var c = this.Context;
            
            path.AddRect(c.ToPixels(r.Left), c.ToPixels(r.Top), c.ToPixels(r.Right), c.ToPixels(r.Bottom), Path.Direction.Cw);


            paint = new Paint();
            paint.Color = Element.BackgroundColor.ToAndroid();

            SetBackgroundColor(Android.Graphics.Color.Transparent);

            this.Invalidate();
        }



        protected override void OnDraw(Canvas canvas)
        {
            if (path != null)
            {
                canvas.ClipPath(path, Region.Op.Difference);
                canvas.DrawRect(0, 0, Width+1, Height+1, paint);
            }
        }

        
    }
}