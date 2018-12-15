using NeuroSpeech.UIAtoms;
using System;
using System.Collections.Generic;
using System.Text;
using UIAtomsDemo.Forms.Models;
using System.Threading.Tasks;
using UIAtomsDemo.Forms.Services;
using Xamarin.Forms;
using NeuroSpeech.UIAtoms.DI;

namespace UIAtomsDemo.ViewModels
{
    public class NavigatorListSampleViewModel: BaseViewModel
    {

        public AtomList<Country> Items { get; }
            = new AtomList<Country>();

        #region Property SelectedItems

        private System.Collections.IEnumerable _SelectedItems = null;

        public System.Collections.IEnumerable SelectedItems
        {
            get
            {
                return _SelectedItems;
            }
            set
            {
                SetProperty(ref _SelectedItems, value);
            }
        }
        #endregion



        public AtomCommand<Country> TapCommand { get; private set; }
        public AtomCommand<IEnumerable<Country>> DeleteCommand { get; private set; }

        public NavigatorListSampleViewModel()
        {
            this.TapCommand = new AtomCommand<Country>(async country => await OnTapCommandAsync(country));

            this.DeleteCommand = new AtomCommand<IEnumerable<Country>>(async countries => await OnDeleteCommandAsync(countries));
        }

        private async Task OnDeleteCommandAsync(IEnumerable<Country> countries)
        {
            await DependencyService.Get<INotificationService>().NotifyAsync($"Countries {string.Join(",", countries)} deleted");

            using (Items.BeginEdit())
            {
                foreach (var country in countries)
                {
                    Items.Remove(country);
                }
            }
        }

        private async Task OnTapCommandAsync(Country country)
        {
            await DependencyService.Get<INotificationService>().NotifyAsync($"Country {country.Label} tapped");
        }

        public override Task InitAsync()
        {
            //Items.Replace(await JsonService.Instance.Countries());

            Items.Replace(new Country[] {
                new Country {
                    Label = "India",
                    Value = "IN",
                },
                new Country {
                    Label = "Canada",
                    Value = "CA"
                },
                new Country {
                    Label = "Italy",
                    Value = "IT"
                },
                new Country {
                    Label = "France",
                    Value = "FR"
                },
                new Country {
                    Label = "United States",
                    Value = "US"
                },
                new Country {
                    Label = "United Kingdom",
                    Value = "UK"
                }
            });

            return Task.CompletedTask;
        }

    }
}
