using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NeuroSpeech.UIAtoms.Services;
using Android.Speech.Tts;
using NeuroSpeech.UIAtoms.Controls;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidTextToSpeechService))]

namespace NeuroSpeech.UIAtoms.Services
{
    class AndroidTextToSpeechService : TextToSpeechService
    {
        private TextToSpeech tts;
        private TaskCompletionSource<bool> initialized = new TaskCompletionSource<bool>();

        class Listener : Java.Lang.Object, TextToSpeech.IOnInitListener
        {
            public Action<OperationResult> Init;

            void TextToSpeech.IOnInitListener.OnInit(OperationResult status)
            {
                Init?.Invoke(status);
            }
        }

        class CompletedListener : UtteranceProgressListener
        {
            public Action<string> Start;
            public Action<string> Done;
            public Action<string> Error;

            public override void OnDone(string utteranceId)
            {
                Done?.Invoke(utteranceId);
            }

            public override void OnError(string utteranceId)
            {
                Error?.Invoke(utteranceId);
            }

            public override void OnStart(string utteranceId)
            {
                Start?.Invoke(utteranceId);
            }
        }

        public AndroidTextToSpeechService()
        {
            this.tts = new TextToSpeech(Android.App.Application.Context, new Listener {
                Init = (status) => {
                    var r = tts.SetLanguage(Java.Util.Locale.English);
                    if (r == LanguageAvailableResult.MissingData || r == LanguageAvailableResult.NotSupported) {
                        System.Diagnostics.Debug.Fail("Language is not supported");
                    }
                    initialized?.TrySetResult(true);
                }
            });
            tts.SetOnUtteranceProgressListener(new CompletedListener {
                Done = async (s) => await OnDone(s),
                Error = async (s) => await OnError(s),
                Start = async (s)  => await OnStart(s)
            });

            
        }

        private Task OnStart(string s)
        {
            return Task.CompletedTask;
        }

        private Task OnError(string s)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {
                var Source = await sources.GetOrAddAsync(s, null);
                Source?.TrySetException(new InvalidOperationException("There was an error playing text"));
                Source = null;
            });
            return Task.CompletedTask;
        }

        private Task OnDone(string s)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {
                var Source = await sources.GetOrAddAsync(s, null);
                Source?.TrySetResult(s);
                Source = null;
            });
            return Task.CompletedTask;
        }

        private AsyncDictionary<string, TaskCompletionSource<string>> sources =
            new AsyncDictionary<string, TaskCompletionSource<string>>();

        public override async Task Speak(string text,System.Threading.CancellationToken ct)
        {
            await initialized.Task;            
            string key = DateTime.UtcNow.Ticks.ToString();
            var source = await sources.GetOrAddAsync(key, k => new TaskCompletionSource<string>());
            ct.Register(() => {
                if (tts.IsSpeaking)
                {
                    tts.Stop();
                }
                source.TrySetCanceled();
            });
            tts.Speak(text, QueueMode.Add, null, key);
            try
            {
                await source.Task;
            }
            finally
            {
                await sources.RemoveAsync(key);
            }
        }

        public override void Dispose()
        {
            try
            {
                tts.Shutdown();
                tts.Dispose();
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.Fail(ex.Message, ex.ToString());
            }
            sources.ClearAsync(a=> a.Value?.TrySetCanceled());
        }
    }
}