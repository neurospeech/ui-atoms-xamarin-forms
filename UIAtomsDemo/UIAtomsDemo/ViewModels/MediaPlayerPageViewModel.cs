using NeuroSpeech.UIAtoms;
using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using UIAtomsDemo.Views;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIAtomsDemo.ViewModels
{
    public class MediaPlayerPageViewModel : AtomViewModel
    {

        #region Property IsVideoDispose

        private bool _IsVideoDispose = true;

        public bool IsVideoDispose
        {
            get
            {
                return _IsVideoDispose;
            }
            set
            {
                SetProperty(ref _IsVideoDispose, value);
            }
        }

        #endregion

        #region Property VideoUrl

        // private AtomVideoSource _VideoUrl;

        public AtomVideoSource VideoUrl
        {
            get
            {
                return new AtomVideoSource
                {
                    //To test portrait mode
                    //Url = "https://d2lcywqhfczovm.cloudfront.net/tfs/229654/1529563/hn9y1tvgjvc3/general---profile-overview-home-app.mp4"

                    //To test landscape mode
                    Url = "https://d2lcywqhfczovm.cloudfront.net/tfs/37422/1498112/hn78dqe086s8/aae2632b-3abd-48e2-be1a-73a12f960f0a-vid-20170814-wa0009-1317852597.mp4/ios-360p.mp4"
                };
            }
        }

        #endregion

        private async Task PlayNextVideos()
        {
            IsVideoDispose = false;
            await appNavigator.PushAsync<SecondMediaPlayer>();
        }

        public ICommand NextCommand { get; }

        public MediaPlayerPageViewModel()
        {
            NextCommand = new AtomCommand(async () => await PlayNextVideos());
        }

        public override void OnAppearing()
        {

            base.OnAppearing();
            IsVideoDispose = true;

        }

    }
}
