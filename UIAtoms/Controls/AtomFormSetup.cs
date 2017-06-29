using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    internal abstract class BaseAtomFormSetup
    {
        internal abstract void SetupNext(View current, View next);
    }

    internal class AtomFormNext {

        internal View Current;
        internal View Next;

        internal AtomFormNext(View current,View next)
        {
            Current = current;
            Next = next;
        }

    }
}
