using System;
using System.Linq;
using System.Collections.Generic;
using NeuroSpeech.UIAtoms.DI;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.DI
{

    public class NavigationItem {
        public string Page { get; }
        private Dictionary<string,string> Parameters { get;}

        public NavigationItem(string uri, Dictionary<string,string> values = null)
        {
            Page = uri;
            Parameters = new Dictionary<string, string>();
            int index = uri.IndexOf('?');
            if (index != -1) {
                Page = uri.Substring(0, index);
                Parse(uri.Substring(index + 1));
            }
        }

        public string Get(string name, string def = null) {
            string v = null;
            Parameters.TryGetValue(name.ToLower(), out v);
            return v;
        }

        public long GetLong(string name, long n = 0) {
            long.TryParse(Get(name, "0"), out n);
            return n;
        }

        public int GetInt(string name, int n = 0)
        {
            int.TryParse(Get(name, "0"), out n);
            return n;
        }

        public float GetFloat(string name, float n = 0)
        {
            float.TryParse(Get(name, "0"), out n);
            return n;
        }

        public double GetDouble(string name, double n = 0)
        {
            double.TryParse(Get(name, "0"), out n);
            return n;
        }

        public decimal GetDecimal(string name, decimal n = 0)
        {
            decimal.TryParse(Get(name, "0"), out n);
            return n;
        }

        public bool GetBool(string name)
        {
            return Get(name,"false").Equals("true", StringComparison.CurrentCulture);
        }

        private void Parse(string v)
        {
            foreach(var pair in v.Split(new char[] {'&'})) {
                var kv = pair.Split(new char[] {'='});
                var key = kv[0];
                string value = key;
                if (kv.Length > 1) {
                    value = kv[1];
                }
                Parameters[key.ToLower()] = value;
            }
        }
    }

}