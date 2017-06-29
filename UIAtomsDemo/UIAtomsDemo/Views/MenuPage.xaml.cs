using NeuroSpeech.UIAtoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIAtomsDemo.Forms.Views;
using UIAtomsDemo.Views;
using Xamarin.Forms;

namespace UIAtomsDemo
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{

            InitializeComponent();


            var items = new MenuItem[]{
                new MenuItem{ Category = "Home", Title = "Home", Type = typeof(HomePage) },
                //new MenuItem{ Category ="Home", Title = "Album", Type = typeof(AlbumChooserDemoPage) },
                new MenuItem{ Category = "Form",  Title = "Forms", Type = typeof(FormDemoPage) },
                new MenuItem{ Category = "Form",  Title = "Navigator List", Type = typeof(NavigatorListSample) },
                new MenuItem{ Category = "Form",  Title = "Image", Type = typeof(ImagePage) },
                new MenuItem { Category = "Form",  Title = "ComboBox", Type=typeof(ComboBoxSample) },
                new MenuItem { Category = "Calendar",  Title= "Calendar", Type=typeof(CalendarPage)}
            };

            this.menuList.ItemsSource = items.GroupBy(x=>x.Category).ToList();

            this.menuList.TapCommand = new AtomCommand<MenuItem>( (item) => {
                RootPage.Navigate(item.Type);
                return Task.CompletedTask;
            });
        }



        public class MenuItem
        {

            public string Category { get; set; }

            public string Title
            {
                get;
                set;
            }

            public Type Type
            {
                get;
                set;
            }
        }
    }
}

