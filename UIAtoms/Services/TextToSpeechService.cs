using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms.Services
{
    public abstract class TextToSpeechService: IDisposable
    {

        public TextToSpeechService()
        {

        }


        public abstract void Dispose();

        public abstract Task Speak(string text, CancellationToken cancellationToken);

        


    }
}
