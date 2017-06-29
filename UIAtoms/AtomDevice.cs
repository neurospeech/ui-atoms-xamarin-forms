using NeuroSpeech.UIAtoms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AtomDevice))]

namespace NeuroSpeech.UIAtoms
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomDevice
    {
        /// <summary>
        /// 
        /// </summary>
        public AtomDevice()
        {

        }

        private static AtomDevice _Device;

        /// <summary>
        /// 
        /// </summary>
        public static AtomDevice Instance
        {
            get
            {
                return _Device ?? (_Device = DependencyService.Get<AtomDevice>());
            }
        }


        /// <summary>
        /// Executes asynchronous task on UI Thread, this is helpful when catching an exception
        /// </summary>
        /// <param name="task"></param>
        public static void RunOnUiThreadAsync(Func<Task> task) {
            Instance.InvokeRunOnUiThreadAsync(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void ReportException(Exception ex) {
            Instance.InternalReportException(ex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <param name="timeout"></param>
        public static void Trigger(ref CancellationTokenSource id, Func<Task> action, TimeSpan timeout)
        {
            id = Instance.InternalTrigger(id, action, timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected virtual CancellationTokenSource InternalTrigger(CancellationTokenSource id, Func<Task> action, TimeSpan timeout)
        {
            id?.Cancel();
            id = UIAtomsApplication.Instance.SetTimeout(() =>
            {
                this.InvokeRunOnUiThreadAsync(action);
            }, timeout);
            return id;
        }

        private static TimeSpan defaultTimeout = TimeSpan.FromMilliseconds(100);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        public static void Trigger(ref CancellationTokenSource id, Func<Task> action)
        {
            Trigger(ref id, action, defaultTimeout);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public static void TriggerOnce(Func<Task> action)
        {
            TriggerOnce(action, defaultTimeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="timeout"></param>
        public static void TriggerOnce(Func<Task> action, TimeSpan timeout)
        {
            Instance.InternalTriggerOnce(action, timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="timeout"></param>
        protected virtual void InternalTriggerOnce(Func<Task> action, TimeSpan timeout)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ActionKey key = new ActionKey(action);
                CancellationTokenSource ct = null;
                ct = UIAtomsApplication.Instance.SetTimeout(async () =>
                {
                    timeouts.TryRemove(key, out ct);
                    try
                    {
                        await action();
                    }
                    catch (Exception ex) {
                        InternalReportException(ex);
                    }
                }, timeout);

                timeouts.AddOrUpdate(key, ct, (k,oldValue) => {
                    oldValue.Cancel();
                    return ct;
                });

                ct.Token.Register(() => {
                    timeouts.TryRemove(key, out ct);
                });

            });

        }


        //Dictionary<ActionKey, ActionKey> executionScope = new Dictionary<ActionKey, ActionKey>();
        //public void TriggerNonRecursive(Action action) {
        //    ActionKey key = new ActionKey(action);
        //    if (executionScope.ContainsKey(key))
        //        return;
        //    try {
        //        executionScope[key] = key;
        //        action();
        //    } finally {
        //        executionScope.Remove(key);
        //    }

        //}

        ConcurrentDictionary<ActionKey, CancellationTokenSource> timeouts = new ConcurrentDictionary<ActionKey, CancellationTokenSource>();

        internal class ActionKey
        {
            private Func<Task> action;

            public ActionKey(Func<Task> action)
            {
                this.action = action;
            }

            public override int GetHashCode()
            {

                string key = (action.Target?.GetType()?.FullName ?? "") + "." + (action.Method.ToString());
                return key.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                ActionKey k = obj as ActionKey;
                if (k == null)
                    return false;
                if (k.action.Method != action.Method || k.action.Target != action.Target)
                    return false;
                return true;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        protected virtual void InvokeRunOnUiThreadAsync(Func<Task> task)
        {
            Device.BeginInvokeOnMainThread(async () => {
                try
                {
                    await task();
                }
                catch (Exception ex)
                {
                    InternalReportException(ex);
                }
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void InternalReportException(Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.Message, ex.ToString());
        }
    }
}
