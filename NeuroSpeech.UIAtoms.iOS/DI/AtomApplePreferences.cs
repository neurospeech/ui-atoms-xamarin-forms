using Foundation;
using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AtomApplePreferences))]

namespace NeuroSpeech.UIAtoms.DI
{
    class AtomApplePreferences : AtomPreferences
    {

        public AtomApplePreferences()
        {
            
        }

        public override Task ClearAsync()
        {
            if (this.Name == null)
            {
                NSUserDefaults.ResetStandardUserDefaults();
                return Task.CompletedTask;
            }

            return Task.Run(() =>
            {
                var d = NSUserDefaults.StandardUserDefaults;
                var all = d.ToDictionary();

                var prefix = this.Name + ".";

                var matches = all.Keys.Select(x => (string)(NSString)x).Where(x => x.StartsWith(prefix)).ToList();

                foreach (var match in matches) {
                    d.RemoveObject(match);
                }
            });
        }

        private string EscapeName(string name) {
            return this.Name != null ? this.Name + "." + name : name;
        }

        private T Get<T,TC>(String name, T def, Func<TC,T> c = null)
            where TC:NSObject
        {
            var obj = NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)EscapeName(name));
            if (obj == null)
                return def;
            if (c == null) {
                return (T)(object)((TC)obj);
            }
            return c((TC)obj);
        }

        public override bool GetValue(string name, bool def = false)
        {
            return Get<bool,NSNumber>(name,def, n=> n.BoolValue);
        }

        public override string GetValue(string name, string def = null)
        {
            return Get<string, NSString>(name, def);
        }

        public override void SetValue(string name, bool v)
        {
            NSUserDefaults.StandardUserDefaults.SetBool(v,EscapeName(name));
        }

        public override void SetValue(string name, string v)
        {
            NSUserDefaults.StandardUserDefaults.SetString(v, EscapeName(name));
        }

        public override int GetValue(string name, int def = 0)
        {
            return Get<int, NSNumber>(name, def, c => c.Int32Value);
        }

        public override void SetValue(string name, int v)
        {
            NSUserDefaults.StandardUserDefaults.SetInt(v, EscapeName(name));
        }

        public override long GetValue(string name, long def = 0)
        {
            var s = NSUserDefaults.StandardUserDefaults.StringForKey(EscapeName(name));
            return long.Parse(s);
        }

        public override void SetValue(string name, long v)
        {
            NSUserDefaults.StandardUserDefaults.SetString(v.ToString(), EscapeName(name));
        }

        public override float GetValue(string name, float def = 0)
        {
            return Get<float, NSNumber>(name, def, c => c.FloatValue);
        }

        public override void SetValue(string name, float v)
        {
            NSUserDefaults.StandardUserDefaults.SetFloat(v, EscapeName(name));
        }

        public override double GetValue(string name, double def = 0)
        {
            return Get<double, NSNumber>(name, def, c => c.DoubleValue);
        }

        public override void SetValue(string name, double v)
        {
            NSUserDefaults.StandardUserDefaults.SetDouble(v, EscapeName(name));
        }
    }
}