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
using Android.Media;
using NeuroSpeech.UIAtoms.Controls;
using System.ComponentModel;
using Xamarin.Forms;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AtomVideoPlayer),typeof(AtomVideoPlayerRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomVideoPlayerRenderer : ViewRenderer<AtomVideoPlayer,Android.Widget.RelativeLayout>
    {

        /// <summary>
        /// 
        /// </summary>
        public AtomVideoPlayerRenderer()
        {

        }

        VideoView videoView = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<AtomVideoPlayer> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;
            
            videoView = new VideoView(Xamarin.Forms.Forms.Context);
            var vlp = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            vlp.AddRule(LayoutRules.CenterInParent);
            //vlp.AddRule(LayoutRules.AlignParentLeft);
            //vlp.AddRule(LayoutRules.AlignParentRight);
            //vlp.AddRule(LayoutRules.AlignParentBottom);
            videoView.LayoutParameters = vlp;

            //vlp.AddRule(LayoutRules.fill);
            var mc = new MediaController(Xamarin.Forms.Forms.Context);
            videoView.SetMediaController(mc);
            
            


            var ctrl = new Android.Widget.RelativeLayout(Xamarin.Forms.Forms.Context);
            ctrl.AddView(videoView);
            SetNativeControl(ctrl);

            ResetVideo();

            if (Element.IsPlaying) {
                videoView.Start();
            }
            else
            {
                videoView.StopPlayback();        
            }
            mc.Show(0);
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Source") {
                ResetVideo();
            }
            if (e.PropertyName == "IsPlaying") {
                if (videoView.IsPlaying != Element.IsPlaying) {
                    if (Element.IsPlaying)
                    {
                        videoView.Start();
                    }
                    else {
                        videoView.StopPlayback();
                    }
                }
            }
            if (e.PropertyName == nameof(AtomVideoPlayer.IsVisible)) {
                if (!Element.IsVisible) {
                    videoView.StopPlayback();
                    videoView.Visibility = ViewStates.Gone;
                }
                else
                {
                    videoView.Start();
                    videoView.Visibility = ViewStates.Visible;
                }
            }
            
        }

        private void ResetVideo()
        {
            var source = Element.Source;
            if (source == null) {
                videoView.SetVideoURI(null);
                return;
            }

            var filePath = source.FilePath;
            if (filePath != null) {
                videoView.SetVideoPath(filePath);
                videoView.SeekTo(1);
                return;
            }

            var fileUri = source.Url;
            if (fileUri != null) {
                videoView.SetVideoURI(Android.Net.Uri.Parse(fileUri));
                videoView.SeekTo(1);
                return;
            }

        }
    }
}