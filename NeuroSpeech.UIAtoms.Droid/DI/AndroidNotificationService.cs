using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NeuroSpeech.UIAtoms.DI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidNotificationService))]
[assembly: Xamarin.Forms.Dependency(typeof(BusyView))]

namespace NeuroSpeech.UIAtoms.DI
{
    /// <summary>
    /// 
    /// </summary>
    public class AndroidNotificationService : INotificationService
    {

        Task INotificationService.AlertAsync(string title, string message, string buttonTitle)
        {
            return Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, buttonTitle);
        }

        Task<bool> INotificationService.ConfirmAsync(string title, string message, string trueButtonTitle, string falseButtonTitle)
        {
            return Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, trueButtonTitle, falseButtonTitle);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="location"></param>
        public Task NotifyAsync(string message, ToastGravity location = ToastGravity.Center)
        {
            var toast = Toast.MakeText(Xamarin.Forms.Forms.Context, message, ToastLength.Long);
            if (location != ToastGravity.Center)
            {
                toast.SetGravity(GravityFlags.Center, 0, 0);
            }
            toast.Show();
            return Task.CompletedTask;
        }

        private int isBusy = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDisposable ShowBusy()
        {

            var view = Xamarin.Forms.DependencyService.Get<IBusyView>();

            isBusy++;

            view.IsVisible = true;

            return new AtomDisposableAction(() => {
                isBusy--;
                view.IsVisible = !(isBusy == 0);
            });
                        
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class BusyView : IBusyView
    {

        private PopupWindow window = null;

        /// <summary>
        /// 
        /// </summary>
        public BusyView()
        {
            CreateProgressWindow();
        }

        private void CreateProgressWindow()
        {
            Android.Widget.ProgressBar progress = new Android.Widget.ProgressBar(Xamarin.Forms.Forms.Context);


            window = new PopupWindow(progress, 60, 60);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return window.IsShowing;
            }

            set
            {
                if (value)
                {

                    if (window == null) {
                        CreateProgressWindow();
                    }

                    if (!window.IsShowing) {

                        var decoreView = ((Activity)Forms.Context).Window.DecorView;
                        if (decoreView == null)
                            return;
                        try
                        {
                            window.ShowAtLocation(decoreView, GravityFlags.Left | GravityFlags.Top, decoreView.Width / 2 - 30, decoreView.Height / 2 - 30);
                        }
                        catch (Exception ex) {
                            System.Diagnostics.Debug.WriteLine(ex);
                        }
                    }
                }
                else {
                    try
                    {
                        if (window.IsShowing)
                            window.Dismiss();
                    }
                    catch {
                        window = null;
                    }
                }
            }
        }
    }

}