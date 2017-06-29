using NeuroSpeech.UIAtoms.Controls;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using UIKit;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using NeuroSpeech.UIAtoms.Web;
using System.Net.Http;
using NeuroSpeech.UIAtoms.Drawing;

#if __UNIFIED__
using RectangleF = CoreGraphics.CGRect;
using SizeF = CoreGraphics.CGSize;
using PointF = CoreGraphics.CGPoint;

#else
using nfloat=System.Single;
using nint=System.Int32;
using nuint=System.UInt32;
#endif

[assembly: ExportRenderer(typeof(AtomImage), typeof(AtomImageRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomImageRenderer : ViewRenderer<AtomImage, AtomImageView>
    {
        bool _isDisposed;

        IElementController ElementController => Element as IElementController;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                UIImage oldUIImage;
                if (Control != null && (oldUIImage = Control.Image) != null)
                {
                    oldUIImage.Dispose();
                    oldUIImage = null;
                }
            }

            _isDisposed = true;

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AtomImage> e)
        {
            if (Control == null)
            {
                var imageView = new AtomImageView(RectangleF.Empty);
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                imageView.ClipsToBounds = true;
                SetNativeControl(imageView);
                imageView.AccessibilityIdentifier = Element?.AutomationId;
            }

            if (e.NewElement != null)
            {
                SetAspect();
                SetImage(e.OldElement);
                /*Device.BeginInvokeOnMainThread(async () => {
                    try
                    {
                        await SetImage(e.OldElement);
                    }
                    catch (TaskCanceledException) {
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                });*/
                SetOpacity();
                SetPadding();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(AtomImage.Source):
                case nameof(AtomImage.OverlaySource):
                    SetImage();
                    break;
                case nameof(AtomImage.Opacity):
                    SetOpacity();
                    break;
                case nameof(AtomImage.Aspect):
                    SetAspect();
                    break;
                case nameof(AtomImage.Padding):
                    SetPadding();
                    break;
                case nameof(AtomImage.AutomationId):
                    Control.AccessibilityIdentifier = Element?.AutomationId;
                    break;
                default:
                    break;
            }

        }

        private void SetPadding()
        {
            var p = Element.Padding;
            var f = Control.Frame;
            Control.Bounds = new RectangleF(p.Left, p.Top, f.Width - p.Left - p.Right, f.Height - p.Top - p.Bottom);
            
        }


        /*private void UpdateImage(AtomImage oldImage = null)
        {
            UIAtomsApplication.Instance.TriggerOnce(async () =>
            {
                try
                {
                    await SetImage(oldImage);
                }
                catch (TaskCanceledException) {
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
        }*/

        void SetAspect()
        {
            Control.ContentMode = Element.Aspect.ToUIViewContentMode();
        }

        async void SetImage(AtomImage oldElement = null)
        {
            try
            {
                var source = Element.Source;

                if (oldElement != null)
                {
                    var oldSource = oldElement.Source;
                    if (Equals(oldSource, source))
                        return;

                    //    //Control.Image = null;
                }

                if (string.IsNullOrWhiteSpace(source))
                {
                    //Control.Image = null;
                    return;
                }

                using (Control.ShowBusy())
                {

                    var bitmap = await CachedLoadImageAsync(source, Element.OverlaySource);

                    if (Element == null || !Equals(Element.Source, source))
                        return;

                    if (!_isDisposed)
                    {
                        Control.Image = bitmap;
                        ((IVisualElementController)Element).NativeSizeChanged();
                        var s = bitmap.Size;
                        Element.ImageSize = new Xamarin.Forms.Size(s.Width, s.Height);
                    }

                }

            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }


            /*if (source != null && (handler = Registrar.Registered.GetHandler<IImageSourceHandler>(source.GetType())) != null)
            {
                UIImage uiimage;
                try
                {
                    uiimage = await handler.LoadImageAsync(source, scale: (float)UIScreen.MainScreen.Scale);
                }
                catch (OperationCanceledException)
                {
                    uiimage = null;
                }

                var imageView = Control;
                if (imageView != null)
                    imageView.Image = uiimage;

                if (!_isDisposed)
                    ((IVisualElementController)Element).NativeSizeChanged();
            }
            else
                Control.Image = null;*/

            //if (!_isDisposed)
            //  ((IImageController)Element).SetIsLoading(false);
        }

        private async Task<UIImage> CachedLoadImageAsync(string source, string overlaySource)
        {

            var imageLoader = DependencyService.Get<AtomImageProvider>();

            if (string.IsNullOrWhiteSpace(overlaySource))
            {

                return await imageLoader.LoadAsync(source);

            }
            var cache = DependencyService.Get<ShortMemoryCache>();


            string key = $"atom-image-{overlaySource}-{source}";
            NSData ci = cache.Get(key) as NSData;


            if (ci != null)
                return new UIImage(ci);

            var bitmap = await imageLoader.LoadAsync(source);

            var overlay = await imageLoader.LoadAsync(overlaySource);



            UIGraphics.BeginImageContext(bitmap.Size);

            bitmap.Draw(new PointF(0, 0));

            overlay.Draw(new PointF((bitmap.Size.Width - overlay.Size.Width) / 2, (bitmap.Size.Height - overlay.Size.Height) / 2));

            bitmap = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();

            var data = await Task.Run(() => bitmap.AsJPEG());
            cache.Add(key, data, TimeSpan.FromMinutes(5));

            return bitmap;
        }


        void SetOpacity()
        {
            //Control.Opaque = Element.IsOpaque;
        }
    }



    public class AtomImageView : UIImageView
    {

        UIActivityIndicatorView _Progress;

        private static NSData _emptyBlock = null;

        public AtomImageView()
        {

        }

        public AtomImageView(RectangleF frame) : base(frame)
        {
        }

        public AtomDisposableAction ShowBusy(UIActivityIndicatorViewStyle style
            = UIActivityIndicatorViewStyle.Gray)
        {

            if (_Progress == null)
            {
                _Progress = new UIActivityIndicatorView(style);
            }

            _Progress.AutoresizingMask =
                UIViewAutoresizing.FlexibleTopMargin |
                UIViewAutoresizing.FlexibleRightMargin |
                UIViewAutoresizing.FlexibleBottomMargin |
                UIViewAutoresizing.FlexibleLeftMargin;

            //_Progress.Frame = new RectangleF(0,0,100, 100);

            //UIImage emptyDot = null;

            //if (_emptyBlock == null)
            //{
            //    var img = CreateEmptyImage();
            //    _emptyBlock = img.AsJPEG();
            //    emptyDot = img;
            //}
            //else {
            //    emptyDot = new UIImage(_emptyBlock);
            //}
            //this.Image = emptyDot;
            //this.InvalidateIntrinsicContentSize();

            this.Image = null;

            UIAtomsApplication.Instance.SetTimeout(() =>
            {

                var bounds = this.Bounds;

                UIView owner = this;

                while (bounds.Width == 0)
                {
                    owner = owner.Superview;
                    bounds = owner.Bounds;
                }

                // we have image.. forget it...
                if (this.Image != null)
                    return;

                _Progress.Center =
                    new CoreGraphics.CGPoint(bounds.GetMidX(), bounds.GetMidY());



                owner.AddSubview(_Progress);
                _Progress.StartAnimating();

            }, TimeSpan.FromMilliseconds(100));



            return new AtomDisposableAction(() =>
            {
                try
                {
                    if (_Progress.IsAnimating)
                        _Progress.StopAnimating();
                    _Progress.RemoveFromSuperview();
                }
                catch
                {

                }

            });
        }

        private UIImage CreateEmptyImage()
        {
            UIGraphics.BeginImageContext(new SizeF(500, 500));
            var context = UIGraphics.GetCurrentContext();
            context.SetFillColor(1, 1, 1, 1);
            context.FillRect(new RectangleF(0, 0, 500, 500));
            //UIGraphics.RectFill()
            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return img;
        }
    }


    public static class ImageExtensions
    {
        public static UIViewContentMode ToUIViewContentMode(this Aspect aspect)
        {
            switch (aspect)
            {
                case Aspect.AspectFill:
                    return UIViewContentMode.ScaleAspectFill;
                case Aspect.Fill:
                    return UIViewContentMode.ScaleToFill;
                case Aspect.AspectFit:
                default:
                    return UIViewContentMode.ScaleAspectFit;
            }
        }
    }
}
