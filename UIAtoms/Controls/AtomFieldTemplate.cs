using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NeuroSpeech.UIAtoms.Controls
{


    /// <summary>
    /// 
    /// </summary>
    public class AtomContentGrid : Grid {

        #region Property ContentRow

        /// <summary>
        /// Bindable Property ContentRow
        /// </summary>
        public static readonly BindableProperty ContentRowProperty = BindableProperty.Create(
          nameof(ContentRow),
          typeof(int),
          typeof(AtomContentGrid),
          0,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomContentGrid)sender).OnContentRowChanged(oldValue,newValue),
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
        /// On ContentRow changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnContentRowChanged(object oldValue, object newValue)
        {
            var c = Content;
            if (c != null) {
                SetRow(c, (int)newValue);
            }   
        }


        /// <summary>
        /// Property ContentRow
        /// </summary>
        public int ContentRow
        {
            get
            {
                return (int)GetValue(ContentRowProperty);
            }
            set
            {
                SetValue(ContentRowProperty, value);
            }
        }
        #endregion

        #region Property ContentColumn

        /// <summary>
        /// Bindable Property ContentColumn
        /// </summary>
        public static readonly BindableProperty ContentColumnProperty = BindableProperty.Create(
          nameof(ContentColumn),
          typeof(int),
          typeof(AtomContentGrid),
          0,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomContentGrid)sender).OnContentColumnChanged(oldValue,newValue),
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
        /// On ContentColumn changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnContentColumnChanged(object oldValue, object newValue)
        {
            var c = Content;
            if (c != null) {
                SetColumn(c, (int)newValue);
            }   
        }


        /// <summary>
        /// Property ContentColumn
        /// </summary>
        public int ContentColumn
        {
            get
            {
                return (int)GetValue(ContentColumnProperty);
            }
            set
            {
                SetValue(ContentColumnProperty, value);
            }
        }
        #endregion

        #region Property ContentRowSpan

        /// <summary>
        /// Bindable Property ContentRowSpan
        /// </summary>
        public static readonly BindableProperty ContentRowSpanProperty = BindableProperty.Create(
          nameof(ContentRowSpan),
          typeof(int),
          typeof(AtomContentGrid),
          1,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomContentGrid)sender).OnContentRowSpanChanged(oldValue,newValue),
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
        /// On ContentRowSpan changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnContentRowSpanChanged(object oldValue, object newValue)
        {
            var c = Content;
            if (c != null) {
                SetRowSpan(c, (int)newValue);
            }   
        }


        /// <summary>
        /// Property ContentRowSpan
        /// </summary>
        public int ContentRowSpan
        {
            get
            {
                return (int)GetValue(ContentRowSpanProperty);
            }
            set
            {
                SetValue(ContentRowSpanProperty, value);
            }
        }
        #endregion

        #region Property ContentColumnSpan

        /// <summary>
        /// Bindable Property ContentColumnSpan
        /// </summary>
        public static readonly BindableProperty ContentColumnSpanProperty = BindableProperty.Create(
          nameof(ContentColumnSpan),
          typeof(int),
          typeof(AtomContentGrid),
          1,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomContentGrid)sender).OnContentColumnSpanChanged(oldValue,newValue),
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
        /// On ContentColumnSpan changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnContentColumnSpanChanged(object oldValue, object newValue)
        {
            var c = Content;
            if (c != null) {
                SetColumnSpan(c, (int)newValue);
            }
        }


        /// <summary>
        /// Property ContentColumnSpan
        /// </summary>
        public int ContentColumnSpan
        {
            get
            {
                return (int)GetValue(ContentColumnSpanProperty);
            }
            set
            {
                SetValue(ContentColumnSpanProperty, value);
            }
        }
        #endregion

        #region Property Content

        /// <summary>
        /// Bindable Property Content
        /// </summary>
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(
          nameof(Content),
          typeof(View),
          typeof(AtomContentGrid),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomContentGrid)sender).OnContentChanged(oldValue,newValue),
          //null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          (sender) => ((AtomContentGrid)sender).GetDefaultContent()
          
        );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual View GetDefaultContent() => null;

        
        /// <summary>
        /// On Content changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnContentChanged(object oldValue, object newValue)
        {
            View view = oldValue as View;
            if (view != null) {
                this.Children.Remove(view);
            }
            view = newValue as View;
            if (view != null) {
                SetColumn(view, ContentColumn);
                SetColumnSpan(view, ContentColumnSpan);
                SetRow(view, ContentRow);
                SetRowSpan(view, ContentRowSpan);
                this.Children.Add(view);
            }
        }


        /// <summary>
        /// Property Content
        /// </summary>
        public View Content
        {
            get
            {
                return (View)GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }
        #endregion



    }


    /// <summary>
    /// 
    /// </summary>
    public class AtomFieldGrid : AtomContentGrid {

        #region Property Label

        /// <summary>
        /// Bindable Property Label
        /// </summary>
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
          nameof(Label),
          typeof(string),
          typeof(AtomFieldGrid),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomFieldGrid)sender).OnLabelChanged(oldValue,newValue),
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
        /// On Label changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnLabelChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Label
        /// </summary>
        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }
        #endregion

        #region Property LabelColor

        /// <summary>
        /// Bindable Property LabelColor
        /// </summary>
        public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(
          nameof(LabelColor),
          typeof(Color),
          typeof(AtomFieldGrid),
          Color.Default,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomFieldGrid)sender).OnLabelColorChanged(oldValue,newValue),
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
        /// On LabelColor changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnLabelColorChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property LabelColor
        /// </summary>
        public Color LabelColor
        {
            get
            {
                return (Color)GetValue(LabelColorProperty);
            }
            set
            {
                SetValue(LabelColorProperty, value);
            }
        }
        #endregion

        #region Property Error

        /// <summary>
        /// Bindable Property Error
        /// </summary>
        public static readonly BindableProperty ErrorProperty = BindableProperty.Create(
          nameof(Error),
          typeof(string),
          typeof(AtomFieldGrid),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomFieldGrid)sender).OnErrorChanged(oldValue,newValue),
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
        /// On Error changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnErrorChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Error
        /// </summary>
        public string Error
        {
            get
            {
                return (string)GetValue(ErrorProperty);
            }
            set
            {
                SetValue(ErrorProperty, value);
            }
        }
        #endregion

        #region Property IsRequired

        /// <summary>
        /// Bindable Property IsRequired
        /// </summary>
        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(
          nameof(IsRequired),
          typeof(bool),
          typeof(AtomFieldGrid),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomFieldGrid)sender).OnIsRequiredChanged(oldValue,newValue),
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
        /// On IsRequired changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsRequiredChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property IsRequired
        /// </summary>
        public bool IsRequired
        {
            get
            {
                return (bool)GetValue(IsRequiredProperty);
            }
            set
            {
                SetValue(IsRequiredProperty, value);
            }
        }
        #endregion

        #region Property ErrorColor

        /// <summary>
        /// Bindable Property ErrorColor
        /// </summary>
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(
          nameof(ErrorColor),
          typeof(Color),
          typeof(AtomFieldGrid),
          Color.White,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomFieldGrid)sender).OnErrorColorChanged(oldValue,newValue),
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
        /// On ErrorColor changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnErrorColorChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ErrorColor
        /// </summary>
        public Color ErrorColor
        {
            get
            {
                return (Color)GetValue(ErrorColorProperty);
            }
            set
            {
                SetValue(ErrorColorProperty, value);
            }
        }
        #endregion

        #region Property ErrorBackgroundColor

        /// <summary>
        /// Bindable Property ErrorBackgroundColor
        /// </summary>
        public static readonly BindableProperty ErrorBackgroundColorProperty = BindableProperty.Create(
          nameof(ErrorBackgroundColor),
          typeof(Color),
          typeof(AtomFieldGrid),
          Color.Red,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomFieldGrid)sender).OnErrorBackgroundColorChanged(oldValue,newValue),
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
        /// On ErrorBackgroundColor changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnErrorBackgroundColorChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ErrorBackgroundColor
        /// </summary>
        public Color ErrorBackgroundColor
        {
            get
            {
                return (Color)GetValue(ErrorBackgroundColorProperty);
            }
            set
            {
                SetValue(ErrorBackgroundColorProperty, value);
            }
        }
        #endregion

        #region Property Description

        /// <summary>
        /// Bindable Property Description
        /// </summary>
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
          nameof(Description),
          typeof(object),
          typeof(AtomFieldGrid),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomFieldGrid)sender).OnDescriptionChanged(oldValue,newValue),
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
        /// On Description changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnDescriptionChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Description
        /// </summary>
        public object Description
        {
            get
            {
                return (object)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public AtomFieldGrid()
        {
            this.BindingContext = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public void BindView(View view) {

            this.Content = view;
            view.PropertyChanged += View_PropertyChanged;
            Label = AtomForm.GetLabel(view);
            this.LabelColor = AtomForm.GetLabelColor(view);
            this.Error = AtomForm.GetError(view);
            this.Description = AtomForm.GetDescription(view);
            this.IsRequired = AtomForm.GetIsRequired(view);
            this.UpdateCell();
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnbindView() {
            var view = Content;
            if (view != null)
            {
                view.PropertyChanged -= View_PropertyChanged;
                Content = null;
            }
        }

        private void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName) {
                case nameof(Label):
                    this.Label = AtomForm.GetLabel(Content);
                    UpdateCell();
                    break;
                case nameof(LabelColor):
                    this.LabelColor = AtomForm.GetLabelColor(Content);
                    UpdateCell();
                    break;
                case nameof(Error):
                    this.Error = AtomForm.GetError(Content);
                    UpdateCell();
                    break;
                case nameof(Description):
                    this.Description = AtomForm.GetDescription(Content);
                    UpdateCell();
                    break;
                case nameof(IsRequired):
                    this.IsRequired = AtomForm.GetIsRequired(Content);
                    UpdateCell();
                    break;
                case nameof(Content):
                    UpdateCell();
                    break;
            }
        }

        private void UpdateCell()
        {
            UIAtomsApplication.Instance.TriggerOnce(() =>
            {
                ViewCell cell = Content.GetParentOfType<ViewCell>();
                if (cell != null)
                {
                    this.InvalidateMeasure();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        cell.ForceUpdateSize();
                    });
                }
            });

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AtomFieldTemplate: AtomFieldGrid
    {       
          
        /// <summary>
        /// 
        /// </summary>
        public AtomFieldTemplate()
        {
            //this.Padding = new Thickness(5);

            this.ContentColumnSpan = 3;
            this.ContentRow = 1;

            this.ColumnSpacing = 5;
            this.RowSpacing = 5;

            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition {  });

            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition {  });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var label = new Label();
            label.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding("Label"));
            label.SetBinding(Xamarin.Forms.Label.TextColorProperty, new Binding("LabelColor"));
            label.SetBinding(Xamarin.Forms.Label.IsVisibleProperty, new Binding("Label", converter: StringToVisibilityConverter.Instance));
            label.FontSize = 11;
            

            var required = new Label();
            required.Text = "*";
            required.TextColor = Color.Red;
            required.FontSize = 11;
            required.SetBinding(Xamarin.Forms.Label.IsVisibleProperty, new Binding("IsRequired"));

            Children.Add(label);
            Children.Add(required);

            SetColumn(required, 1);

            //this.AddRowItems(GridLength.Auto, label,required);

            //cp = new ContentPresenter();
//            cp.PropertyChanging += (s, e) => {
//                if (e.PropertyName == ContentPresenter.ContentProperty.PropertyName) {
//                    if (cp.Content != null) {
//                        cp.Content.PropertyChanged -= Content_PropertyChanged;
//                    }
//                }
//            };
//            cp.PropertyChanged += (s, e) => {
//                if (e.PropertyName == ContentPresenter.ContentProperty.PropertyName)
//                {
//                    if (cp.Content != null)
//                    {
//                        cp.HeightRequest = cp.Content.HeightRequest;
//                        cp.Content.PropertyChanged -= Content_PropertyChanged;
//                    }
//                }
//            };
            //this.AddRowItem(cp);

            

            //this.SetBinding(IsVisibleProperty, new Binding("Content.IsVisible") { Source = cp });

            //Device.BeginInvokeOnMainThread( async () => {
            //    while (cp.Content == null)
            //    {
            //        await Task.Delay(5);
            //        if (cp.Content != null)
            //        {
            //            var c = cp.Content;
            //            var h = c.Height;
            //            if (c.HeightRequest > h) {
            //                h = c.HeightRequest;
            //            }
            //            cp.HeightRequest = h;
            //            cp.MinimumHeightRequest = c.MinimumHeightRequest;
            //            this.InvalidateMeasure();
            //            this.InvalidateLayout();
            //            break;
            //        }
            //    }
            //});

            var error = new Label();
            error.SetBinding(Xamarin.Forms.Label.TextProperty,new Binding("Error"));
            error.SetBinding(Xamarin.Forms.Label.TextColorProperty, new Binding("ErrorColor"));
            error.SetBinding(Xamarin.Forms.Label.IsVisibleProperty, new Binding("Error", converter: StringToVisibilityConverter.Instance));
            error.SetBinding(Xamarin.Forms.Label.BackgroundColorProperty, new Binding("ErrorBackgroundColor"));
            error.HorizontalOptions = LayoutOptions.Fill;
            error.HorizontalTextAlignment = TextAlignment.Start;
            /*var errorFrame = new Frame();
            errorFrame.Content = error;
            errorFrame.SetBinding(Frame.PaddingProperty, new TemplateBinding("ErrorPadding"));
            errorFrame.SetBinding(Frame.BackgroundColorProperty, new TemplateBinding("ErrorBackgroundColor"));
            errorFrame.SetBinding(Frame.IsVisibleProperty, new TemplateBinding("Error", converter: StringToVisibilityConverter.Instance));*/
            //this.AddRowItem(error, GridLength.Auto);

            SetColumnSpan(error, 3);
            SetRow(error, 2);
            Children.Add(error);

            //var warning = new Label();
            //warning.SetBinding(Label.TextProperty,new TemplateBinding("Warning"));
            //var warningFrame = new Frame();
            //warningFrame.Padding = new Thickness(5);
            //warningFrame.Content = warning;
            //warningFrame.BackgroundColor = Color.Yellow;
            //warningFrame.SetBinding(Frame.IsVisibleProperty, new TemplateBinding("Warning", converter: StringToVisibilityConverter.Instance));
            //this.AddRowItem(warningFrame, GridLength.Auto);

            var description = new Label();
            description.SetBinding(Xamarin.Forms.Label.FormattedTextProperty,new Binding("Description"));
            description.SetBinding(Xamarin.Forms.Label.IsVisibleProperty, new Binding("Description", converter: StringToVisibilityConverter.Instance));
            //this.AddRowItem(description, GridLength.Auto);

            SetColumnSpan(description, 3);
            SetRow(description, 3);
            Children.Add(description);        

        }


        /*bool invalidCalled = false;
		protected override void OnChildMeasureInvalidated ()
		{
			if (invalidCalled)
				return;
			try{
			invalidCalled = true;
			base.OnChildMeasureInvalidated ();
			this.InvalidateMeasure ();
			System.Diagnostics.Debug.WriteLine ("Child Measure Invalidated");
			}finally{
				invalidCalled = false;
			}
		}*/

        //		private void Content_PropertyChanged(object sender, PropertyChangedEventArgs e){
        //			if (cp.Content != null) {
        //				cp.HeightRequest = cp.Content.HeightRequest;
        //				cp.MinimumHeightRequest = cp.Content.MinimumHeightRequest;
        //				cp.ForceLayout ();
        //			}
        //			this.ForceLayout ();
        //		}
    }

}