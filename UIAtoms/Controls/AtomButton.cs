using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// Presents an option to confirm an action before executing the command.
    /// Automatically closes Popup if ClosePopup is set to true (Default is true) after executing the command.
    /// </summary>
    public class AtomButton: Button
    {

        #region Property ConfirmTitle

        /// <summary>
        /// Bindable Property ConfirmTitle
        /// </summary>
        public static readonly BindableProperty ConfirmTitleProperty = BindableProperty.Create(
          "ConfirmTitle",
          typeof(string),
          typeof(AtomButton),
          "Question?",
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
        /// On ConfirmTitle changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnConfirmTitleChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ConfirmTitle
        /// </summary>
        public string ConfirmTitle
        {
            get
            {
                return (string)GetValue(ConfirmTitleProperty);
            }
            set
            {
                SetValue(ConfirmTitleProperty, value);
            }
        }
        #endregion



        #region Property Confirm

        /// <summary>
        /// Bindable Property Confirm
        /// </summary>
        public static readonly BindableProperty ConfirmProperty = BindableProperty.Create(
          "Confirm",
          typeof(bool),
          typeof(AtomButton),
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
        /// On Confirm changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnConfirmChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property Confirm
        /// </summary>
        public bool Confirm
        {
            get
            {
                return (bool)GetValue(ConfirmProperty);
            }
            set
            {
                SetValue(ConfirmProperty, value);
            }
        }
        #endregion

        #region Property ConfirmMessage

        /// <summary>
        /// Bindable Property ConfirmMessage
        /// </summary>
        public static readonly BindableProperty ConfirmMessageProperty = BindableProperty.Create(
          "ConfirmMessage",
          typeof(string),
          typeof(AtomButton),
          "Are you sure?",
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
        /// On ConfirmMessage changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnConfirmMessageChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ConfirmMessage
        /// </summary>
        public string ConfirmMessage
        {
            get
            {
                return (string)GetValue(ConfirmMessageProperty);
            }
            set
            {
                SetValue(ConfirmMessageProperty, value);
            }
        }
        #endregion

        #region Property ConfirmButtonText

        /// <summary>
        /// Bindable Property ConfirmButtonText
        /// </summary>
        public static readonly BindableProperty ConfirmButtonTextProperty = BindableProperty.Create(
          "ConfirmButtonText",
          typeof(string),
          typeof(AtomButton),
          "Confirm",
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
        /// On ConfirmButtonText changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnConfirmButtonTextChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ConfirmButtonText
        /// </summary>
        public string ConfirmButtonText
        {
            get
            {
                return (string)GetValue(ConfirmButtonTextProperty);
            }
            set
            {
                SetValue(ConfirmButtonTextProperty, value);
            }
        }
        #endregion

        #region Property CancelButtonText

        /// <summary>
        /// Bindable Property CancelButtonText
        /// </summary>
        public static readonly BindableProperty CancelButtonTextProperty = BindableProperty.Create(
          "CancelButtonText",
          typeof(string),
          typeof(AtomButton),
          "Cancel",
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
        /// On CancelButtonText changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnCancelButtonTextChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property CancelButtonText
        /// </summary>
        public string CancelButtonText
        {
            get
            {
                return (string)GetValue(CancelButtonTextProperty);
            }
            set
            {
                SetValue(CancelButtonTextProperty, value);
            }
        }
        #endregion

        #region Property ClosePopup

        /// <summary>
        /// Bindable Property ClosePopup
        /// </summary>
        public static readonly BindableProperty ClosePopupProperty = BindableProperty.Create(
          nameof(ClosePopup),
          typeof(bool),
          typeof(AtomButton),
          true,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomButton)sender).OnClosePopupChanged(oldValue,newValue),
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
        /// On ClosePopup changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnClosePopupChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// If set to true (default is true), it will automatically close the popup if button is inside the popup
        /// </summary>
        public bool ClosePopup
        {
            get
            {
                return (bool)GetValue(ClosePopupProperty);
            }
            set
            {
                SetValue(ClosePopupProperty, value);
            }
        }
        #endregion

        private ICommand previousCommand = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName) {

                case "Confirm":
                case "Command":
                    ChangeCommand();
                    break;
            }

        }

        private void ChangeCommand()
        {
            if (Command is IntenralCommand)
            {
                return;
            }
            previousCommand = Command;
            if (previousCommand == null)
            {
                Device.BeginInvokeOnMainThread(async () => await CloseOwnerPopup());
                return;
            }
            Command = new IntenralCommand(async a =>
            {
                if (Confirm)
                {
                    var r = await Application.Current.MainPage
                        .DisplayAlert(ConfirmTitle, ConfirmMessage, ConfirmButtonText, CancelButtonText);

                    if (r)
                    {
                        previousCommand.Execute(CommandParameter);
                        await CloseOwnerPopup();
                    }
                }
                else
                {
                    previousCommand.Execute(CommandParameter);
                    await CloseOwnerPopup();
                }

                

            }, previousCommand);
        }

        private async System.Threading.Tasks.Task CloseOwnerPopup()
        {
            if (ClosePopup)
            {
                var page = this.GetParentOfType<AtomPopupPage>();
                if (page != null)
                {
                    await DependencyService.Get<INavigation>().PopModalAsync();
                }
            }
        }
    }

    internal class IntenralCommand : ICommand {
        private readonly Action<object> action;
        private readonly ICommand command;

        public IntenralCommand(Action<object> action, ICommand command)
        {
            this.command = command;
            this.action = action;
        }

        public event EventHandler CanExecuteChanged {
            add {
                command.CanExecuteChanged += value;
            }
            remove {
                command.CanExecuteChanged -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return command.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }

    /// <summary>
    /// Derived from AtomButton with Confirm as true
    /// </summary>
    public class AtomDeleteButton : AtomButton {

        /// <summary>
        /// 
        /// </summary>
        public AtomDeleteButton()
        {
            Confirm = true;
            ConfirmMessage = "Are you sure you want to delete this?";
        }

    }
}
