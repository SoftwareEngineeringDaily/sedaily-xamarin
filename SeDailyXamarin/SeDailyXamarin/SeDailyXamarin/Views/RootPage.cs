using SeDailyXamarin.PageModels;
using SeDailyXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SeDailyXamarin.Views
{
    public class RootPage : MasterDetailPage
    {
        public static bool IsUWPDesktop { get; set; }
        Dictionary<MenuType, Page> Pages { get; set; }
        public RootPage()
        {
            if (IsUWPDesktop)
                this.MasterBehavior = MasterBehavior.Popover;

            Pages = new Dictionary<MenuType, Page>();
            Master = new MenuPage(this);
            BindingContext = new BaseViewModel
            {
                Title = "SEDaily",
                Icon = "slideout.png"

            };
            //setup home page
            NavigateAsync(MenuType.About);

            InvalidateMeasure();
        }



        public async Task NavigateAsync(MenuType id)
        {

            if (Detail != null)
            {
                if (IsUWPDesktop || Device.Idiom != TargetIdiom.Tablet)
                    IsPresented = false;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(300);
            }

            Page newPage;
            if (!Pages.ContainsKey(id))
            {

                switch (id)
                {
                    case MenuType.About:
                        Pages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case MenuType.Podcast:
                        Pages.Add(id,  new TabbedPage
                        {
                            Icon = "slideout.png",
                            Children = {

                                       new NavigationPage(new PodcastPage(id){
                                            Title = "All"

                                        }),

                                        new NavigationPage(new PodcastPage(id){
                                            Title = "All"

                                        }),
                            }

                        });
                       // Pages.Add(id, new NavigationPage(new PodcastPage(id)));
                        break;
                    case MenuType.Playlist:
                        Pages.Add(id, new NavigationPage(new PlayListPage(id)));
                        break;
                   

                }
            }

            newPage = Pages[id];
            if (newPage == null)
                return;

            //pop to root for Windows Phone
            if (Detail != null && Device.RuntimePlatform == Device.WinPhone)
            {
                await Detail.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }
    }
}
