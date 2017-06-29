using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomPopupPage: Rg.Plugins.Popup.Pages.PopupPage
    {
        public AtomPopupPage()
        {
            this.ControlTemplate = new Xamarin.Forms.ControlTemplate(typeof(AtomPopupPageTemplate));
        }
    }

    public class AtomPopupPageTemplate : Grid {
        public AtomPopupPageTemplate()
        {

            Padding = new Thickness(5);


            Grid innerGrid = new Grid();
            innerGrid.Padding = new Thickness(10);

            innerGrid.Effects.Add(new AtomRoundBorderEffect
            {
                BackgroundColor = Color.White,
                CornerRadius = 5
            });



            RowDefinitions.Add(new RowDefinition { });
            RowDefinitions.Add(new RowDefinition {
                Height = GridLength.Auto
            });
            RowDefinitions.Add(new RowDefinition {
                Height = new GridLength(5, GridUnitType.Star)
            });


            BoxView bv = new BoxView();
            bv.BackgroundColor = Color.Transparent;
            bv.MinimumHeightRequest = 30;
            Children.Add(bv);


            AtomImage cancelImage = new AtomImage {
                Source = AtomStockImages.DeleteImageUrl,
                WidthRequest = 30,
                HeightRequest = 30
            };

            cancelImage.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new AtomCommand(async ()=> {
                    try
                    {
                        await DependencyService.Get<IAppNavigator>().PopModalAsync(true);
                    }
                    catch (Exception ex){
                        UIAtomsApplication.Instance?.LogException?.Invoke(ex);
                        System.Diagnostics.Debug.Fail(ex.Message, ex.ToString());
                    }
                })
            });

            Label title = new Label {
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(5)
            };

            title.SetBinding(Label.TextProperty, new TemplateBinding {
                Path = nameof(AtomPopupPage.Title)
            });

            Grid.SetColumn(cancelImage, 1);

            innerGrid.ColumnDefinitions.Add(new ColumnDefinition { });
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition {
                Width = GridLength.Auto
            });

            innerGrid.AddRowItems(GridLength.Auto, title, cancelImage);

            innerGrid.AddRowItem(new ContentPresenter { });

            Grid.SetRow(innerGrid, 1);
            Children.Add(innerGrid);




        }

    }
}
