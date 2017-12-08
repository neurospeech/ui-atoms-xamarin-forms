using NeuroSpeech.UIAtoms;
using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIAtomsDemo.ViewModels
{
    public class MediaPlayerPageViewModel : AtomViewModel
    {
        #region Property VideoUrl

        private AtomVideoSource _VideoUrl;

        public AtomVideoSource VideoUrl
        {
            get
            {
                return new AtomVideoSource
                {
                    Url = "https://d2lcywqhfczovm.cloudfront.net/tfs/229654/1529563/hn9y1tvgjvc3/general---profile-overview-home-app.mp4"
                };
            }
        }

        #endregion
    }
}
