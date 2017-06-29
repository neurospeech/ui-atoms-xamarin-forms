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

using Xamarin.Forms;
using NeuroSpeech.UIAtoms.Controls.Internal;

//[assembly: ExportRenderer(typeof(ScrollableListView), typeof(ScrollableListViewRenderer))]


namespace NeuroSpeech.UIAtoms.Controls.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class ScrollableListViewRenderer: ListViewRenderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null) {
                Control.Scroll += Control_Scroll;
            }
        }

        DateTime lastScroll = DateTime.MinValue;

        private void Control_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            if (e.FirstVisibleItem + e.VisibleItemCount == e.TotalItemCount)
            {
                var s = DateTime.UtcNow;
                if ((s - lastScroll).TotalMilliseconds > 500)
                {
                    ((IOverScrollView)Element).InvokeOverScrolled();
                }
                lastScroll = s;
            }
        }
    }
}