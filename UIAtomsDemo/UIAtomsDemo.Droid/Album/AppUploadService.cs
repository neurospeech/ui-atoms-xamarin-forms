using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

//[assembly: Xamarin.Forms.Dependency(typeof(AppUploadService))]

namespace UIAtomsDemo.Droid.Album
{
    //public class AppUploadService : BaseFileUploaderService
    //{

    //    /// <summary>
    //    /// Lets keep chunksize to only 5kb
    //    /// </summary>
    //    /// <param name="file"></param>
    //    /// <param name="fileSize"></param>
    //    /// <returns></returns>
    //    public override long GetChunkSize(UploadRequest file, long fileSize)
    //    {
    //        return 5120;
    //    }

    //    public override async Task UploadAsync(UploadRequest file, UploadRequestBlock block, Stream stream, Func<int, Task> onProgress = null)
    //    {
    //        for (int i = 0; i < 100; i++) {
    //            await onProgress(i);
    //            await Task.Delay(1000);
    //        }

    //    }

    //    public override async Task UploadCompleteAsync(UploadRequest file, List<UploadRequestBlock> list)
    //    {
    //        await Task.Delay(1000);
    //    }
    //}
}