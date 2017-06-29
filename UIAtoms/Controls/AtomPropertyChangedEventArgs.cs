using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomPropertyChangedEventArgs: EventArgs
    {

        public object OldValue { get; set; }

        public object NewValue { get; set; }

        public AtomPropertyChangedEventArgs(object oldValue, object newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

    }
}
