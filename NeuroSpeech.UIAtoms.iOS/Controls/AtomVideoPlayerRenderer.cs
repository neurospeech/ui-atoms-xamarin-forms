using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using AVFoundation;
using UIKit;
using System.Threading.Tasks;
using Foundation;
//using NeuroSpeech.UIAtoms.iOS.Album;
using AVKit;
using CoreMedia;

[assembly: ExportRenderer(typeof(AtomVideoPlayer),typeof(AtomVideoPlayerRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomVideoPlayerRenderer: ViewRenderer<AtomVideoPlayer,UIView>
    {

        public AtomVideoPlayerRenderer()
        {

        }

        AVPlayer player;
        AVAsset asset;
        AVPlayerItem playerItem;
        AVPlayerViewController playerController;

        protected override void OnElementChanged(ElementChangedEventArgs<AtomVideoPlayer> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            var view = new UIView();
            SetNativeControl(view);
            SetVideoSource(Element.Source);
           
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Source") {
                SetVideoSource(Element.Source);
            }

            //if (e.PropertyName == "Poster") {
            //    SetVideoPoster(Element.Poster);
            //}

            if (e.PropertyName == "IsPlaying") {
                if (Element.IsPlaying)
                {
                    player?.Play();
                }
                else {
                    player?.Pause();
                }
            }
        }

        private void SetVideoPoster(string poster)
        {
            
        }

        private void SetVideoSource(AtomVideoSource source)
        {
            DisposeChildren();

            if (source == null)
                return;

            Device.BeginInvokeOnMainThread(async () => await SetSource(source));

        }

        protected override void Dispose(bool disposing)
        {
            DisposeChildren();
            base.Dispose(disposing);
        }


        private void DisposeChildren()
        {
            
            player?.Pause();

            playerController?.RemoveFromParentViewController();
            playerController?.Dispose();
            playerController = null;

            player?.Dispose();
            playerItem?.Dispose();

            player = null;

            playerItem = null;
        }

        private async Task SetSource(AtomVideoSource source)
        {
            asset = await LoadAsset(source);

            playerItem = new AVPlayerItem(asset);
            player = new AVPlayer(playerItem);

            playerController = new AVPlayerViewController();
            Control.AddSubview(playerController.View);
            playerController.Player = player;
            playerController.View.Frame = Control.Frame;

            player.Seek(CMTime.FromSeconds(1,1000));

            if (Element.IsPlaying) {
                player.Play();
            }
        }

        private Task<AVAsset> LoadAsset(AtomVideoSource source)
        {
            if (source.Url != null) {
                return Task.FromResult( AVAsset.FromUrl(new NSUrl(source.Url)));
            }

            if (source.FilePath != null) {
                return Task.FromResult(AVAsset.FromUrl(new NSUrl(source.FilePath, false)));
            }

            throw new NotImplementedException();
        }
    }
}
