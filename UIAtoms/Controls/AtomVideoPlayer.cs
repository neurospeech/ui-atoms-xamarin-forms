using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomVideoPlayer: View
    {

        #region Property Source

        /// <summary>
        /// Bindable Property Source
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
          "Source",
          typeof(AtomVideoSource),
          typeof(AtomVideoPlayer),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
           (sender,oldValue,newValue) => ((AtomVideoPlayer)sender).OnSourceChanged(oldValue,newValue),
          //null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          // () => Default(T)
          null
        );

        
        /// <summary>
        /// On Source changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSourceChanged(object oldValue, object newValue)
        {
            if (AutoPlay)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsPlaying = true;
                });
            }
        }


        /// <summary>
        /// Property Source
        /// </summary>
        public AtomVideoSource Source
        {
            get
            {
                return (AtomVideoSource)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        #endregion

        #region Property AutoPlay

        /// <summary>
        /// Bindable Property AutoPlay
        /// </summary>
        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(
          "AutoPlay",
          typeof(bool),
          typeof(AtomVideoPlayer),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          // (sender,oldValue,newValue) => {}
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          // () => Default(T)
          null
        );

        /*
        /// <summary>
        /// On AutoPlay changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAutoPlayChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property AutoPlay
        /// </summary>
        public bool AutoPlay
        {
            get
            {
                return (bool)GetValue(AutoPlayProperty);
            }
            set
            {
                SetValue(AutoPlayProperty, value);
            }
        }
        #endregion

        #region Property IsPlaying

        /// <summary>
        /// Bindable Property IsPlaying
        /// </summary>
        public static readonly BindableProperty IsPlayingProperty = BindableProperty.Create(
          "IsPlaying",
          typeof(bool),
          typeof(AtomVideoPlayer),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          // (sender,oldValue,newValue) => {}
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          // () => Default(T)
          null
        );

        /*
        /// <summary>
        /// On IsPlaying changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsPlayingChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property IsPlaying
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                return (bool)GetValue(IsPlayingProperty);
            }
            set
            {
                SetValue(IsPlayingProperty, value);
            }
        }
        #endregion
        
    }

    public class AtomVideoSource {

        public string AlbumID { get; set; }

        public string FilePath { get; set; }

        public string Url { get; set; }

    }
}
