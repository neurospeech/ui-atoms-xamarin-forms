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
                var tempFile = $"{UIAtomsApplication.Instance.CacheDir.FullName}/{name}-{(Guid.NewGuid().ToString().Trim('{', '}'))}{ext}";
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

    }
}
