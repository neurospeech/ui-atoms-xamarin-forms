using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomProgressPopupPage : AtomPopupPage
    {

        /// <summary>
        /// 
        /// </summary>
        public AtomProgressPopupPage()
        {
            this.ControlTemplate = new Xamarin.Forms.ControlTemplate(typeof(BlankControlTemplate));

            this.Content = new ProgressBar
            {
                IsEnabled = true,
                WidthRequest = 200,
                HeightRequest = 30
            };
            
            this.Content.SetBinding(ProgressBar.ProgressProperty, new Binding()
            {
                Source = this,
                Path = nameof(Progress)
            });

            this.CloseWhenBackgroundIsClicked = false;
        }



        #region Property Progress

        /// <summary>
        /// Bindable Property Progress
        /// </summary>
        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
          nameof(Progress),
          typeof(double),
          typeof(AtomProgressPopupPage),
          (double)0.0,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AppProgressDialog)sender).OnProgressChanged(oldValue,newValue),
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
        /// On Progress changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnProgressChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Progress
        /// </summary>
        public double Progress
        {
            get
            {
                return (double)GetValue(ProgressProperty);
            }
            set
            {
                SetValue(ProgressProperty, value);
            }
        }
        #endregion

        #region Property ProgressDescription

        /// <summary>
        /// Bindable Property ProgressDescription
        /// </summary>
        public static readonly BindableProperty ProgressDescriptionProperty = BindableProperty.Create(
          nameof(ProgressDescription),
          typeof(string),
          typeof(AtomProgressPopupPage),
          "",
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomProgressPopupPage)sender).OnProgressDescriptionChanged(oldValue,newValue),
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
        /// On ProgressDescription changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnProgressDescriptionChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ProgressDescription
        /// </summary>
        public string ProgressDescription
        {
            get
            {
                return (string)GetValue(ProgressDescriptionProperty);
            }
            set
            {
                SetValue(ProgressDescriptionProperty, value);
            }
        }
        #endregion




        /// <summary>
        /// 
        /// </summary>
        public class BlankControlTemplate : Grid
        {

            /// <summary>
            /// 
            /// </summary>
            public BlankControlTemplate()
            {

                Grid innerGrid = new Grid() {
                    Padding = new Thickness(10)
                };

                innerGrid.RowDefinitions = new RowDefinitionCollection();
                innerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                innerGrid.RowDefinitions.Add(new RowDefinition { });

                innerGrid.ColumnDefinitions = new ColumnDefinitionCollection();
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition { });
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                innerGrid.Effects.Add(new AtomRoundBorderEffect {
                    BackgroundColor = Color.White,
                    StrokeWidth = 0,
                    CornerRadius = 10
                });

                var title = new Label();

                title.SetBinding(Label.TextProperty, new TemplateBinding(nameof(AtomProgressPopupPage.Title)));

                innerGrid.Children.Add(title);

                var cp = new ContentPresenter { };


                var desc = new Label();

                desc.SetBinding(Label.TextProperty, new TemplateBinding(nameof(AtomProgressPopupPage.ProgressDescription)));

                Grid.SetColumn(desc, 1);

                innerGrid.Children.Add(desc);

                Grid.SetRow(cp, 1);
                Grid.SetColumnSpan(cp, 2);

                innerGrid.Children.Add(cp);

                innerGrid.VerticalOptions = LayoutOptions.Center;

                Children.Add(innerGrid);
            }

        }
    }
}
