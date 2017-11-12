using SeDailyXamarin.PageModels;
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
                Title = "SE Daily",
                Icon = "slideout.png"

            };

            ListViewMenu.ItemsSource = menuItems = new List<HomeMenuItem>
                {

                    new HomeMenuItem { Title = "About", MenuType = MenuType.About, Icon ="about_icon.png"},
                    new HomeMenuItem { Title = "Podcast", MenuType = MenuType.Podcast,Icon = "podcast_icon.png"  },
                    new HomeMenuItem { Title = "Playlist", MenuType = MenuType.Playlist, Icon = "playlist_icon.png"},
                    new HomeMenuItem { Title = "Slack", MenuType = MenuType.Slack, Icon = "slack_icon.png"},
                    new HomeMenuItem { Title = "Twitter", MenuType = MenuType.Twitter, Icon = "twitter_icon.png"},
                    new HomeMenuItem { Title = "Store", MenuType = MenuType.Store, Icon = "store_icon"},
                    new HomeMenuItem { Title = "Profile", MenuType = MenuType.Profile, Icon = "profile_generic.png" },


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