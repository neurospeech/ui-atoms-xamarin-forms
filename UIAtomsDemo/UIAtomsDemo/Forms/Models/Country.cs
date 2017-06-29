using NeuroSpeech.UIAtoms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIAtomsDemo.Forms.Models
{
    public class Country: AtomModel
    {

        #region Property Label

        private string _Label = "";

        [JsonProperty("label")]
        public string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                SetProperty(ref _Label, value);
            }
        }
        #endregion

        #region Property Value

        private string _Value = "";

        [JsonProperty("value")]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                SetProperty(ref _Value, value);
            }
        }
        #endregion

        public override string ToString()
        {
            return _Label;
        }

    }
}
