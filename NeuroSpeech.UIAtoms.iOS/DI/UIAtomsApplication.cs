using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms
{
    public partial class UIAtomsApplication
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public CancellationTokenSource SetTimeout(Action action, TimeSpan ts)
        {

            CancellationTokenSource ct = new CancellationTokenSource();
            UIApplication.SharedApplication.Invoke(() => {
                if (!ct.IsCancellationRequested)
                {
                    try
                    {
                        action();
                    }
                    catch { }
                }
            },ts);


            //Device.BeginInvokeOnMainThread(async () => {
            //    try
            //    {
            //        await Task.Delay(ts, ct.Token);
            //    }
            //    catch (TaskCanceledException)
            //    {
            //        return;
            //    }
            //    if (ct.IsCancellationRequested)
            //        return;
            //    try
            //    {
            //        action();
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine(ex.ToString());
            //    }
            //});

            return ct;

        }

    }
}
