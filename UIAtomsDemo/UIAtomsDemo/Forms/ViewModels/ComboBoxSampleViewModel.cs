using NeuroSpeech.UIAtoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAtomsDemo.Forms.Models;
using UIAtomsDemo.Forms.Services;

namespace UIAtomsDemo.Forms.ViewModels
{
    public class ComboBoxSampleViewModel: BaseViewModel
    {

        public AtomList<Country> Items { get; }
            = new AtomList<Country>();


        #region Property CountryCode

        private string _CountryCode = "US";

        public string CountryCode
        {
            get
            {
                return _CountryCode;
            }
            set
            {
                SetProperty(ref _CountryCode, value);
            }
        }
        #endregion





        public override async Task InitAsync()
        {
            //await Task.Delay(5000);
            var r = await JsonService.Instance.Countries();
            //Items.AddRange(r);

            Items.Replace(r);
        }

    }

    
}
