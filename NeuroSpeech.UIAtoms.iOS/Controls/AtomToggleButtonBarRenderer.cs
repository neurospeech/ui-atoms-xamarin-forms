using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(AtomToggleButtonBar), typeof(AtomToggleButtonBarRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomToggleButtonBarRenderer : ViewRenderer<AtomToggleButtonBar,UISegmentedControl>
    {
        public AtomToggleButtonBarRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<AtomToggleButtonBar> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            var control = new UISegmentedControl();
            SetNativeControl(control);
            Recreate();
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            var selectedItem = Control.SelectedSegment;
            var selectedObj = items.FirstOrDefault(x => x.Item1 == selectedItem);
            Element.SelectedItem = selectedObj?.Item2;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch(e.PropertyName)
            {
                case nameof(AtomToggleButtonBar.LabelPath):
                case nameof(AtomToggleButtonBar.Version):
                case nameof(AtomToggleButtonBar.ItemsSource):
                    Recreate();
                    break;
                case nameof(AtomToggleButtonBar.SelectedItem):
                    ResetStates();
                    break;

            }

           

        }

        List<Tuple<nint, object>> items = new List<Tuple<nint, object>>();

        private void ResetStates()
        {
            var item = items.FirstOrDefault(x => x.Item2 == Element.SelectedItem);
            if (item != null) {
                Control.SelectedSegment = item.Item1;
            }
        }

        private void Recreate()
        {
            DisposeChildren();

            var source = Element?.ItemsSource;
            if (source == null)
                return;

            nint i = 0;

            Func<object, string> getText = x => x.ToString();

            if (Element.LabelPath != null) {
                getText = x => (string)x.GetType().GetProperty(Element.LabelPath).GetValue(x);
            }

            foreach (var item in source) {

                string label = getText(item);
                items.Add(new Tuple<nint, object>(i, item));
                Control.InsertSegment(label, i++, false);

            }

            Control.ValueChanged += Control_ValueChanged;
            ResetStates();


        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            DisposeChildren();
        }

        private void DisposeChildren()
        {
            if (Control == null)
                return;
            Control.ValueChanged -= Control_ValueChanged;
            Control.RemoveAllSegments();


            
        }
    }
}
