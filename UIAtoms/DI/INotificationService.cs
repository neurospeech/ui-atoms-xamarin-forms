using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms.DI
{

    /// <summary>
    /// 
    /// </summary>
    public interface INotificationService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="location"></param>
        Task NotifyAsync(string message, ToastGravity location = ToastGravity.Center);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="buttonTitle"></param>
        /// <returns></returns>
        Task AlertAsync(string title, string message, string buttonTitle = "OK");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="trueButtonTitle"></param>
        /// <param name="falseButtonTitle"></param>
        /// <returns></returns>
        Task<bool> ConfirmAsync(string title, string message, string trueButtonTitle = "YES", string falseButtonTitle = "NO");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDisposable ShowBusy();








    }


    /// <summary>
    /// 
    /// </summary>
    public enum ToastGravity
    {

        /// <summary>
        /// 
        /// </summary>
        Top,

        /// <summary>
        /// 
        /// </summary>
        Center,

        /// <summary>
        /// 
        /// </summary>
        Bottom
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IBusyView {

        /// <summary>
        /// 
        /// </summary>
        bool IsVisible { get; set; }

    }



}
