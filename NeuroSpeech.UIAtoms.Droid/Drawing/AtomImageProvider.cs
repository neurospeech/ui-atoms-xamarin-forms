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
using Android.Graphics;
using System.Threading.Tasks;
using Xamarin.Forms;
using NeuroSpeech.UIAtoms.Drawing;
using System.Net.Http;
using NeuroSpeech.UIAtoms.Web;
using Xamarin.Forms.Platform.Android;
using NeuroSpeech.UIAtoms.Controls;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(AtomImageProvider))]



namespace NeuroSpeech.UIAtoms.Drawing
{
    public class AtomImageProvider : BaseAtomImageProvider<Bitmap>
    {
        public override async Task<string> CropAsync(string source, CropRect cropRect)
        {
            var image = await LoadAsync(source);
            var d = cropRect.Destination;
            return await Task.Run<string>(() =>
            {

                string name = System.IO.Path.GetFileNameWithoutExtension(source);
                string ext = System.IO.Path.GetExtension(source);
                var tempFile = Java.IO.File.CreateTempFile(name, ext);
                image = Bitmap.CreateBitmap(image, (int)d.Left, (int)d.Top, (int)d.Width, (int)d.Height);
                tempFile.DeleteOnExit();
                using (var s = System.IO.File.OpenWrite(tempFile.CanonicalPath)) {
                    image.Compress(Bitmap.CompressFormat.Png, 0, s);
                }
                return tempFile.CanonicalPath;
            });
            
        }

        public override async Task<Bitmap> LoadAsync(string source)
        {

            Uri img = new Uri(source.Trim(), UriKind.RelativeOrAbsolute);

            if (!img.IsAbsoluteUri)
            {
                if (!img.OriginalString.StartsWith("/"))
                {
                    // load from resources....

                    return await Xamarin.Forms.Forms.Context.Resources.GetBitmapAsync(img.OriginalString);
                }

                return await BitmapFactory.DecodeFileAsync(img.OriginalString);
            }

            if (img.Scheme.Equals("res", StringComparison.OrdinalIgnoreCase))
            {

            }

            if (img.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                // load from file...
                return await BitmapFactory.DecodeFileAsync(img.AbsolutePath);
            }

            var data = await WebFetchAsync(img);

            var s = data as Stream;
            if (s != null) {
                return await BitmapFactory.DecodeStreamAsync(s);
            }

            var bytes = data as byte[];
            if (bytes != null)
            {
                return await BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length);
            }

            if (data is Bitmap)
                return (Bitmap)data;

            throw new NotSupportedException();

        }
    }
}