using Foundation;
using NeuroSpeech.UIAtoms.Drawing;
using NeuroSpeech.UIAtoms.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using NeuroSpeech.UIAtoms.Controls;
using CoreGraphics;

[assembly: Xamarin.Forms.Dependency(typeof(AtomImageProvider))]

namespace NeuroSpeech.UIAtoms.Drawing
{
    public class AtomImageProvider : BaseAtomImageProvider<UIImage>
    {
        public override async Task<string> CropAsync(string source, CropRect cropRect)
        {
            var image = await LoadAsync(source);
            var d = cropRect.Destination;
            return await Task.Run<string>(async () =>
            {


                string name = System.IO.Path.GetFileNameWithoutExtension(source);
                string ext = System.IO.Path.GetExtension(source);
                var tempFile = $"{System.IO.Path.GetTempPath()}/{name}-{(Guid.NewGuid().ToString().Trim('{', '}'))}{ext}";
                var r = new CGRect(d.Left, d.Top, d.Width, d.Height);
                var cgi = image.CGImage.WithImageInRect(r);
                image = new UIImage(cgi);
                using (var s = System.IO.File.OpenWrite(tempFile))
                {
                    var iss = image.AsPNG().AsStream();
                    await iss.CopyToAsync(s);
                }
                return tempFile;
            });
        }

        public virtual async Task<string> RotateAsync(string source, int angle, string side)
        {
            var image = await LoadAsync(source);
            return await Task.Run<string>(async () =>
            {
                //var sourceSize = image.Size;
                //UIGraphics.BeginImageContextWithOptions(new CGSize(sourceSize.Height, sourceSize.Width), true, 1.0f);
                //CGContext bitmap = UIGraphics.GetCurrentContext();
                //bitmap.DrawImage(new CGRect(0, 0, sourceSize.Height, sourceSize.Width), image.CGImage);
                //bitmap.RotateCTM((float)(side.Equals("Left") ? Math.PI / 2 : -Math.PI / 2));
                

                //var r = new CGRect(0, 0, image.Size.Height, image.Size.Width);
                //var cgi = image.CGImage.WithImageInRect(r);
                //var bitmap1= new UIImage(cgi);


                string name = System.IO.Path.GetFileNameWithoutExtension(source);
                string ext = System.IO.Path.GetExtension(source);
                var tempFile = $"{System.IO.Path.GetTempPath()}/{name}-{(Guid.NewGuid().ToString().Trim('{', '}'))}{ext}";

                var newImage = ScaleImage(image,1);

                var imageRotation = side.Equals("Left") ? UIImageOrientation.Left : UIImageOrientation.Right;
                image  = UIImage.FromImage(image.CGImage, image.CurrentScale, imageRotation);

                using (var s = System.IO.File.OpenWrite(tempFile))
                {
                    var iss = image.AsPNG().AsStream();
                    await iss.CopyToAsync(s);
                }
                return tempFile;
            });
        }

        public override async Task<UIImage> LoadAsync(string source)
        {

            Uri img = new Uri(source.Trim(), UriKind.RelativeOrAbsolute);

            if (!img.IsAbsoluteUri)
            {
                if (!img.OriginalString.StartsWith("/"))
                {
                    return UIImage.FromBundle(img.OriginalString);
                }

                return UIImage.FromFile(img.OriginalString);
            }

            if (img.Scheme.Equals("res", StringComparison.OrdinalIgnoreCase))
            {

            }

            if (img.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                return UIImage.FromFile(img.AbsolutePath);
            }

            var data = await WebFetchAsync(img);

            var s = data as System.IO.Stream;
            if (s != null) {
                return UIImage.LoadFromData(NSData.FromStream(s));
            }

            var bytes = data as byte[];
            if (bytes != null)
            {
                return UIImage.LoadFromData(NSData.FromArray(bytes));
            }

            if (data is UIImage)
                return (UIImage)data;

            throw new NotSupportedException();
        }

        public static UIImage ScaleImage(UIImage image, int maxSize)
        {

            UIImage res;

            using (CGImage imageRef = image.CGImage)
            {
                CGImageAlphaInfo alphaInfo = imageRef.AlphaInfo;
                CGColorSpace colorSpaceInfo = CGColorSpace.CreateDeviceRGB();
                if (alphaInfo == CGImageAlphaInfo.None)
                {
                    alphaInfo = CGImageAlphaInfo.NoneSkipLast;
                }

                int width, height;

                width = Convert.ToInt32(imageRef.Width);
                height = Convert.ToInt32(imageRef.Height);


                if (height >= width)
                {
                    width = (int)Math.Floor((double)width * ((double)maxSize / (double)height));
                    height = maxSize;
                }
                else
                {
                    height = (int)Math.Floor((double)height * ((double)maxSize / (double)width));
                    width = maxSize;
                }


                CGBitmapContext bitmap;

                if (image.Orientation == UIImageOrientation.Up || image.Orientation == UIImageOrientation.Down)
                {
                    bitmap = new CGBitmapContext(IntPtr.Zero, width, height, imageRef.BitsPerComponent, imageRef.BytesPerRow, colorSpaceInfo, alphaInfo);
                }
                else
                {
                    bitmap = new CGBitmapContext(IntPtr.Zero, height, width, imageRef.BitsPerComponent, imageRef.BytesPerRow, colorSpaceInfo, alphaInfo);
                }

                switch (image.Orientation)
                {
                    case UIImageOrientation.Left:
                        bitmap.RotateCTM((float)Math.PI / 2);
                        bitmap.TranslateCTM(0, -height);
                        break;
                    case UIImageOrientation.Right:
                        bitmap.RotateCTM(-((float)Math.PI / 2));
                        bitmap.TranslateCTM(-width, 0);
                        break;
                    case UIImageOrientation.Up:
                        break;
                    case UIImageOrientation.Down:
                        bitmap.TranslateCTM(width, height);
                        bitmap.RotateCTM(-(float)Math.PI);
                        break;
                }

                bitmap.DrawImage(new CGRect(0, 0, width, height), imageRef);


                res = UIImage.FromImage(bitmap.ToImage());
                bitmap = null;

            }


            return res;
        }

    }
}
