using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms.Common
{

    /// <summary>
    /// Represents an asynchronous message loop, this is different then usual message queue
    /// where a delegate is scheduled to run, this queue represents asynchronous tasks and
    /// it will only execute next task after the current has finished.
    /// </summary>
    public class AtomAsyncDispatcher
    {
        private static Task<int> CompletedTask = Task<int>.FromResult(0);

        private AutoResetEvent waiter = new AutoResetEvent(false);
        private ConcurrentQueue<Func<Task>> queue = new ConcurrentQueue<Func<Task>>();

        /// <summary>
        /// 
        /// </summary>
        public AtomAsyncDispatcher()
        {
            Task.Run(Run);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Post(Func<Task> action)
        {
            queue.Enqueue(action);
            waiter.Set();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task PostAsync(Func<Task> action)
        {
            Post(action);
            return CompletedTask;
        }

        private async Task Run()
        {
            while (true)
            {
                waiter.WaitOne();
                Func<Task> action = null;
                if (queue.TryDequeue(out action))
                {
                    try
                    {
                        await action();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                }
            }
        }
    }
}
