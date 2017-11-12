using SeDailyXamarin.PageModels;
using SeDailyXamarin.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SeDailyXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
             MainPage = new RootPage();
            //SetMainPage();

        }

        public static bool IsWindows10 { get; set; }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },

                }
            };
        }
    }
}
