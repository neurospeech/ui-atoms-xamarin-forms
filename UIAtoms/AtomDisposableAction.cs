using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.UIAtoms
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomDisposableAction : IDisposable
    {
        private readonly Action disposableAction;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposableAction"></param>
        public AtomDisposableAction(Action disposableAction = null)
        {
            this.disposableAction = disposableAction;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            disposableAction?.Invoke();
        }
    }


    public class NonRecursiveContext {


        private bool called = false;

        public void Run(Action action) {
            try {
                if (called)
                    return;
                called = true;
                action();
            } finally {
                called = false;
            }
        }   


    }

}
