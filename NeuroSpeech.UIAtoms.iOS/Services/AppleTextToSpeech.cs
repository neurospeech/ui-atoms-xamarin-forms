using AVFoundation;
using NeuroSpeech.UIAtoms.Controls;
using NeuroSpeech.UIAtoms.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppleTextToSpeech))]

namespace NeuroSpeech.UIAtoms.Services
{
    public class AppleTextToSpeech : TextToSpeechService
    {
        private AVSpeechSynthesizer synth;

        public AppleTextToSpeech()
        {
            this.synth = new AVSpeechSynthesizer();

            synth.DidFinishSpeechUtterance += Synth_DidFinishSpeechUtterance;
            synth.DidCancelSpeechUtterance += Synth_DidCancelSpeechUtterance;
        }

        private void Synth_DidCancelSpeechUtterance(object sender, AVSpeechSynthesizerUteranceEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                TaskCompletionSource<string> source = await utterances.GetOrAddAsync(e.Utterance, null);
                source?.TrySetCanceled();
            });
        }

        private void Synth_DidFinishSpeechUtterance(object sender, AVSpeechSynthesizerUteranceEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                TaskCompletionSource<string> source = await utterances.GetOrAddAsync(e.Utterance, null);
                source?.TrySetResult("");
            });
        }

        public override void Dispose()
        {


            if (synth != null) {
                synth.DidFinishSpeechUtterance -= Synth_DidFinishSpeechUtterance;
            }
            synth?.Dispose();
            synth = null;

            utterances.ClearAsync(a => a.Value?.TrySetCanceled());

        }

        private AsyncDictionary<AVSpeechUtterance,TaskCompletionSource<string>> utterances = new AsyncDictionary<AVSpeechUtterance,TaskCompletionSource<string>>();

        public override async Task Speak(string text, System.Threading.CancellationToken cancellationToken)
        {
            var utterance = new AVSpeechUtterance(text);
            var source = await utterances.GetOrAddAsync(utterance, (u) => new TaskCompletionSource<string>());

            cancellationToken.Register(() => {
                if (synth.Speaking)
                {
                    synth.StopSpeaking(AVSpeechBoundary.Immediate);
                }
                source.TrySetCanceled();
            });

            synth.SpeakUtterance(utterance);
            try
            {
                await source.Task;
            }
            finally
            {
                await utterances.RemoveAsync(utterance);
            }
        }
    }

}
