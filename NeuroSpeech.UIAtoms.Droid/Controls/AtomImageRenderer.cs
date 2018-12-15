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

using AImageView = Android.Widget.ImageView;
using System.ComponentModel;
using Android.Graphics;
using System.Threading.Tasks;
using Java.IO;
using Android.Util;
using NeuroSpeech.UIAtoms.Web;
using System.IO;
using System.Net.Http;
using NeuroSpeech.UIAtoms.Drawing;

[assembly: ExportRenderer(typeof(AtomImage), typeof(AtomImageRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomImageRenderer : ViewRenderer<AtomImage, AImageView>
    {
        bool _isDisposed;

        /// <summary>
        /// 
        /// </summary>
        public AtomImageRenderer(Context context) : base(context)
        {
            AutoPackage = false;

            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<AtomImage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var view = new FormsImageView(Context);
                SetNativeControl(view);
                view.ContentDescription = Element?.AutomationId;
            }
            UpdateBitmap(e.OldElement);
            UpdateAspect();
            UpdatePadding();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(AtomImage.Source):
                case nameof(AtomImage.OverlaySource):
                    UpdateBitmap();
                    break;
                case nameof(AtomImage.Aspect):
                    UpdateAspect();
                    break;
                case nameof(AtomImage.Padding):
                    UpdatePadding();
                    break;
                case nameof(AtomImage.AutomationId):
                    Control.ContentDescription = Element?.AutomationId;
                    break;
                default:
                    break;
            }
        }

        private void UpdatePadding()
        {
            var p = Element.Padding;
            Control.SetPadding((int)p.Left, (int)p.Top, (int)p.Right, (int)p.Bottom);
        }

        void UpdateAspect()
        {
            AImageView.ScaleType type = AImageView.ScaleType.CenterCrop;

            switch (Element.Aspect)
            {
                case Aspect.AspectFit:
                    type = AImageView.ScaleType.FitCenter;
                    break;
                case Aspect.AspectFill:
                    type = AImageView.ScaleType.CenterCrop;
                    break;
                case Aspect.Fill:
                    type = AImageView.ScaleType.FitXy;
                    break;
                default:
                    break;
            }
            Control.SetScaleType(type);
        }

        async void UpdateBitmap(AtomImage previous = null)
        {
            try
            {
                Bitmap bitmap = null;

                string source = Element.Source;


                if (previous != null && Equals(previous.Source, Element.Source))
                {
                //    //System.Diagnostics.Debug.WriteLine($"{Element.Source} == {source}");
                    return;
                }

                var formsImageView = Control as FormsImageView;
                if (formsImageView != null)
                    formsImageView.SkipInvalidate();

                if (source == null)
                {
                    //Control.SetImageResource(global::Android.Resource.Color.Transparent);
                    //System.Diagnostics.Debug.WriteLine("source is null");
                    return;
                }

                if (!Element.ShowProgress) {

                    bitmap = await CachedLoadImageAsync(Element.Source, Element.OverlaySource);

                    if (Element == null || !Equals(Element.Source, source)) {
                        bitmap?.Dispose();
                        return;
                    }

                    if (!_isDisposed)
                    {
                        Control.SetImageBitmap(bitmap);
                        Element.ImageSize = new Xamarin.Forms.Size(bitmap.Width, bitmap.Height);
                        bitmap?.Dispose();

                        ((IVisualElementController)Element).NativeSizeChanged();
                    }

                    return;
                }

                using (AnimatedCircleDrawable a = new AnimatedCircleDrawable(Xamarin.Forms.Color.Accent.ToAndroid(), 4, Xamarin.Forms.Color.Transparent.ToAndroid(), 40))
                {

                    Control.SetImageDrawable(a);

                    bitmap = await CachedLoadImageAsync(Element.Source, Element.OverlaySource);

                    if (Element == null || !Equals(Element.Source, source))
                    {
                        //System.Diagnostics.Debug.WriteLine($"{Element.Source} != {source}");
                        bitmap?.Dispose();
                        return;
                    }

                    if (!_isDisposed)
                    {
                        Control.SetImageBitmap(bitmap);
                        Element.ImageSize = new Xamarin.Forms.Size(bitmap.Width, bitmap.Height);
                        bitmap?.Dispose();

                        //((IElementController)Element).SetValueFromRenderer(Image.IsLoadingPropertyKey, false);
                        ((IVisualElementController)Element).NativeSizeChanged();
                    }
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async Task<Bitmap> CachedLoadImageAsync(string source, string overlaySource)
        {

            var imageLoader = DependencyService.Get<AtomImageProvider>();

            if (string.IsNullOrWhiteSpace(overlaySource))
                return await imageLoader.LoadAsync(source);

            string key = $"atom-image-{overlaySource}-{source}";

            var cache = DependencyService.Get<ShortMemoryCache>();

            byte[] data = (cache.Get(key)) as byte[];
            if (data != null)
                return await BitmapFactory.DecodeByteArrayAsync(data,0,data.Length);

            var bitmap = await imageLoader.LoadAsync(source);

            if (!string.IsNullOrWhiteSpace(overlaySource))
            {
                var overlay = await imageLoader.LoadAsync(overlaySource);

                var newBitmap = Bitmap.CreateBitmap(bitmap.Width, bitmap.Height, bitmap.GetConfig());

                var canvas = new Canvas(newBitmap);

                canvas.DrawBitmap(bitmap, 0, 0, null);
                //canvas.DrawBitmap(overlay, new Rect(0,0,bitmap.Width,bitmap.Height),)

                canvas.DrawBitmap(overlay, (bitmap.Width - overlay.Width) / 2, (bitmap.Height - overlay.Height) / 2, null);

                bitmap = newBitmap;

            }

            using (MemoryStream ms = new MemoryStream()) {
                await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg,99,ms);
                cache.Add(key, ms.ToArray(), TimeSpan.FromSeconds(30));
            }           

            return bitmap;
        }

       


        class FormsImageView : ImageView
        {
            bool _skipInvalidate;

            public FormsImageView(Context context) : base(context)
            {
            }

            protected FormsImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
            {
            }

            public override void Invalidate()
            {
                if (_skipInvalidate)
                {
                    _skipInvalidate = false;
                    return;
                }

                base.Invalidate();
            }

            public void SkipInvalidate()
            {
                //_skipInvalidate = true;
            }
        }

    }
}