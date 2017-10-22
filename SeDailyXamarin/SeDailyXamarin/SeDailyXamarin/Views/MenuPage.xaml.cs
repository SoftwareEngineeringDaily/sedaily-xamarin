using SeDailyXamarin.Models;
using SeDailyXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeDailyXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        RootPage root;
        List<HomeMenuItem> menuItems;
        public MenuPage(RootPage root)
        {

            this.root = root;
            InitializeComponent();
            if (!App.IsWindows10)
            {
                BackgroundColor = Color.FromHex("#03A9F4");
                ListViewMenu.BackgroundColor = Color.FromHex("#F5F5F5");
            }
            BindingContext = new BaseViewModel
            {
                Title = "SeDaily",
                Icon = "slideout.png"

            };

            ListViewMenu.ItemsSource = menuItems = new List<HomeMenuItem>
                {

                    new HomeMenuItem { Title = "About", MenuType = MenuType.About},
                    new HomeMenuItem { Title = "Podcast", MenuType = MenuType.Podcast },
                    new HomeMenuItem { Title = "Playlist", MenuType = MenuType.Playlist},
                    new HomeMenuItem { Title = "Slack", MenuType = MenuType.Slack},
                    new HomeMenuItem { Title = "Store", MenuType = MenuType.Store},
                    new HomeMenuItem { Title = "Profile", MenuType = MenuType.Profile },


                };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (ListViewMenu.SelectedItem == null)
                    return;

                await this.root.NavigateAsync(((HomeMenuItem)e.SelectedItem).MenuType);
            };
        }
    }
}