using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    //public class AtomPopup: ContentView
    //{



    //}

    //public class AtomPopupButton: AtomButton {

    //    public AtomPopupButton()
    //    {
    //        Command = new AtomCommand(async ()=> await OnCommandAsync());
    //    }

    //    private Task OnCommandAsync()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    [ContentProperty("Popup")]
    public class AtomPopupToolbarItem : AtomToolbarItem
    {



        #region Property Popup

        /// <summary>
        /// Bindable Property Popup
        /// </summary>
        public static readonly BindableProperty PopupProperty = BindableProperty.Create(
          nameof(Popup),
          typeof(DataTemplate),
          typeof(AtomPopupToolbarItem),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomPopupToolbarItem)sender).OnPopupChanged(oldValue,newValue),
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
        /// On Popup changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnPopupChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Popup
        /// </summary>
        public DataTemplate Popup
        {
            get
            {
                return (DataTemplate)GetValue(PopupProperty);
            }
            set
            {
                SetValue(PopupProperty, value);
            }
        }
        #endregion



        #region Property PopupTitle

        /// <summary>
        /// Bindable Property PopupTitle
        /// </summary>
        public static readonly BindableProperty PopupTitleProperty = BindableProperty.Create(
          nameof(PopupTitle),
          typeof(string),
          typeof(AtomPopupToolbarItem),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomPopupToolbarItem)sender).OnPopupTitleChanged(oldValue,newValue),
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
        /// On PopupTitle changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnPopupTitleChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property PopupTitle
        /// </summary>
        public string PopupTitle
        {
            get
            {
                return (string)GetValue(PopupTitleProperty);
            }
            set
            {
                SetValue(PopupTitleProperty, value);
            }
        }
        #endregion



        #region Property CloseWhenClickedOutside

        /// <summary>
        /// Bindable Property CloseWhenClickedOutside
        /// </summary>
        public static readonly BindableProperty CloseWhenClickedOutsideProperty = BindableProperty.Create(
          nameof(CloseWhenClickedOutside),
          typeof(bool),
          typeof(AtomPopupToolbarItem),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomPopupToolbarItem)sender).OnCloseWhenClickedOutsideChanged(oldValue,newValue),
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
        /// On CloseWhenClickedOutside changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCloseWhenClickedOutsideChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CloseWhenClickedOutside
        /// </summary>
        public bool CloseWhenClickedOutside
        {
            get
            {
                return (bool)GetValue(CloseWhenClickedOutsideProperty);
            }
            set
            {
                SetValue(CloseWhenClickedOutsideProperty, value);
            }
        }
        #endregion



        public AtomPopupToolbarItem()
        {
            Command = new AtomCommand(async () => await OnCommandAsync());
        }

        private async Task OnCommandAsync()
        {

            var c = Popup?.CreateContent() as VisualElement;
            if (c == null)
                return;

            var p = c as AtomPopupPage;
            if (p == null) {
                p = new AtomPopupPage {
                    Content = c as View
                };
            }
            p.SetBinding(AtomPopupPage.BindingContextProperty, new Binding {
                Path = nameof(BindingContext),
                Source = this
            });
            p.Title = PopupTitle ?? Text;
            p.CloseWhenBackgroundIsClicked = CloseWhenClickedOutside;

            var nav = DependencyService.Get<INavigation>();
            await nav.PushModalAsync(p);
        }
    }

}
