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
using NeuroSpeech.UIAtoms.Controls;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(AtomFrame), typeof(AtomFrameRenderer))]

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomFrameRenderer: Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public AtomFrameRenderer(Context context): base(context)
        {

        }
    }
}