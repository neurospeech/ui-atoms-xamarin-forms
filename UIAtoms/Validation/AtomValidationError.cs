using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Validation
{
    public class AtomValidationError
    {
        public string Message { get; set; }
        public BindableProperty Property { get; set; }
        public View Source { get; set; }
    }
}
