using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomToggleButtonBar : View
    {

        //Grid grid;

        /// <summary>
        /// 
        /// </summary>
        public AtomToggleButtonBar()
        {
            //grid = new Grid();
            //this.Content = grid;

            //this.ItemTemplate = new DataTemplate(typeof(Button));
        }


        #region Property ItemsSource

        /// <summary>
        /// Bindable Property ItemsSource
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
          nameof(ItemsSource),
          typeof(System.Collections.IEnumerable),
          typeof(AtomToggleButtonBar),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
           (sender,oldValue,newValue) => ((AtomToggleButtonBar)sender).OnItemsSourceChanged(oldValue,newValue),
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
        /// On ItemsSource changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemsSourceChanged(object oldValue, object newValue)
        {
            ItemsSourceChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ItemsSourceChanged;

        /// <summary>
        /// Property ItemsSource
        /// </summary>
        public System.Collections.IEnumerable ItemsSource
        {
            get
            {
                return (System.Collections.IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        #endregion

        #region Property SelectedItem

        /// <summary>
        /// Bindable Property SelectedItem
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
          nameof(SelectedItem),
          typeof(object),
          typeof(AtomToggleButtonBar),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
           (sender,oldValue,newValue) => ((AtomToggleButtonBar)sender).OnSelectedItemChanged(oldValue,newValue),
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
        /// On SelectedItem changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSelectedItemChanged(object oldValue, object newValue)
        {
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler SelectedItemChanged;


        /// <summary>
        /// Property SelectedItem
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }
        #endregion


        #region Property LabelPath

        /// <summary>
        /// Bindable Property LabelPath
        /// </summary>
        public static readonly BindableProperty LabelPathProperty = BindableProperty.Create(
          nameof(LabelPath),
          typeof(string),
          typeof(AtomToggleButtonBar),
          null,
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
        /// On LabelPath changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnLabelPathChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property LabelPath
        /// </summary>
        public string LabelPath
        {
            get
            {
                return (string)GetValue(LabelPathProperty);
            }
            set
            {
                SetValue(LabelPathProperty, value);
            }
        }
        #endregion





        #region Property IsVertical

        /// <summary>
        /// Bindable Property IsVertical
        /// </summary>
        public static readonly BindableProperty IsVerticalProperty = BindableProperty.Create(
          nameof(IsVertical),
          typeof(bool),
          typeof(AtomToggleButtonBar),
          false,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomToggleButtonBar)sender).OnIsVerticalChanged(oldValue,newValue),
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
        /// On IsVertical changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsVerticalChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property IsVertical
        /// </summary>
        public bool IsVertical
        {
            get
            {
                return (bool)GetValue(IsVerticalProperty);
            }
            set
            {
                SetValue(IsVerticalProperty, value);
            }
        }
        #endregion






        #region Old
        //    #region Property ItemTemplate
        //    public static readonly BindableProperty ItemTemplateProperty =
        //BindableProperty.Create<AtomToggleButtonBar, DataTemplate>(
        //    x => x.ItemTemplate,
        //    null,
        //    BindingMode.OneWay,
        //    (sender, value) => true,
        //    (sender, oldValue, newValue) => ((AtomToggleButtonBar)sender).OnItemTemplateChanged(oldValue, newValue),
        //    (sender, oldValue, newValue) => { },
        //    (sender, value) => value
        //    );

        //    protected virtual void OnItemTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        //    {
        //        RecreateItems();
        //    }


        //    public DataTemplate ItemTemplate
        //    {
        //        get
        //        {
        //            return (DataTemplate)GetValue(ItemTemplateProperty);
        //        }
        //        set
        //        {
        //            SetValue(ItemTemplateProperty, value);
        //        }
        //    }
        //    #endregion

        //    public Color SelectedColor { get; set; }

        //    public Color SelectedBackground { get; set; } 
        #endregion

        private void RecreateItems()
        {
            //grid.Children.Clear();
            //if (ItemsSource == null)
            //    return;

            //foreach (var item in ItemsSource) {
            //    var view = ItemTemplate.CreateContent() as View;
            //    if (view == null) {
            //        throw new InvalidOperationException("DataTemplate of AtomToggleButtonBar must be View and not Cell");
            //    }
            //    view.GestureRecognizers.Add(new TapGestureRecognizer {
            //        Command = new AtomCommand(() => SelectedItem = item)
            //    });
            //    grid.Children.Add(view);
            //    view.BackgroundColor = this.BackgroundColor;
                
                
            //}
        }

    }
}
