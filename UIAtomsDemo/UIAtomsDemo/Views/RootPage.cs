using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UIAtomsDemo.Views
{
    public class RootPage : MasterDetailPage
    {

        private static RootPage Current { get; set; }
        public static NavigationPage CurrentPage
        {
            get
            {
                return (NavigationPage)Current.Detail;
            }
        }




        public RootPage()
        {
            Current = this;



            Master = new MenuPage();

            Navigate(typeof(HomePage));

            InvalidateMeasure();

        }


        private static Dictionary<Type, NavigationPage> cache = new Dictionary<Type, NavigationPage>();

        public static void Navigate(Type type)
        {
            Current.Detail = new ContentPage { };

            if (Device.Idiom != TargetIdiom.Tablet)
            {
                Current.IsPresented = false;
            }


            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {

                    await Task.Delay(1000);

                    NavigationPage page = null;
                    if (!cache.TryGetValue(type, out page))
                    {
                        //ContentPage cp = (ContentPage)Activator.CreateInstance(type);

                        IAppNavigator navigator = DependencyService.Get<IAppNavigator>(DependencyFetchTarget.GlobalInstance);
                        var p = await navigator.NewPage(type);
                        page = new NavigationPage(p);
                        cache[type] = page;
                    }
                    Current.Detail = page;

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            });
        }

    }
}
