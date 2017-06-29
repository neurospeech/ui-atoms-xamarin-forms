using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomToolbarItem: ToolbarItem
    {

        public AtomToolbarItem()
        {
            Device.BeginInvokeOnMainThread(async () => {
                await Task.Delay(100);

                _parent = this.Parent as ContentPage;

                OnIsVisibleChanged(false, this.IsVisible);


            });
        }


        #region Property IsVisible

        /// <summary>
        /// Bindable Property IsVisible
        /// </summary>
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
          "IsVisible",
          typeof(bool),
          typeof(AtomToolbarItem),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomToolbarItem)sender).OnIsVisibleChanged(oldValue,newValue),
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

        ContentPage _parent;
        
        /// <summary>
        /// On IsVisible changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnIsVisibleChanged(object oldValue, object newValue)
        {

            if (_parent == null)
                return;

            var newVisible = (bool)newValue;
            var isVisible = _parent.ToolbarItems.Contains(this);

            if (newVisible)
            {
                if (!isVisible)
                {
                    _parent.ToolbarItems.Add(this);
                }
            }
            else {
                if (isVisible) {
                    _parent.ToolbarItems.Remove(this);
                }
            }
            
        }


        /// <summary>
        /// Property IsVisible
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return (bool)GetValue(IsVisibleProperty);
            }
            set
            {
                SetValue(IsVisibleProperty, value);
            }
        }
        #endregion



    }
}
