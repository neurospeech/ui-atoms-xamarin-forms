using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.UIAtoms
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomUtils
    {

        private static ConcurrentDictionary<Type, object> singletones = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Singleton<T>() {
            Type type = typeof(T);
            return (T)singletones.GetOrAdd(type, x => Activator.CreateInstance(x));
        }

    }
}
