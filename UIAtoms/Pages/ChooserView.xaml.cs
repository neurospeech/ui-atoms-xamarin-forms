using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Pages
{
	public partial class ChooserView : ContentView
	{
        public ChooserView(AtomChooser chooser)
        {
            InitializeComponent();
            this.Chooser = chooser;

            DoneCommand = new AtomCommand(async () => await OnDoneCommandAsync());
            Init();
        }

        private async Task OnDoneCommandAsync()
        {

            var selectedItems = listView.SelectedItems;

            if (Chooser.EnableSelection)
            {



                var values = listView.SelectedItems.Cast<object>()
                    .Select(x => x?.GetPropertyValue(Chooser.ValuePath))
                    .Where(x => x != null);

                AtomList<object> sitems = Chooser.SelectedItems as AtomList<object>;

                if (Chooser.AllowMultipleSelection)
                {
                    sitems.Replace(listView.SelectedItems.Cast<object>());
                    Chooser.Value = string.Join(Chooser.ValueSeparator, values.Select(x => x.ToString()));
                }
                else
                {
                    var first = values.FirstOrDefault();
                    sitems.Replace(listView.SelectedItems.Cast<object>().Take(1));
                    Chooser.Value = first;
                }

            }

            Chooser.TapCommand?.Execute(selectedItems);

            Chooser = null;
            if (Popup) {
                await DependencyService.Get<INavigation>().PopModalAsync();
            } else
            {
                await DependencyService.Get<INavigation>().PopAsync();
            }
        }

        public bool Popup { get; set; }

        public AtomChooser Chooser { get; private set; }
        public AtomCommand DoneCommand { get; private set; }

        private AtomListView listView = null;

        private bool isSearching = false;

        private IDisposable BeginEdit() {
            isSearching = true;
            return new AtomDisposableAction(()=> {
                isSearching = false;
            });
        }

        private void Init()
        {

            cancelButton.Command = new AtomCommand(async () => {
                await DependencyService.Get<INavigation>().PopAsync();
            });

            if (Chooser.AllowMultipleSelection)
            {
                // take navigator list...
                listView = new AtomNavigatorListView();
                listView.AutoScrollOnSelection = false;
                /*this.ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Done",
                    Command = DoneCommand
                });*/


                okButton.Command = DoneCommand;

            }
            else
            {
                listView = new AtomListView();
                listView.TapCommand = DoneCommand;


                okButton.IsVisible = false;
                Grid.SetColumnSpan(cancelButton, 2);
            }

            listView.ItemsChanged += ListView_ItemsChanged;

            Grid.SetColumnSpan(listView, 2);

            //this.Title = Chooser.PromptText;

            listView.ItemTemplate = Chooser.ItemTemplate;
            listView.GroupDisplayBinding = Chooser.GroupDisplayBinding;
            listView.GroupHeaderTemplate = Chooser.GroupHeaderTemplate;
            listView.IsGroupingEnabled = Chooser.IsGroupingEnabled;
            listView.HasUnevenRows = Chooser.HasUnevenRows;
            if (Chooser.RowHeight != -1) {
                listView.RowHeight = Chooser.RowHeight;
            }

            listView.SelectionChanged += (s, e) => {
                if (isSearching)
                    return;
                searchBar?.Unfocus();
            };
            //listView.ItemsSource = Chooser.ItemsSource;

            listView.SetBinding(AtomListView.ItemsSourceProperty, new Binding()
            {
                Source = Chooser,
                Path = nameof(AtomChooser.ItemsSource)
            });

            Grid.SetRow(listView, 2);

            searchBar.IsVisible = Chooser.ShowSearch;

            addButton.IsVisible = Chooser.AddNewCommand != null;
            addButton.Command = Chooser.AddNewCommand;
            addButton.Text = Chooser.AddNewLabel;

            if (searchBar.IsVisible)
            {

                UIAtomsApplication.Instance.SetTimeout(() => {
                    searchBar.Focus();
                }, TimeSpan.FromMilliseconds(100));

                searchBar.TextChanged += (s, e) => {

                    using (BeginEdit())
                    {

                        string st = e.NewTextValue;

                        Chooser.SearchText = st;

                        string fp = Chooser.FilterPath;
                        if (string.IsNullOrWhiteSpace(fp))
                        {
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(st))
                        {
                            listView.SetBinding(AtomListView.ItemsSourceProperty, new Binding()
                            {
                                Source = Chooser,
                                Path = nameof(AtomChooser.ItemsSource)
                            });

                        }
                        else
                        {
                            var list = Chooser.ItemsSource.Cast<object>();

                            var glist = list as IEnumerable<IGrouping<object, object>>;
                            if (glist != null)
                            {
                                // grouped list filtering...

                                listView.ItemsSource = glist.Where(x => x.Any(i => Filter(i, st, fp) || i == listView.SelectedItem));

                            }
                            else
                            {
                                listView.ItemsSource = list.Where(i => Filter(i, st, fp) || i == listView.SelectedItem);
                            }


                        }
                    }


                };
            }

            theGrid.Children.Add(listView);
        }

        private void ListView_ItemsChanged(object sender, EventArgs e)
        {
            UIAtomsApplication.Instance.TriggerOnce(() => {

                using (BeginEdit())
                {

                    if (Chooser.Value == null)
                    {
                        return;
                    }

                    string valuePath = Chooser.ValuePath;
                    if (string.IsNullOrWhiteSpace(valuePath))
                        return;

                    if (listView.AllowMultipleSelection)
                    {
                        var values = Chooser.Value.ToString()
                            .Split(Chooser.ValueSeparator.Trim().ToCharArray())
                            .Select(x => x.Trim())
                            .ToList();
                        foreach (var item in listView.ItemsSource)
                        {
                            var v = item.GetPropertyValue(valuePath)?.ToString();
                            if (v == null)
                                continue;

                            var vf = values.FirstOrDefault(x => string.Equals(v, x, StringComparison.OrdinalIgnoreCase));
                            if (vf != null)
                            {
                                listView.SelectedItems.Add(item);

                            }
                        }
                    }
                    else
                    {
                        var value = Chooser.Value;
                        foreach (var item in listView.ItemsSource)
                        {
                            var v = item.GetPropertyValue(valuePath);
                            if (value == v || value.Equals(v))
                            {
                                listView.SelectedItems.Clear();
                                listView.SelectedItems.Add(item);
                                break;
                            }
                        }
                    }
                }

            });
        }

        private bool Filter(object i, string searchText, string filterProperties)
        {
            var properties = filterProperties.Split(',', ';').Select(x => x.Trim()).ToList();

            foreach (var prop in properties)
            {
                var value = i.GetPropertyValue(prop);
                if (value == null)
                    continue;

                // we always want to show selection...
                if (i == listView.SelectedItem)
                    return true;
                if (value.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1)
                    return true;
            }
            return false;
        }
    }
}
