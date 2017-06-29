using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomWebView: WebView
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="AtomWebView"/> class.
        /// </summary>
        public AtomWebView()
        {
            this.Navigating += AtomWebView_Navigating;
        }

        private string lastUrl = null;
        private bool lastResult = false;

        private void AtomWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            var n = NavigatingCommand;
            if (n == null)
                return;

            if (e.Url == lastUrl) {
                e.Cancel = lastResult;
                return;
            }

            var ae = new AtomWebNavigatingEventArgs() { Url = e.Url, Cancel = e.Cancel };
            n.Execute(ae);
            e.Cancel = ae.Cancel;

            lastUrl = e.Url;
            lastResult = e.Cancel;

            UIAtomsApplication.Instance.SetTimeout(() => {
                lastUrl = null;
                lastResult = false;
            }, TimeSpan.FromMilliseconds(100));
        }


        #region Property NavigatingCommand

        /// <summary>
        /// Bindable Property NavigatingCommand
        /// </summary>
        public static readonly BindableProperty NavigatingCommandProperty = BindableProperty.Create(
          "NavigatingCommand",
          typeof(ICommand),
          typeof(AtomWebView),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomWebView)sender).OnNavigatingCommandChanged(oldValue,newValue),
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

        





        /// <summary>
        /// Property NavigatingCommand
        /// </summary>
        public ICommand NavigatingCommand
        {
            get
            {
                return (ICommand)GetValue(NavigatingCommandProperty);
            }
            set
            {
                SetValue(NavigatingCommandProperty, value);
            }
        }
        #endregion



    }

    /// <summary>
    /// 
    /// </summary>
    public interface IWebNavigatingEventArgs {

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        string Url { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IWebNavigatingEventArgs"/> is cancel.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancel; otherwise, <c>false</c>.
        /// </value>
        bool Cancel { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class AtomWebNavigatingEventArgs: IWebNavigatingEventArgs {

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AtomWebNavigatingEventArgs"/> is cancel.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancel; otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }
    }
}
