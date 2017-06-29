using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomDataForm: ContentView
    {

        public static readonly BindableProperty SearchProperty =
            BindableProperty.Create<AtomDataForm, string>(
                x => x.Search,
                null,
                BindingMode.Default,
                (y, x) => x is string,
                (x, y, z) => { },
                (x, y, z) => { },
                (y, x) => x);


        public string Search {
            get {
                return (string)GetValue(SearchProperty);
            }
            set {
                SetValue(SearchProperty, value);
            }

        }


        public static readonly BindableProperty PropertyListProperty =
            BindableProperty.Create<AtomDataForm, PropertyListViewModel>(
                x => x.PropertyList,
                null,
                BindingMode.Default,
                (y, x) => x is PropertyListViewModel,
                (x, y, z) => { },
                (x, y, z) => { },
                (y, x) => x);


        public PropertyListViewModel PropertyList
        {
            get
            {
                return (PropertyListViewModel)GetValue(PropertyListProperty);
            }
            set
            {
                SetValue(PropertyListProperty, value);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        protected ListView ListView { get; set; }

        protected SearchBar SearchBar { get; set; }

 

        /// <summary>
        /// 
        /// </summary>
        public AtomDataForm()
        {
            this.ListView = new ExListView();

            this.SearchBar = new SearchBar();

            this.SearchBar.SetBinding(SearchBar.TextProperty, new Binding() { Path = "PropertyList.Search", Source = this, Mode = BindingMode.TwoWay });

            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() );

            Grid.SetRow(ListView, 1);


            grid.Children.Add(SearchBar);
            grid.Children.Add(ListView);

            this.ListView.VerticalOptions = LayoutOptions.FillAndExpand;
            this.ListView.HasUnevenRows = true;
            this.ListView.GroupDisplayBinding = new Binding {
                Path = "GroupName"
            };
            this.ListView.IsGroupingEnabled = true;
            this.ListView.SetBinding(ListView.ItemsSourceProperty, new Binding {
                Source = this,
                Path = "PropertyList.Items",
                Mode = BindingMode.OneWay
            });
            
            
            this.Content = grid;

            this.VerticalOptions = LayoutOptions.Fill;

            OnBindingContextChanged();

        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                PropertyList = new PropertyListViewModel(BindingContext);
            }
            else {
                if (PropertyList == null)
                    return;
                PropertyList = null;
            }
        }




    }

    public class ExListView : ListView {

        public ExListView() : base(ListViewCachingStrategy.RetainElement)
        {
            HasUnevenRows = true;
            //IsGroupingEnabled = true;

            this.VerticalOptions = LayoutOptions.FillAndExpand;
            //DataTemplate dt = new DataTemplate(typeof(FormItemTemplate));
            //dt.SetBinding(View.BindingContextProperty, new Binding { });
            this.ItemTemplate = new DataTemplate(typeof(FormItemTemplate));
        }


    }

    public class FormItemTemplate : ViewCell {

        public FormItemTemplate()
        {
            Debug.WriteLine("Form Item Template Created successfully...");
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            PropertyBinding pb = BindingContext as PropertyBinding;
            if (pb == null)
            {
                this.View = new Label
                {
                    Text = "Loading..."
                };
            }
            else {
                this.View = pb.FormField.CreateView(pb, pb.Property);
                this.Height = this.View.HeightRequest;
            }
        }

    }

}
