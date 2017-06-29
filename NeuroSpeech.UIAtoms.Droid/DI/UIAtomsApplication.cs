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
using System.Threading;

namespace NeuroSpeech.UIAtoms
{
    public partial class UIAtomsApplication
    {

        private Handler _handler = null;
        private Handler Handler {
            get {
                return _handler ?? (_handler = new Handler(Looper.MainLooper));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public CancellationTokenSource SetTimeout(Action action, TimeSpan ts)
        {

            CancellationTokenSource ct = new CancellationTokenSource();

            Handler.PostDelayed(() => {
                if (!ct.IsCancellationRequested) {
                    try {
                        action();
                    } catch {
                    }
                }
            }, (long)ts.TotalMilliseconds);

            return ct;

        }
    }
}