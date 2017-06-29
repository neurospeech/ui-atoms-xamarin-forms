using NeuroSpeech.UIAtoms.Controls.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(ScrollableListView), typeof(ScrollableListViewRenderer))]

namespace NeuroSpeech.UIAtoms.Controls.Internal
{
    public class ScrollableListViewRenderer: ListViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

        }

        

        private void Control_Scrolled(object sender, EventArgs e)
        {
            var offset = Control.ContentOffset;
            var bounds = Control.Bounds;
            var size = Control.ContentSize;
            var inset = Control.ContentInset;
            var y = offset.Y + bounds.Size.Height - inset.Bottom;
            var h = size.Height;
            // NSLog(@"offset: %f", offset.y);   
            // NSLog(@"content.height: %f", size.height);   
            // NSLog(@"bounds.height: %f", bounds.size.height);   
            // NSLog(@"inset.top: %f", inset.top);   
            // NSLog(@"inset.bottom: %f", inset.bottom);   
            // NSLog(@"pos: %f of %f", y, h);

            float reload_distance = 10;
            if (y > h + reload_distance)
            {
                //NSLog(@"load more rows");

                (Element as IOverScrollView)?.InvokeOverScrolled();

            }
        }
    }
}
