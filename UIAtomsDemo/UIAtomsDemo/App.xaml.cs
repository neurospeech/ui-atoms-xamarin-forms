using NeuroSpeech.UIAtoms;
using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAtomsDemo.Views;
using Xamarin.Forms;

namespace UIAtomsDemo
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent ();

            


            Device.BeginInvokeOnMainThread(async () => {

                await AtomCoachMarks.Preferences.ClearAsync();

                await UIAtomsApplication.Instance.InitAsync(
                    new UIAtomsConfig(null)
                    {
                        BaseUrl = "https://secure.800casting.com"
                    });
            });

            // The root page of your application
            MainPage = new RootPage();

        }
    }
}
