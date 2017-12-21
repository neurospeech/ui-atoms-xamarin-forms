using NeuroSpeech.UIAtoms.DI;
using NeuroSpeech.UIAtoms.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace NeuroSpeech.UIAtoms.Controls
{


    /// <summary>
    /// Image Cropper
    /// </summary>
    public class AtomImageCropper : Grid
    {


        AtomImage croppedImage = new AtomImage();
        //AtomImage outerImage = new AtomImage();
        AtomMaskBox maskView = new AtomMaskBox();
        AtomZoomView zoomView = new AtomZoomView();
        Grid cropContainer = new Grid();




        public ICommand CropCommand { get; }

        public ICommand UndoCommand { get; }

        public ICommand RotateLeft { get; }

        public ICommand RotateRight { get; }

        //ScrollView scrollView = new ScrollView();

        public AtomImageCropper()
        {

            

            //this.Padding = new Thickness(10);

            //Children.Add(zoomView);

            //croppedImage.Margin = new Thickness(320, 320, 320, 320);
            //zoomView.Padding = new Thickness(320, 320, 320, 320);



            maskView.SetBinding(AtomMaskBox.BackgroundColorProperty, new Binding {
                Path = nameof(CropMarginColor),
                Source = this
            });

            zoomView.Content = croppedImage;
            zoomView.SetBinding(AtomZoomView.IsEnabledProperty, new Binding() {
                Path = nameof(CanUndo),
                Source = this,
                Converter = NegateBooleanConverter.Instance
            });


            //cropContainer.Content = zoomView;
            cropContainer.Children.Add(zoomView);
            cropContainer.HorizontalOptions = LayoutOptions.Center;
            cropContainer.VerticalOptions = LayoutOptions.Center;
            //cropContainer.BackgroundColor = Color.Red;
            cropContainer.SetBinding(Grid.BackgroundColorProperty, new Binding {
                Path = nameof(CropErrorColor),
                Source = this
            });
            
            Children.Add(cropContainer);

            Children.Add(maskView);



            //this.Margin = new Thickness(50, 50, 50, 50);
            //this.IsClippedToBounds = true;

            croppedImage.PropertyChanged += OnImagePropertyChanged;
            cropContainer.PropertyChanged += OnCropContainerPropertyChanged;
            zoomView.PropertyChanged += OnZoomViewPropertyChanged;


            //ResizeCropContainer();

            CropCommand = new AtomCommand(async () =>
            {
                await OnCropCommandAsync();
            });

            RotateLeft = new AtomCommand(async () =>
            {
                await OnRotateCommandAsync("Left");
            });

            

            RotateRight = new AtomCommand(async () =>
            {
                await OnRotateCommandAsync("Right");
            });

            UndoCommand = new AtomCommand(() =>
            {

                // try to delete...
                System.IO.File.Delete(CroppedFile);
                CroppedFile = null;
                Source = originalSource;
                originalSource = null;
                CanUndo = false;
                return Task.CompletedTask;
            });
        }

        private void OnZoomViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName) {
                case nameof(AtomZoomView.Translation):
                    UpdateCropSize();
                    break;
            }
        }

        private async Task OnCropCommandAsync()
        {
            try
            {
                var imageProvider = DependencyService.Get<AtomImageProvider>();

                string path = await imageProvider.CropAsync(Source, CropRect);

                CanUndo = !string.IsNullOrWhiteSpace(path);

                CroppedFile = path;
                originalSource = Source;
                Source = path;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail("Cropping Failed", ex.ToString());
                await DependencyService.Get<INotificationService>().NotifyAsync("Cropping Failed");
            }

        }

        private async Task OnRotateCommandAsync(string side)
        {
            try
            {
                int angle = 0;
                if (side.Equals("Left"))
                    angle = Convert.ToInt32(croppedImage.Rotation + 270);
                else
                    angle = Convert.ToInt32(croppedImage.Rotation + 90);


                var imageProvider = DependencyService.Get<AtomImageProvider>();

                string path = await imageProvider.RotateAsync(Source, angle, side);
                
                Source = path;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail("Rotate Failed", ex.ToString());
                await DependencyService.Get<INotificationService>().NotifyAsync("Rotation Failed");
            }

        }

        private string originalSource = null;

        private void OnCropContainerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnImagePropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(X):
                case nameof(Y):
                case nameof(Width):
                case nameof(Height):
                    // update mask...
                    maskView.MaskRect = new Rectangle(cropContainer.X, cropContainer.Y, cropContainer.Width, cropContainer.Height);
                    break;
            }

        }

        private void OnImagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(AtomImage.ImageSize):
                    Debug.WriteLine($"Cropping Tool: ImageSize modified...");
                    UIAtomsApplication.Instance.SetTimeout(UpdateImageSize, TimeSpan.FromSeconds(1));
                    break;
                //case nameof(AtomImage.TranslationX):
                //case nameof(AtomImage.TranslationY):
                case nameof(AtomImage.Scale):
                case nameof(AtomImage.Width):
                case nameof(AtomImage.Height):
                //case nameof(AtomImage.Rotation):

                    UpdateCropSize();
                    break;
                default:
                    break;
            }
        }

        private void UpdateImageSize()
        {

            double width = croppedImage.Width;
            double height = croppedImage.Height;

            var isize = croppedImage.ImageSize;
            double iwidth = isize.Width;
            double iheight = isize.Height;


            if (iheight <= 0)
            {
                return;
            }

            if (height <= 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(500);
                    UpdateImageSize();
                });
            }


            if (Scaled)
            {
                //zoomView.MinimumScale = scale;
                //Debug.WriteLine($"Exit New Scale: {scale}, diff: {diff}");
                return;
            }
            Scaled = true;

            this.IsClippedToBounds = true;



            var ciAspect = width / height;

            var imageAspect = iwidth / iheight;

            double scale = 1.0;

            double pad = 0;

            if (ciAspect > imageAspect)
            {
                scale = ciAspect / imageAspect;// width / (imageAspect * height) ;// ciAspect/imageAspect ;// width /(imageAspect/height);

                pad = (width - imageAspect * height)/2;

                zoomView.CropPadding = new Thickness(pad, 0, pad, 0);
                // set top/bottom padding...

            }
            else {
                scale = height / (width / imageAspect);// height*imageAspect/width ;// height/(width/imageAspect);

                pad = (height - width / imageAspect) / 2;

                // set left/right padding...
                zoomView.CropPadding = new Thickness(0, pad, 0, pad);
            }

            var diff = Math.Abs(zoomView.Content.Scale - scale);

            //Debug.WriteLine($"New Scale: {scale}, diff: {diff}");

            croppedImage.AnchorX = 0.5;
            croppedImage.AnchorX = 0.5;
            croppedImage.TranslationX = 0;
            croppedImage.TranslationY = 0;
            croppedImage.Scale = scale;

            //croppedImage.AnchorX = 0;
            //croppedImage.AnchorX = 0;

            zoomView.MinimumScale = scale;

            zoomView.OnPanUpdated(zoomView, new PanUpdatedEventArgs(GestureStatus.Started, 0, 0, 0));
            zoomView.OnPanUpdated(zoomView, new PanUpdatedEventArgs(GestureStatus.Running, 0, 0, 0));
            zoomView.OnPanUpdated(zoomView, new PanUpdatedEventArgs(GestureStatus.Completed, 0, 0, 0));

            UpdateCropSize();

            zoomView.Refresh();

        }

        private void UpdateCropSize()
        {

            if (CroppedFile!=null)
                return;

            double width = croppedImage.Width;
            double height = croppedImage.Height;


            var isize = croppedImage.ImageSize;
            double iwidth = isize.Width;
            double iheight = isize.Height;

            if (height == 0 || iheight == 0)
                return;

            double constraintAspect = croppedImage.Width / croppedImage.Height;
            double imageAspect = iwidth / iheight;


            double iw = 0;
            double ih = 0;

            double iscale = 1;
            if (constraintAspect > imageAspect)
            {
                iscale = height  * croppedImage.Scale / iheight;
                iw = imageAspect * height;
                ih = height;
            }
            else
            {
                iscale = width * croppedImage.Scale / iwidth ;
                ih = width/imageAspect;
                iw = width;
            }

            // multiple scale further...
            double scale = iscale;// * croppedImage.Scale;

            Rectangle src = new Rectangle(0, 0, iwidth, iheight);

            double cx = (width-iw)/2;// ex;
            double cy = (height-ih)/2;// ey;

            var t = zoomView.Translation;

            double px = cropContainer.X;
            double py = cropContainer.Y;

            var maxc = Math.Max(cx, cy);

            double x = (-t.X - cx*croppedImage.Scale) ;
            double y = (-t.Y - cy*croppedImage.Scale);


            Rectangle dest = new Rectangle(
                x/scale, 
                y/scale, 
                Math.Min(cropContainer.Width - 0,cropContainer.Width) / scale, 
                Math.Min(cropContainer.Height - 0,cropContainer.Height) / scale);

            var cis = new CropRect { Source = src, Destination = dest };
            CropRect = cis;

            //Debug.WriteLine($"iw:{iw}, ih:{ih} scale:{croppedImage.Scale}, imageAspect:{imageAspect}, containerAspect:{constraintAspect} width:{width}, height:{height}");
            //Debug.WriteLine($"Image: {croppedImage.X},{croppedImage.Y} {croppedImage.Width},{croppedImage.Height}");
            //Debug.WriteLine($"Container: {zoomView.X},{zoomView.Y} {zoomView.Width},{zoomView.Height}");
            //Debug.WriteLine($"Cropping Tool: Source: {cis.Source} Dest: {cis.Destination}");

        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            ResizeCropContainer();
        }

        private void ResizeCropContainer()
        {

            
            if (double.IsNaN(this.Width)
                || double.IsInfinity(this.Width)
                || this.Width <= 0)
                return;
            if (double.IsNaN(this.Height)
                || double.IsInfinity(this.Height)
                || this.Height <= 0)
                return;

            var paddingWidth = 2 * CropPadding;
            var paddingHeight = paddingWidth;

            var imageAspect = this.CropAspectRatio;

            var containerAspect = Width / Height;

            // find out smaller...
            if (containerAspect > imageAspect)
            {
                var h = this.Height - paddingHeight;
                var w = imageAspect * h - paddingWidth;
                cropContainer.WidthRequest = w;
                cropContainer.HeightRequest = w / imageAspect;
            }
            else
            {
                var w = this.Width - paddingWidth;
                var h = w / imageAspect - paddingHeight;
                cropContainer.HeightRequest = h;
                cropContainer.WidthRequest = h * imageAspect;
            }

            Device.BeginInvokeOnMainThread(async () => {
                await Task.Delay(500);
                UpdateImageSize();
            });
            
        }


        #region Property Source

        /// <summary>
        /// Bindable Property Source
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
          nameof(Source),
          typeof(string),
          typeof(AtomImageCropper),
          null,
          BindingMode.TwoWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender, oldValue, newValue) => ((AtomImageCropper)sender).OnSourceChanged(oldValue, newValue),
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
            this.Scaled = false;
            string src = (string)newValue;
            if (CroppedFile != src)
            {
                CroppedFile = null;
            }
            //if (croppedImage.Source != null)
            //{
               
            //}
            RecreateImage();
            croppedImage.Source = src;
            this.Scaled = false;
        }

        /// <summary>
        /// Since Xamarin does not remove default transformations, we need to simply remove and create new AtomZoomView
        /// with new AtomImage
        /// </summary>
        private void RecreateImage()
        {
            this.IsClippedToBounds = false;
            croppedImage.PropertyChanged -= OnImagePropertyChanged;
            zoomView.PropertyChanged -= OnZoomViewPropertyChanged;
            cropContainer.Children.Remove(zoomView);


            //Debug.WriteLine($"zoomView is reset correctly");

            zoomView = new AtomZoomView();
            croppedImage = new AtomImage();
            croppedImage.PropertyChanged += OnImagePropertyChanged;
            zoomView.PropertyChanged += OnZoomViewPropertyChanged;
            zoomView.Content = croppedImage;
            cropContainer.Children.Add(zoomView);

            zoomView.SetBinding(AtomZoomView.IsEnabledProperty, new Binding() {
                Path = nameof(CanUndo),
                Source = this,
                Converter = NegateBooleanConverter.Instance
            });


        }


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

        #region Property CropRect

        /// <summary>
        /// Bindable Property CropRect
        /// </summary>
        private static readonly BindablePropertyKey CropRectPropertyKey = BindableProperty.CreateReadOnly(
          nameof(CropRect),
          typeof(CropRect),
          typeof(AtomImageCropper),
          CropRect.Zero,
          BindingMode.OneWayToSource,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImageCropper)sender).OnCropRectChanged(oldValue,newValue),
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

        public static readonly BindableProperty CropRectProperty = CropRectPropertyKey.BindableProperty;

        /*
        /// <summary>
        /// On CropRect changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCropRectChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CropRect
        /// </summary>
        public CropRect CropRect
        {
            get
            {
                return (CropRect)GetValue(CropRectProperty);
            }
            private set
            {
                SetValue(CropRectPropertyKey, value);
            }
        }
        #endregion

        #region Property CropPadding

        /// <summary>
        /// Bindable Property CropPadding
        /// </summary>
        public static readonly BindableProperty CropPaddingProperty = BindableProperty.Create(
          nameof(CropPadding),
          typeof(double),
          typeof(AtomImageCropper),
          (double)20,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender, oldValue, newValue) => ((AtomImageCropper)sender).OnCropPaddingChanged(oldValue, newValue),
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
        /// On CropPadding changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCropPaddingChanged(object oldValue, object newValue)
        {
            ResizeCropContainer();
        }


        /// <summary>
        /// Property CropPadding
        /// </summary>
        public double CropPadding
        {
            get
            {
                return (double)GetValue(CropPaddingProperty);
            }
            set
            {
                SetValue(CropPaddingProperty, value);
            }
        }
        #endregion

        #region Property CropAspectRatio

        /// <summary>
        /// Bindable Property CropAspectRatio
        /// </summary>
        public static readonly BindableProperty CropAspectRatioProperty = BindableProperty.Create(
          nameof(CropAspectRatio),
          typeof(double),
          typeof(AtomImageCropper),
          (double)600.0 / (double)400.0,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender, oldValue, newValue) => ((AtomImageCropper)sender).OnCropAspectRatioChanged(oldValue, newValue),
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
        /// On CropAspectRatio changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCropAspectRatioChanged(object oldValue, object newValue)
        {
            ResizeCropContainer();
            RecreateImage();
            croppedImage.Source = Source;
            this.Scaled = false;
        }



        /// <summary>
        /// Property CropAspectRatio
        /// </summary>
        public double CropAspectRatio
        {
            get
            {
                return (double)GetValue(CropAspectRatioProperty);
            }
            set
            {
                SetValue(CropAspectRatioProperty, value);
            }
        }
        #endregion

        #region Property CroppedFile

        /// <summary>
        /// Bindable Property CroppedFile
        /// </summary>
        public static readonly BindablePropertyKey CroppedFilePropertyKey = BindableProperty.CreateReadOnly(
          nameof(CroppedFile),
          typeof(string),
          typeof(AtomImageCropper),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImageCropper)sender).OnCroppedFileChanged(oldValue,newValue),
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

        private static readonly BindableProperty CroppedFileProperty = CroppedFilePropertyKey.BindableProperty;

        /*
        /// <summary>
        /// On CroppedFile changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCroppedFileChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CroppedFile
        /// </summary>
        public string CroppedFile
        {
            get
            {
                return (string)GetValue(CroppedFileProperty);
            }
            private set
            {
                SetValue(CroppedFilePropertyKey, value);
            }
        }
        #endregion

        #region Property CanUndo

        /// <summary>
        /// Bindable Property CanUndo
        /// </summary>
        private static readonly BindablePropertyKey CanUndoPropertyKey = BindableProperty.CreateReadOnly(
          nameof(CanUndo),
          typeof(bool),
          typeof(AtomImageCropper),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImageCropper)sender).OnCanUndoChanged(oldValue,newValue),
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

        public static readonly BindableProperty CanUndoProperty = CanUndoPropertyKey.BindableProperty;

        /*
        /// <summary>
        /// On CanUndo changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCanUndoChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CanUndo
        /// </summary>
        public bool CanUndo
        {
            get
            {
                return (bool)GetValue(CanUndoProperty);
            }
            private set
            {
                SetValue(CanUndoPropertyKey, value);
            }
        }
        #endregion



        #region Property CropMarginColor

        /// <summary>
        /// Bindable Property CropMarginColor
        /// </summary>
        public static readonly BindableProperty CropMarginColorProperty = BindableProperty.Create(
          nameof(CropMarginColor),
          typeof(Color),
          typeof(AtomImageCropper),
          Color.FromRgba(0,0,0,0.5),
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImageCropper)sender).OnCropMarginColorChanged(oldValue,newValue),
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
        /// On CropMarginColor changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCropMarginColorChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CropMarginColor
        /// </summary>
        public Color CropMarginColor
        {
            get
            {
                return (Color)GetValue(CropMarginColorProperty);
            }
            set
            {
                SetValue(CropMarginColorProperty, value);
            }
        }
        #endregion



        #region Property CropErrorColor

        /// <summary>
        /// Bindable Property CropErrorColor
        /// </summary>
        public static readonly BindableProperty CropErrorColorProperty = BindableProperty.Create(
          nameof(CropErrorColor),
          typeof(Color),
          typeof(AtomImageCropper),
          Color.Red,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomImageCropper)sender).OnCropErrorColorChanged(oldValue,newValue),
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
        /// On CropErrorColor changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCropErrorColorChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CropErrorColor
        /// </summary>
        public Color CropErrorColor
        {
            get
            {
                return (Color)GetValue(CropErrorColorProperty);
            }
            set
            {
                SetValue(CropErrorColorProperty, value);
            }
        }

        public bool Scaled { get; private set; }
        #endregion







    }

    //public class AtomCropBorderView : AtomFrame {
    //    public AtomCropBorderView()
    //    {
    //        this.OutlineColor = Color.FromRgba(0, 0, 0, 1.0);
    //    }
    //}

    public struct CropRect
    {
        public Rectangle Source;
        public Rectangle Destination;

        public override string ToString()
        {
            return $"S:{Source.Left},{Source.Top},{Source.Width},{Source.Height}, D:{Destination.Left},{Destination.Top},{Destination.Width},{Destination.Height}";
        }

        public static CropRect Zero = new CropRect();
    }


}
    
