using NeuroSpeech.UIAtoms.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ShortMemoryCache))]

namespace NeuroSpeech.UIAtoms.Web
{
    /// <summary>
    /// Class helps in caching GET HTTP resources for short period of time
    /// </summary>
    public class ShortMemoryCache
    {

        /// <summary>
        /// Leave it to default 30 seconds
        /// </summary>
        public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromSeconds(30);

        private ConcurrentDictionary<string, object> _memoryCache = new ConcurrentDictionary<string, object>();

        public void Add(string key, object data, TimeSpan? defaultTimespan = null) {
            var ts = defaultTimespan == null ? DefaultTimeout : defaultTimespan.Value;

            _memoryCache.AddOrUpdate(key, data, (f, a) =>
            {
                return data;
            });

            if (ts == TimeSpan.MaxValue)
                return;

            Task.Run(async () =>
            {
                await Task.Delay(ts);
                object d;
                _memoryCache.TryRemove(key, out d);
            });            
        }

        public object Get(string key) {
            object d = null;
            _memoryCache.TryGetValue(key, out d);
            return d;
        }

        //public Task<object> GetAsync(string key) {
        //    return Task.Run(() =>
        //    {
        //        object d = null;
        //        _memoryCache.TryGetValue(key, out d);
        //        return d;
        //    });
        //}

        //public Task<object> GetOrAddAsync(string key, Func<string, object> loader, TimeSpan? ts = null) {
        //    return Task.Run(()=> {
        //        ts = ts ?? DefaultTimeout;
        //        return _memoryCache.GetOrAdd(key, (k)=> {
        //            var v = loader(k);
        //            if (v != null) {
        //                if (ts != TimeSpan.MaxValue)
        //                {
        //                    Task.Run(async()=> {
        //                        await Task.Delay(ts.Value);
        //                        object d;
        //                        _memoryCache.TryRemove(key, out d);
        //                    });
        //                }
        //            }
        //            return v;
        //        });
        //    });
        //}

    }

}
