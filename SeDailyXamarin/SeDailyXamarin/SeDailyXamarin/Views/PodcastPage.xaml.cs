using SeDailyXamarin.PageModels;
using SeDailyXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeDailyXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PodcastPage : ContentPage
    {
        private PodcastViewModel ViewModel
        {
            get { return BindingContext as PodcastViewModel; }
        }

        public PodcastPage(MenuType item)
        {
            InitializeComponent();

            BindingContext = new PodcastViewModel(item);


            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                this.Navigation.PushAsync(new PodcastPlaybackPage
                   (listView.SelectedItem as FeedItem));
                listView.SelectedItem = null;
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //FeaturedImageView.WidthRequestProperty = this.Width;
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
                return;

            ViewModel.LoadItemsCommand.Execute(null);
        }


    }
}