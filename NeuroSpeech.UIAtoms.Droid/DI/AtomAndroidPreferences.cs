using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NeuroSpeech.UIAtoms.DI;

[assembly: Xamarin.Forms.Dependency(typeof(AtomAndroidPreferences))]

namespace NeuroSpeech.UIAtoms.DI
{
    class AtomAndroidPreferences : AtomPreferences
    {

        private ISharedPreferences GetPreferences() {
            return Xamarin.Forms.Forms.Context.GetSharedPreferences(Name, FileCreationMode.Private);
        }

        private ISharedPreferencesEditor Edit() {
            return GetPreferences().Edit();
        }

        public override Task ClearAsync()
        {
            return Task.Run(()=> {
                Edit()
                .Clear()
                .Commit();
            });
        }

        public override bool GetValue(string name, bool def = false)
        {
            return GetPreferences().GetBoolean(name, def);
        }

        public override int GetValue(string name, int def = 0)
        {
            return GetPreferences().GetInt(name, def);
        }

        public override long GetValue(string name, long def = 0)
        {
            return GetPreferences().GetLong(name, def);
        }

        public override float GetValue(string name, float def = 0)
        {
            return GetPreferences().GetFloat(name, def);
        }

        public override double GetValue(string name, double def = 0)
        {
            var n = GetPreferences().GetString(name, def.ToString());
            return double.Parse(n);
        }

        public override string GetValue(string name, string def = null)
        {
            return GetPreferences().GetString(name, def);
        }

        public override void SetValue(string name, long v)
        {
            Edit().PutLong(name, v).Apply();
        }

        public override void SetValue(string name, double v)
        {
            Edit().PutString(name, v.ToString()).Apply();
        }

        public override void SetValue(string name, float v)
        {
            Edit().PutFloat(name, v).Apply();
        }

        public override void SetValue(string name, int v)
        {
            Edit().PutInt(name, v).Apply();
        }

        public override void SetValue(string name, bool v)
        {
            Edit().PutBoolean(name, v).Apply();
        }

        public override void SetValue(string name, string v)
        {
            Edit().PutString(name, v).Apply();
        }
    }
}