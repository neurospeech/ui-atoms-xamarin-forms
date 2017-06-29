using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.DI
{
    public abstract class AtomPreferences
    {

        private static Dictionary<string, AtomPreferences> cache = new Dictionary<string, AtomPreferences>();
        protected string Name { get; private set; }

        public static AtomPreferences Default {
            get {
                return DependencyService.Get<AtomPreferences>(); 
            }
        }

        public static AtomPreferences GetPreferences(string rootName) {
            if (string.IsNullOrWhiteSpace(rootName))
            {
                throw new ArgumentNullException(nameof(rootName));
            }
            AtomPreferences result = null;
            if (!cache.TryGetValue(rootName, out result)) {
                result = DependencyService.Get<AtomPreferences>(DependencyFetchTarget.NewInstance);
                result.Name = rootName;
                cache[rootName] = result;
            }
            return result;
        }

        public abstract Task ClearAsync();

        public abstract string GetValue(string name, string def = null);
        public abstract void SetValue(string name, string v);

        public abstract bool GetValue(string name, bool def = false);
        public abstract void SetValue(string name, bool v);

        public abstract int GetValue(string name, int def = 0);
        public abstract void SetValue(string name, int v);

        public abstract long GetValue(string name, long def = 0);
        public abstract void SetValue(string name, long v);

        public abstract float GetValue(string name, float def = 0);
        public abstract void SetValue(string name, float v);

        public abstract double GetValue(string name, double def = 0);
        public abstract void SetValue(string name, double v);

        public virtual Task<T> GetJsonAsync<T>(string key = null)
            where T : class
        {
            return Task.Run(() =>
            {
                if (key == null)
                {
                    key = typeof(T).FullName;
                }
                string v = GetValue(key, (string)null);
                if (v == null)
                    return null;

                return JsonConvert.DeserializeObject<T>(v);
            });
        }
        public virtual Task SetJsonAsync<T>(T v, string key = null)
            where T : class
        {
            return Task.Run(() =>
            {
                if (key == null)
                {
                    key = typeof(T).FullName;
                }

                SetValue(key, JsonConvert.SerializeObject(v));
            });
        }

    }
}
