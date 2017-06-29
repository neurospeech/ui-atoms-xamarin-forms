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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using NeuroSpeech.UIAtoms.Controls;
using NeuroSpeech.UIAtoms.Droid.Controls;
using Android.Support.V4.Widget;
using System.Collections.Specialized;
using System.Collections;
using Android.Database;
using Java.Lang;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(AtomToggleButtonBar),typeof(AtomToggleButtonBarRenderer))]

namespace NeuroSpeech.UIAtoms.Droid.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomToggleButtonBarRenderer : ViewRenderer<AtomToggleButtonBar, LinearLayout>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<AtomToggleButtonBar> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            LinearLayout layout = new LinearLayout(Xamarin.Forms.Forms.Context);
            layout.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);

            SetNativeControl(layout);

            Recreate();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "ItemsSource")
            {
                Recreate();
            }
            else if (e.PropertyName == "SelectedItem")
            {
                ResetStates();
            }
        }

        private List<ViewHolder<Android.Widget.RadioButton>> views = new List<ViewHolder<Android.Widget.RadioButton>>();

        private void Recreate()
        {
            DisposeChildren();
            if (Control == null)
                return;
            Control.RemoveAllViews();
            if (Element == null)
                return;
            if (Element.ItemsSource == null)
                return;

            Func<object, string> getText = x => x.ToString();
            if (Element.LabelPath != null)
            {
                getText = x => (string)x.GetType().GetProperty(Element.LabelPath).GetValue(x);
            }

            if (Element.SelectedItem == null)
            {
                Element.SelectedItem = Element.ItemsSource.Cast<object>().FirstOrDefault();
            }

            foreach (var item in Element.ItemsSource)
            {
                var button = new Android.Widget.RadioButton(Xamarin.Forms.Forms.Context);
                button.Text = getText(item);
                var vh = new ViewHolder<Android.Widget.RadioButton> { Data = item, View = button };
                views.Add(vh);
                var lp = new LinearLayout.LayoutParams(0, LayoutParams.WrapContent);
                lp.Weight = 1;
                button.LayoutParameters = lp;
                Control.AddView(button);
                button.Checked = item == Element.SelectedItem;
                button.Click += Button_Click;
                
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var s = views.FirstOrDefault(x => x.View == sender)?.Data;
            Element.SelectedItem = s;
            ResetStates();
        }

        private void ResetStates()
        {
            foreach (var view in views) {
                view.View.Checked = view.Data == Element.SelectedItem;
                view.View.Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            DisposeChildren();
            base.Dispose(disposing);
        }

        private void DisposeChildren()
        {
            foreach (var view in views)
            {
                view.View.Click -= Button_Click;
            }
            views.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ViewHolder<T>
        {
            /// <summary>
            /// 
            /// </summary>
            public object Data { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public T View { get; set; }
        }
    }
}