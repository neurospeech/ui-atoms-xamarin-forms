using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomDevice {

        /// <summary>
        /// 
        /// </summary>
        public static AtomDevice Instance = new AtomDevice();


        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<LogEventArgs> LogEvent;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="logCancelledException"></param>
        public void RunOnUIThread(Func<Task> task, bool logCancelledException = false) {
            Device.BeginInvokeOnMainThread(async () => {
                try {
                    await task();
                } catch (Exception ex) {
                    if (ex is TaskCanceledException) {
                        if (!logCancelledException)
                            return;
                    }
                    Log(ex);
                }
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public CancellationTokenSource SetTimeout(Func<Task> task, TimeSpan delay) {
            return UIAtomsApplication.Instance.SetTimeout(() => RunOnUIThread(task), delay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="maxDelay"></param>
        public void TriggerOnce(Func<Task> task, TimeSpan maxDelay) {
            Device.BeginInvokeOnMainThread(async () => {
                try
                {
                    var key = task.Method;
                    CancellationTokenSource ct = null;
                    if (timeouts.TryGetValue(key, out ct)) {
                        ct.Cancel();
                    }
                    timeouts[key] = new CancellationTokenSource();
                    try
                    {
                        await Task.Delay(maxDelay, ct.Token);
                        if (!ct.IsCancellationRequested)
                        {
                            await task();
                        }
                    }
                    catch (TaskCanceledException)
                    {
                    }
                    catch (Exception ex)
                    {
                        Log(ex);
                    }
                    finally {
                        timeouts.Remove(key);
                    }
                }
                catch (TaskCanceledException) {
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        private Dictionary<System.Reflection.MethodInfo, CancellationTokenSource> timeouts = new Dictionary<System.Reflection.MethodInfo, CancellationTokenSource>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void Log(Exception ex) {
            LogEvent?.Invoke(this, new LogEventArgs { Error = ex });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Log(string msg)
        {
            LogEvent?.Invoke(this, new LogEventArgs { Text = msg });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LogEventArgs : EventArgs
    {

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Exception Error { get; set; }
    }

}
