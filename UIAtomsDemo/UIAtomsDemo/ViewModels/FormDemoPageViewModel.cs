using NeuroSpeech.UIAtoms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIAtomsDemo.ViewModels
{
    public class FormDemoPageViewModel : AtomViewModel
    {

        public FormDemoPageViewModel()
        {


            //Refresh();
            LoginCommand = new AtomCommand(async()=> await OnLoginAsync());
        }

        int i = 1;
        private void Refresh()
        {
            UsernameLabel = $"Username ({i++})";
            UIAtomsApplication.Instance.SetTimeout(Refresh, TimeSpan.FromSeconds(1));
        }


        #region Property UsernameLabel

        private string _UsernameLabel = "Username";

        public string UsernameLabel
        {
            get
            {
                return _UsernameLabel;
            }
            set
            {
                SetProperty(ref _UsernameLabel, value);
            }
        }
        #endregion


        #region Property IsStaffLogin

        private bool _IsStaffLogin = false;

        public bool IsStaffLogin
        {
            get
            {
                return _IsStaffLogin;
            }
            set
            {
                SetProperty(ref _IsStaffLogin, value);
            }
        }
        #endregion


        #region Property Username

        private string _Username = "";

        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                SetProperty(ref _Username, value);
            }
        }
        #endregion

        #region Property Password

        private string _Password = "";

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetProperty(ref _Password, value);
            }
        }
        #endregion


        public ICommand LoginCommand { get; }

        private async Task OnLoginAsync()
        {
            await notificationService.NotifyAsync("Logged in Successfully");
        }

    }
}
