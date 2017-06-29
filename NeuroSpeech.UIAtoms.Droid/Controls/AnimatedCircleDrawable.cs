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
using Android.Graphics.Drawables;
using Android.Graphics;
using System.Collections.Concurrent;

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class AnimatedCircleDrawable : AnimationDrawable
    {

        //private int bg;
        //private int color;

        private static Dictionary<string, CircleDrawable[]> cache
            = new Dictionary<string, CircleDrawable[]>();

        private int size;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="javaReference"></param>
        /// <param name="transfer"></param>
        public AnimatedCircleDrawable(IntPtr javaReference, JniHandleOwnership transfer):base(javaReference,transfer)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="strokeWidth"></param>
        /// <param name="bg"></param>
        /// <param name="size"></param>
        public AnimatedCircleDrawable(int color, float strokeWidth, int bg, int size)
        {
            string key = $"c:{color},w{strokeWidth},bg:{bg},s:{size}";

            this.size = size;

            CircleDrawable[] drawables = null;

            if (!cache.TryGetValue(key, out drawables)) {
                drawables = new CircleDrawable[36];
                for (int i = 0; i < 36; i++)
                {
                    drawables[i] = new CircleDrawable(i * 10, color, strokeWidth, bg, size);
                }
                cache[key] = drawables;
            }

            foreach(var d in drawables)
            {
                AddFrame(d, 10);
            }
            Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public override int IntrinsicWidth
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int IntrinsicHeight
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int MinimumHeight
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int MinimumWidth
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if(disposing)
                    Stop();
            }
            catch{
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        public class CircleDrawable : Drawable
        {

            private int angle;
            private int size;
            Paint stroke;
            Paint background;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="angle"></param>
            /// <param name="color"></param>
            /// <param name="strokeWidth"></param>
            /// <param name="bg"></param>
            /// <param name="size"></param>
            public CircleDrawable(
                    int angle,
                    int color,
                    float strokeWidth,
                    int bg,
                    int size)
            {


                this.size = size;

                stroke = new Paint();
                stroke.SetStyle(Paint.Style.Stroke);
                stroke.StrokeWidth = strokeWidth;
                stroke.AntiAlias = true;
                stroke.Color = new Color(color);

                background = new Paint();
                background.Color = new Color(bg);
                background.SetStyle(Paint.Style.Fill);

                this.angle = angle;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="canvas"></param>
            public override void Draw(Canvas canvas)
            {
                //int s = canvas.save();

                RectF rect = new RectF(0, 0, canvas.Width, canvas.Height);
                float left = (rect.Width() - size) / 2;
                float top = (rect.Height() - size) / 2;
                RectF arcRect = new RectF(left, top, left + size, top + size);

                // draw background...
                canvas.DrawRect(rect, background);

                canvas.DrawArc(arcRect, angle, 270, false, stroke);

                //canvas.restoreToCount(s);



            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="alpha"></param>
            public override void SetAlpha(int alpha)
            {

            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="colorFilter"></param>
            public override void SetColorFilter(ColorFilter colorFilter)
            {

            }

            /// <summary>
            /// 
            /// </summary>
            public override int Opacity => 0;
        }
    }
}