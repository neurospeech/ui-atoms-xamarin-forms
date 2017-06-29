using CoreGraphics;
using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppleNotificationService))]
[assembly: Xamarin.Forms.Dependency(typeof(BusyView))]

namespace NeuroSpeech.UIAtoms.DI
{
    /// <summary>
    /// 
    /// </summary>
    public class AppleNotificationService : INotificationService
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
        /// <returns></returns>
        public Task NotifyAsync(string message, ToastGravity location = ToastGravity.Center)
        {
            AtomToast toast = new AtomToast { Text = message, DurationSeconds = TimeSpan.FromSeconds(5), Gravity = location };
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
                view.IsVisible = !(isBusy <= 0);
            });

        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class BusyView : IBusyView
    {

        UIView view;
        UIActivityIndicatorView indicator;
        bool visible = false;

        /// <summary>
        /// 
        /// </summary>
        public BusyView()
        {
            UIButton v = UIButton.FromType(UIButtonType.Custom);
            view = v;


            indicator = new UIActivityIndicatorView(new CGRect(0, 0, 30, 30));
            v.Frame = new CGRect(0, 0, indicator.Frame.Width+10, indicator.Frame.Height + 10);
            indicator.Center = new CGPoint(v.Frame.Size.Width / 2, v.Frame.Height / 2);

            v.AddSubview(indicator);

            v.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0.7f);
            v.Layer.CornerRadius = 5;

            UIWindow window = UIApplication.SharedApplication.Windows[0];

            CGPoint point = new CGPoint(window.Frame.Size.Width / 2, window.Frame.Size.Height / 2);

            point = new CGPoint(window.Frame.Size.Width / 2, window.Frame.Size.Height / 2);

            ///point = new CGPoint(point.X + offsetLeft, point.Y + offsetTop);
            point = new CGPoint(point.X, point.Y);
            v.Center = point;

            

        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return visible;
            }

            set
            {
                if (visible == value)
                    return;
                visible = value;

                if (value) {
                    UIWindow window = UIApplication.SharedApplication.Windows[0];
                    window.AddSubview(view);
                    indicator.StartAnimating();
                } else {
                    view.RemoveFromSuperview();
                    indicator.StopAnimating();
                }

            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AtomToast {
        private UIButton view;

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan DurationSeconds { get; set; } = TimeSpan.FromSeconds(2);

        /// <summary>
        /// 
        /// </summary>
        public ToastGravity Gravity { get; set; } = ToastGravity.Center;

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Show() {
            UIButton v = UIButton.FromType(UIButtonType.Custom);
            view = v;

            UIFont font = UIFont.SystemFontOfSize(16);
            CGSize textSize = UIStringDrawing.StringSize(Text, font, new CoreGraphics.CGSize(280, 60));

            UILabel label = new UILabel(new CGRect(0, 0, textSize.Width + 5, textSize.Height + 5));
            label.BackgroundColor = UIColor.Clear;
            label.TextColor = UIColor.White;
            label.Font = font;
            label.Text = Text;
            label.Lines = 0;
            label.ShadowColor = UIColor.DarkGray;
            label.ShadowOffset = new CGSize(1, 1);


            v.Frame = new CGRect(0, 0, textSize.Width + 10, textSize.Height + 10);
            label.Center = new CGPoint(v.Frame.Size.Width / 2, v.Frame.Height / 2);
            v.AddSubview(label);

            v.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0.7f);
            v.Layer.CornerRadius = 5;

            UIWindow window = UIApplication.SharedApplication.Windows[0];

            CGPoint point = new CGPoint(window.Frame.Size.Width / 2, window.Frame.Size.Height / 2);

            if (Gravity == ToastGravity.Top)
            {
                point = new CGPoint(window.Frame.Size.Width / 2, 45);
            }
            else if (Gravity == ToastGravity.Bottom)
            {
                point = new CGPoint(window.Frame.Size.Width / 2, window.Frame.Size.Height - 45);
            }
            else if (Gravity == ToastGravity.Center)
            {
                point = new CGPoint(window.Frame.Size.Width / 2, window.Frame.Size.Height / 2);
            }

            ///point = new CGPoint(point.X + offsetLeft, point.Y + offsetTop);
            point = new CGPoint(point.X , point.Y );
            v.Center = point;
            window.AddSubview(v);
            v.AllTouchEvents += delegate { HideToast(); };

            
            Device.BeginInvokeOnMainThread(async () => {
                await Task.Delay(DurationSeconds);
                HideToast();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Closed;

        private void HideToast()
        {
            if (view == null)
                return;
            UIView.BeginAnimations("");
            view.Alpha = 0;
            UIView.CommitAnimations();

            view.RemoveFromSuperview();
            view = null;

            Closed?.Invoke(this, EventArgs.Empty);
        }
    }
}
