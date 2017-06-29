using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuroSpeech.UIAtoms.DI
{
    public struct ObjectPool<T> : IDisposable
        where T : class
    {


        [ThreadStatic]
        private static Queue<T> pool;

        private T assigned;

        public static ObjectPool<T> Create(int maxCount = 5)
        {
            ObjectPool<T> p = new ObjectPool<T>();
            if (pool == null)
            {
                pool = new Queue<T>();
            }

            if (pool.Any())
            {
                p.assigned = pool.Dequeue();
            }
            else
            {
                p.assigned = Activator.CreateInstance<T>();
            }
            return p;
        }

        public T Assigned
        {
            get
            {
                if (assigned == null)
                    throw new ObjectDisposedException("Pooled object is disposed");
                return assigned;
            }
        }

        public void Dispose()
        {
            pool.Enqueue(assigned);
            assigned = null;
        }
    }
}
