using System;
using System.Linq;
using System.Collections.Generic;
using NeuroSpeech.UIAtoms.DI;
using NeuroSpeech.UIAtoms.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    public class AtomZoomView : ContentView
    {
        private double startScale, currentScale, xOffset, yOffset, startX, startY;



        #region Property Translation

        /// <summary>
        /// Bindable Property Translation
        /// </summary>
        private static readonly BindablePropertyKey TranslationPropertyKey = BindableProperty.CreateReadOnly(
          nameof(Translation),
          typeof(Point),
          typeof(AtomZoomView),
          Point.Zero,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomZoomView)sender).OnTranslationChanged(oldValue,newValue),
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

        public static readonly BindableProperty TranslationProperty = TranslationPropertyKey.BindableProperty;

        /*
        /// <summary>
        /// On Translation changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTranslationChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Translation
        /// </summary>
        public Point Translation
        {
            get
            {
                return (Point)GetValue(TranslationProperty);
            }
            private set
            {
                SetValue(TranslationPropertyKey, value);
            }
        }
        #endregion

        #region Property CropPadding

        /// <summary>
        /// Bindable Property CropPadding
        /// </summary>
        public static readonly BindableProperty CropPaddingProperty = BindableProperty.Create(
          nameof(CropPadding),
          typeof(Thickness),
          typeof(AtomZoomView),
          new Thickness(0),
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomZoomView)sender).OnCropPaddingChanged(oldValue,newValue),
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
        /// On CropPadding changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCropPaddingChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CropPadding
        /// </summary>
        public Thickness CropPadding
        {
            get
            {
                return (Thickness)GetValue(CropPaddingProperty);
            }
            set
            {
                SetValue(CropPaddingProperty, value);
            }
        }
        #endregion

        #region Property MinimumScale

        /// <summary>
        /// Bindable Property MinimumScale
        /// </summary>
        public static readonly BindableProperty MinimumScaleProperty = BindableProperty.Create(
          nameof(MinimumScale),
          typeof(double),
          typeof(AtomZoomView),
          (double)1.0,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomZoomView)sender).OnMinimumScaleChanged(oldValue,newValue),
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
        /// On MinimumScale changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnMinimumScaleChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property MinimumScale
        /// </summary>
        public double MinimumScale
        {
            get
            {
                return (double)GetValue(MinimumScaleProperty);
            }
            set
            {
                SetValue(MinimumScaleProperty, value);
            }
        }
        #endregion

        public AtomZoomView()
        {

            this.BackgroundColor = Color.White;

            if (Device.OS != TargetPlatform.Android)
            {
                var pinchGesture = new PinchGestureRecognizer();
                pinchGesture.PinchUpdated += OnPinchUpdated;
                GestureRecognizers.Add(pinchGesture);

                var panGesture = new PanGestureRecognizer();
                panGesture.PanUpdated += OnPanUpdated;
                GestureRecognizers.Add(panGesture);
            }
        }

        public void Refresh() {
            InvalidateLayout();
        }

        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (!IsEnabled)
                return;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    startX = e.TotalX;
                    startY = e.TotalY;
                    Content.AnchorX = 0;
                    Content.AnchorY = 0;

                    break;

                case GestureStatus.Running:

                    var cp = CropPadding;
                    //Debug.WriteLine($"{cp.Left},{cp.Top},{cp.Right},{cp.Bottom}");

                    var maxTranslationX = Content.Scale * (Content.Width - cp.Right) - Content.Width;
                    Content.TranslationX = Math.Min(-Content.Scale * cp.Left, Math.Max(-maxTranslationX, xOffset + e.TotalX - startX));

                    var maxTranslationY = Content.Scale * (Content.Height - cp.Bottom) - Content.Height;
                    Content.TranslationY = Math.Min(-Content.Scale * cp.Top, Math.Max(-maxTranslationY, yOffset + e.TotalY - startY));

                    break;

                case GestureStatus.Completed:
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;

                    Translation = new Point(xOffset, yOffset);

                    break;
            }
        }

        public void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (!IsEnabled)
                return;

            switch (e.Status)
            {
                case GestureStatus.Started:
                    // Store the current scale factor applied to the wrapped user interface element,
                    // and zero the components for the center point of the translate transform.
                    startScale = Content.Scale;
                    Content.AnchorX = 0;
                    Content.AnchorY = 0;

                    break;

                case GestureStatus.Running:
                    // Calculate the scale factor to be applied.
                    currentScale += (e.Scale - 1) * startScale;
                    currentScale = Math.Max(MinimumScale, currentScale);

                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                    // so get the X pixel coordinate.
                    double renderedX = Content.X + xOffset;
                    double deltaX = renderedX / Width;
                    double deltaWidth = Width / (Content.Width * startScale);
                    double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                    // so get the Y pixel coordinate.
                    double renderedY = Content.Y + yOffset;
                    double deltaY = renderedY / Height;
                    double deltaHeight = Height / (Content.Height * startScale);
                    double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                    // Calculate the transformed element pixel coordinates.
                    double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                    double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                    // Apply translation based on the change in origin.
                    Content.TranslationX = Math.Min(0, Math.Max(targetX, -Content.Width * (currentScale - 1)));
                    Content.TranslationY = Math.Min(0, Math.Max(targetY, -Content.Height * (currentScale - 1)));

                    // Apply scale factor.
                    Content.Scale = currentScale;

                    break;

                case GestureStatus.Completed:
                    // Store the translation delta's of the wrapped user interface element.
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;
                    Translation = new Point(xOffset, yOffset);

                    break;
            }
        }
    }

}