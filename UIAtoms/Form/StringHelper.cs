using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;

namespace NeuroSpeech.UIAtoms
{
    internal static class StringHelper
    {

        internal static bool HasText(this string t, string v) {

            bool isValueEmpty = string.IsNullOrWhiteSpace(v);

            if (string.IsNullOrWhiteSpace(t)) {
                return false;
            }

            return t.IndexOf(v, StringComparison.CurrentCultureIgnoreCase) != -1;
        }


        private static readonly UIMemoryCache<string, BindableProperty> BindableProperties
            = new UIMemoryCache<string, BindableProperty>(null);

        internal static BindableProperty ResolveBindableProperty(this Type type, string name) {
            string key = type.FullName + ":" + name;
            return BindableProperties.GetOrAdd(key, x => {
                var f = type.GetRuntimeField(name + "Property");
                return (BindableProperty)f.GetValue(null);
            });
        }


    }

    internal class UIMemoryCache<TKey, TValue> : Dictionary<TKey,TValue> {

        private readonly Func<TKey, TValue> factory;

        public UIMemoryCache(Func<TKey,TValue> tf)
        {
            factory = tf;
        }

        public TValue GetOrAdd(TKey key, Func<TKey,TValue> ftv = null) {
            if (ftv == null) {
                ftv = factory;
            }

            lock (this) {
                TValue v;
                if (TryGetValue(key, out v))
                    return v;
                v = factory(key);
                Add(key, v);
                return v;
            }
        }

    }
}
