using NeuroSpeech.UIAtoms.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomNavigatorListView: AtomListView
    {

        protected override ControlTemplate GetDefaultItemStyleTemplate() 
            => new ControlTemplate(typeof(AtomNavigatorListViewItemStyle));

        protected override bool GetDefaultAllowMultipleSelection() 
            => true;

        protected override bool GetDefaultTapOnClick() => false;

        public AtomNavigatorListView()
        {
        }



    }

    public class AtomCheckBoxImage : AtomImage {


        protected override string GetDefaultSource() => AtomStockImages.EmptyCheckBoxImageUrl;

        protected override bool GetDefaultShowProgress() => false;

        #region Property CheckedSource

        /// <summary>
        /// Bindable Property CheckedSource
        /// </summary>
        public static readonly BindableProperty CheckedSourceProperty = BindableProperty.Create(
          nameof(CheckedSource),
          typeof(string),
          typeof(AtomCheckBoxImage),
          AtomStockImages.CheckBoxImageUrl,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomCheckBoxImage)sender).OnCheckedSourceChanged(oldValue,newValue),
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
        /// On CheckedSource changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCheckedSourceChanged(object oldValue, object newValue)
        {
            UpdateSource();   
        }


        /// <summary>
        /// Property CheckedSource
        /// </summary>
        public string CheckedSource
        {
            get
            {
                return (string)GetValue(CheckedSourceProperty);
            }
            set
            {
                SetValue(CheckedSourceProperty, value);
            }
        }
        #endregion

        #region Property UncheckedSource

        /// <summary>
        /// Bindable Property UncheckedSource
        /// </summary>
        public static readonly BindableProperty UncheckedSourceProperty = BindableProperty.Create(
          nameof(UncheckedSource),
          typeof(string),
          typeof(AtomCheckBoxImage),
          AtomStockImages.EmptyCheckBoxImageUrl,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomCheckBoxImage)sender).OnUncheckedSourceChanged(oldValue,newValue),
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
        /// On UncheckedSource changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnUncheckedSourceChanged(object oldValue, object newValue)
        {
            UpdateSource();   
        }


        /// <summary>
        /// Property UncheckedSource
        /// </summary>
        public string UncheckedSource
        {
            get
            {
                return (string)GetValue(UncheckedSourceProperty);
            }
            set
            {
                SetValue(UncheckedSourceProperty, value);
            }
        }
        #endregion





        #region Property IsChecked

        /// <summary>
        /// Bindable Property IsChecked
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
          nameof(IsChecked),
          typeof(bool),
          typeof(AtomCheckBoxImage),
          false,
          BindingMode.TwoWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomCheckBoxImage)sender).OnIsCheckedChanged(oldValue,newValue),
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
        /// On IsChecked changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsCheckedChanged(object oldValue, object newValue)
        {
            bool v = (bool)newValue;
            UpdateSource();
        }

        private void UpdateSource()
        {
            SetBinding(SourceProperty, new Binding()
            {
                Source = this,
                Path = IsChecked ? nameof(CheckedSource) : nameof(UncheckedSource)
            });
        }


        /// <summary>
        /// Property IsChecked
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }
        #endregion


        #region Property ToggleOnTouch

        /// <summary>
        /// Bindable Property ToggleOnTouch
        /// </summary>
        public static readonly BindableProperty ToggleOnTouchProperty = BindableProperty.Create(
          nameof(ToggleOnTouch),
          typeof(bool),
          typeof(AtomCheckBoxImage),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomCheckBoxImage)sender).OnToggleOnTouchChanged(oldValue,newValue),
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
        /// On ToggleOnTouch changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnToggleOnTouchChanged(object oldValue, object newValue)
        {
            bool b = (bool)newValue;
            if (b)
            {
                GestureRecognizers.Add(new TapGestureRecognizer {
                    Command = new AtomCommand(() => {
                        IsChecked = !IsChecked;
                        return Task.CompletedTask;
                    })
                });
            }
            else {
                GestureRecognizers.Clear();
            }
        }


        /// <summary>
        /// Property ToggleOnTouch
        /// </summary>
        public bool ToggleOnTouch
        {
            get
            {
                return (bool)GetValue(ToggleOnTouchProperty);
            }
            set
            {
                SetValue(ToggleOnTouchProperty, value);
            }
        }
        #endregion




        

    }

    public class AtomNavigatorListViewItemStyle : AtomListViewCell
    {

        public AtomNavigatorListViewItemStyle()
        {

            ColumnDefinitions = new ColumnDefinitionCollection();
            var iconColumn = new ColumnDefinition { Width = GridLength.Auto };
            ColumnDefinitions.Add(iconColumn);
            ColumnDefinitions.Add(new ColumnDefinition { });

            RowDefinitions = new RowDefinitionCollection();
            var iconHeight = new RowDefinition { Height = GridLength.Auto };
            RowDefinitions.Add(iconHeight);
            RowDefinitions.Add(new RowDefinition { });


            SetBinding(BackgroundColorProperty, new TemplateBinding(nameof(BackgroundColor)));

            //ContentView cv = new ContentView();
            //cv.SetBinding(ContentView.WidthRequestProperty, 
            //    new TemplateBinding("OwnerListView." + nameof(AtomListView.ItemIconWidth)));





            AtomCheckBoxImage img = new AtomCheckBoxImage
            {
                AutomationId = "ListViewCheckBox",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };
            img.SetBinding(Image.WidthRequestProperty, new Binding($"{nameof(AtomListViewCell.OwnerListView)}.{nameof(AtomListView.ItemIconWidth)}") {
                Source = this
            });
            img.SetBinding(Image.MarginProperty, new Binding($"{nameof(OwnerListView)}.{nameof(AtomListView.ItemIconMargin)}") {
                Source = this
            });
            img.SetBinding(AtomCheckBoxImage.UncheckedSourceProperty, new Binding($"{nameof(OwnerListView)}.{nameof(AtomListView.ItemIcon)}") {
                Source = this
            });

            img.SetBinding(AtomCheckBoxImage.CheckedSourceProperty, new Binding($"{nameof(OwnerListView)}.{nameof(AtomListView.SelectedItemIcon)}") {
                Source = this
            });
            img.SetBinding(AtomCheckBoxImage.IsCheckedProperty, new Binding(nameof(IsSelected)) {
                Source = this
            });
            var tp = new TapGestureRecognizer {
            };
            tp.SetBinding(TapGestureRecognizer.CommandProperty, new Binding(nameof(ToggleSelectionCommand)) {
                Source = this
            });
            img.GestureRecognizers.Add(tp);
            //img.ToggleOnTouch = true;


            //cv.Content = img;

            



            Children.Add(img);

            //var v = new ContentPresenter();

            //Grid.SetColumn(v, 1);
            //Grid.SetRowSpan(v, 2);
            //Children.Add(v);

            ContentRowSpan = 2;
            ContentColumn = 1;


            //cv.GestureRecognizers.Add(new TapGestureRecognizer
            //{
            //    Command = new AtomCommand(() =>
            //    {
            //        var avc = this.Parent as AtomListViewCell;
            //        avc.IsSelected = !avc.IsSelected;
            //    })
            //});



        }

    }
}
