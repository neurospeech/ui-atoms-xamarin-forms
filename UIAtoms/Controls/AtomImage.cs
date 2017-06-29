using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomImage : View
    {

        #region Property Source

        /// <summary>
        /// Bindable Property Source
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
          "Source",
          typeof(string),
          typeof(AtomImage),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImage)sender).OnSourceChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           (s) => ((AtomImage)s).GetDefaultSource()
        );

        protected virtual string GetDefaultSource() => null;

        /*
        /// <summary>
        /// On Source changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSourceChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Source
        /// </summary>
        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        #endregion

        #region Property OverlaySource

        /// <summary>
        /// Bindable Property OverlaySource
        /// </summary>
        public static readonly BindableProperty OverlaySourceProperty = BindableProperty.Create(
          "OverlaySource",
          typeof(string),
          typeof(AtomImage),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImage)sender).OnOverlaySourceChanged(oldValue,newValue),
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
        /// On OverlaySource changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnOverlaySourceChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property OverlaySource
        /// </summary>
        public string OverlaySource
        {
            get
            {
                return (string)GetValue(OverlaySourceProperty);
            }
            set
            {
                SetValue(OverlaySourceProperty, value);
            }
        }
        #endregion

        #region Property ShowProgress

        /// <summary>
        /// Bindable Property ShowProgress
        /// </summary>
        public static readonly BindableProperty ShowProgressProperty = BindableProperty.Create(
          nameof(ShowProgress),
          typeof(bool),
          typeof(AtomImage),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImage)sender).OnShowProgressChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           (s) => ((AtomImage)s).GetDefaultShowProgress()
        );

        protected virtual bool GetDefaultShowProgress() => true;

        /*
        /// <summary>
        /// On ShowProgress changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnShowProgressChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ShowProgress
        /// </summary>
        public bool ShowProgress
        {
            get
            {
                return (bool)GetValue(ShowProgressProperty);
            }
            set
            {
                SetValue(ShowProgressProperty, value);
            }
        }
        #endregion

        #region Property ImageSize

        /// <summary>
        /// Bindable Property ImageSize
        /// </summary>
        public static readonly BindableProperty ImageSizeProperty = BindableProperty.Create(
          nameof(ImageSize),
          typeof(Size),
          typeof(AtomImage),
          Size.Zero,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImage)sender).OnImageSizeChanged(oldValue,newValue),
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
        /// On ImageSize changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnImageSizeChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Actual Size of Image loaded from source
        /// </summary>
        public Size ImageSize
        {
            get
            {
                return (Size)GetValue(ImageSizeProperty);
            }
            set
            {
                SetValue(ImageSizeProperty, value);
            }
        }
        #endregion



        public AtomImage()
        {
            //this.MinimumWidthRequest = 50;
            //this.MinimumHeightRequest = 50;   
        }

        #region Property Aspect

        /// <summary>
        /// Bindable Property Aspect
        /// </summary>
        public static readonly BindableProperty AspectProperty = BindableProperty.Create(
          "Aspect",
          typeof(Aspect),
          typeof(AtomImage),
          Aspect.AspectFit,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImage)sender).OnAspectChanged(oldValue,newValue),
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
        /// On Aspect changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnAspectChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Aspect
        /// </summary>
        public Aspect Aspect
        {
            get
            {
                return (Aspect)GetValue(AspectProperty);
            }
            set
            {
                SetValue(AspectProperty, value);
            }
        }
        #endregion



        #region Property Padding

        /// <summary>
        /// Bindable Property Padding
        /// </summary>
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(
          nameof(Padding),
          typeof(Thickness),
          typeof(AtomImage),
          new Thickness(0),
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImage)sender).OnPaddingChanged(oldValue,newValue),
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
        /// On Padding changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnPaddingChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Padding
        /// </summary>
        public Thickness Padding
        {
            get
            {
                return (Thickness)GetValue(PaddingProperty);
            }
            set
            {
                SetValue(PaddingProperty, value);
            }
        }
        #endregion




        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            SizeRequest desiredSize = base.OnMeasure(double.PositiveInfinity, double.PositiveInfinity);

            double desiredAspect = desiredSize.Request.Width / desiredSize.Request.Height;
            double constraintAspect = widthConstraint / heightConstraint;

            double desiredWidth = desiredSize.Request.Width;
            double desiredHeight = desiredSize.Request.Height;

            if (desiredWidth == 0 || desiredHeight == 0)
                return new SizeRequest(new Size(0, 0));

            double width = desiredWidth;
            double height = desiredHeight;
            if (constraintAspect > desiredAspect)
            {
                // constraint area is proportionally wider than image
                switch (Aspect)
                {
                    case Aspect.AspectFit:
                    case Aspect.AspectFill:
                        height = Math.Min(desiredHeight, heightConstraint);
                        width = desiredWidth * (height / desiredHeight);
                        break;
                    case Aspect.Fill:
                        width = Math.Min(desiredWidth, widthConstraint);
                        height = desiredHeight * (width / desiredWidth);
                        break;
                }
            }
            else if (constraintAspect < desiredAspect)
            {
                // constraint area is proportionally taller than image
                switch (Aspect)
                {
                    case Aspect.AspectFit:
                    case Aspect.AspectFill:
                        width = Math.Min(desiredWidth, widthConstraint);
                        height = desiredHeight * (width / desiredWidth);
                        break;
                    case Aspect.Fill:
                        height = Math.Min(desiredHeight, heightConstraint);
                        width = desiredWidth * (height / desiredHeight);
                        break;
                }
            }
            else
            {
                // constraint area is same aspect as image
                width = Math.Min(desiredWidth, widthConstraint);
                height = desiredHeight * (width / desiredWidth);
            }

            return new SizeRequest(new Size(width, height));
        }

        //#region Property OverlayWidth

        ///// <summary>
        ///// Bindable Property OverlayWidth
        ///// </summary>
        //public static readonly BindableProperty OverlayWidthProperty = BindableProperty.Create(
        //  "OverlayWidth",
        //  typeof(double),
        //  typeof(AtomImage),
        //  (double)0,
        //  BindingMode.OneWay,
        //  // validate value delegate
        //  // (sender,value) => true
        //  null,
        //  // property changed, delegate
        //  //(sender,oldValue,newValue) => ((AtomImage)sender).OnOverlayWidthChanged(oldValue,newValue),
        //  null,
        //  // property changing delegate
        //  // (sender,oldValue,newValue) => {}
        //  null,
        //  // coerce value delegate 
        //  // (sender,value) => value
        //  null,
        //  // create default value delegate
        //  // () => Default(T)
        //  null
        //);

        ///*
        ///// <summary>
        ///// On OverlayWidth changed
        ///// </summary>
        ///// <param name="oldValue">Old Value</param>
        ///// <param name="newValue">New Value</param>
        //protected virtual void OnOverlayWidthChanged(object oldValue, object newValue)
        //{

        //}*/


        ///// <summary>
        ///// Property OverlayWidth
        ///// </summary>
        //public double OverlayWidth
        //{
        //    get
        //    {
        //        return (double)GetValue(OverlayWidthProperty);
        //    }
        //    set
        //    {
        //        SetValue(OverlayWidthProperty, value);
        //    }
        //}
        //#endregion

        //#region Property OverlayHeight

        ///// <summary>
        ///// Bindable Property OverlayHeight
        ///// </summary>
        //public static readonly BindableProperty OverlayHeightProperty = BindableProperty.Create(
        //  "OverlayHeight",
        //  typeof(double),
        //  typeof(AtomImage),
        //  (double)0,
        //  BindingMode.OneWay,
        //  // validate value delegate
        //  // (sender,value) => true
        //  null,
        //  // property changed, delegate
        //  //(sender,oldValue,newValue) => ((AtomImage)sender).OnOverlayHeightChanged(oldValue,newValue),
        //  null,
        //  // property changing delegate
        //  // (sender,oldValue,newValue) => {}
        //  null,
        //  // coerce value delegate 
        //  // (sender,value) => value
        //  null,
        //  // create default value delegate
        //  // () => Default(T)
        //  null
        //);

        ///*
        ///// <summary>
        ///// On OverlayHeight changed
        ///// </summary>
        ///// <param name="oldValue">Old Value</param>
        ///// <param name="newValue">New Value</param>
        //protected virtual void OnOverlayHeightChanged(object oldValue, object newValue)
        //{

        //}*/


        ///// <summary>
        ///// Property OverlayHeight
        ///// </summary>
        //public double OverlayHeight
        //{
        //    get
        //    {
        //        return (double)GetValue(OverlayHeightProperty);
        //    }
        //    set
        //    {
        //        SetValue(OverlayHeightProperty, value);
        //    }
        //}
        //#endregion



    }

}
