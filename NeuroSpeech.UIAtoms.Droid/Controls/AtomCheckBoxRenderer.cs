//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Xamarin.Forms;
//using NeuroSpeech.UIAtoms.Controls;
//using Xamarin.Forms.Platform.Android;
//using System.ComponentModel;

//[assembly: ExportRenderer(typeof(AtomCheckBox),typeof(AtomCheckBoxRenderer))]

//namespace NeuroSpeech.UIAtoms.Controls
//{
//    public class AtomCheckBoxRenderer : ViewRenderer<AtomCheckBox,CheckBox>
//    {

//        public AtomCheckBoxRenderer()
//        {

//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<AtomCheckBox> e)
//        {
//            base.OnElementChanged(e);

//            if (Element == null)
//                return;

//            var control = new CheckBox(Android.App.Application.Context);
//            control.Text = Element.Label;
//            control.CheckedChange += Control_CheckedChange;
//            SetNativeControl(control);
//        }

//        private void Control_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
//        {
//            if (Element == null)
//                return;
//            Element.IsChecked = e.IsChecked;
//        }

//        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            base.OnElementPropertyChanged(sender, e);

//            if (e.PropertyName == "Label") {
//                Control.Text = Element.Label;
//            }
//            if (e.PropertyName == "IsChecked") {
//                if(Control.Checked != Element.IsChecked)
//                    Control.Checked = Element.IsChecked;
//            }
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (Control != null) {
//                Control.CheckedChange -= Control_CheckedChange;
//            }
//            base.Dispose(disposing);
//        }

//    }
//}