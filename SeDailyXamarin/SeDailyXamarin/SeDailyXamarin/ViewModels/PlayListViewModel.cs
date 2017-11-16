using Newtonsoft.Json;
using SeDailyXamarin.PageModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SeDailyXamarin.ViewModels
{
    class PlayListViewModel : BaseViewModel
    {
        MenuType item;
        public PlayListViewModel(MenuType item)
        {
            this.item = item;

            switch (item)
            {
                case MenuType.Podcast:
                    Title = "Hear Me";
                    break;
                case MenuType.Playlist:
                    Title = "Play";
                    break;

            }
        }
        private Command loadItemsCommand;
        public Command LoadItemsCommand
        {
            get
            {
                return loadItemsCommand ?? (loadItemsCommand = new Command(async () => await ExecuteLoadItemsCommandAsync()));
            }
        }
        private ObservableCollection<FeedItem> feedItems = new ObservableCollection<FeedItem>();

        public ObservableCollection<FeedItem> FeedItems
        {
            get
            {
                return feedItems;
            }
            set
            {
                feedItems = value;
                OnPropertyChanged();
            }
        }

        private async Task ExecuteLoadItemsCommandAsync()
        {


            IsBusy = true;
            bool error = false;
            try
            {
                HttpClient httpClient = new HttpClient();
                string feed = string.Empty;
                switch (item)
                {
                    case MenuType.Playlist:
                        feed = @"https://software-enginnering-daily-api.herokuapp.com/api/posts";
                        break;
                    case MenuType.Podcast:
                        feed = @"https://software-enginnering-daily-api.herokuapp.com/api/posts";
                        break;
                    case MenuType.Twitter:
                        feed = @"https://feeds.podtrac.com/9dPm65vdpLL1";
                        break;

                }

                string responseString = await httpClient.GetStringAsync(feed);
                FeedItems.Clear();
                var items = await ParseFeed(responseString);
                foreach (var feedItem in items)
                {
                    FeedItems.Add(feedItem);
                }
            }
            catch (Exception e)
            {
                error = true;
                Debug.WriteLine(e.Message);
            }

            if (error)
            {
                ContentPage page = new ContentPage();
                Task result = page.DisplayAlert("Error", $"Unable to load podcast feed.", "OK");
            }

            IsBusy = false;

        }

        private async Task<List<FeedItem>> ParseFeed(string rss)
        {
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<List<FeedItem>>(rss);

            });
        }
        public FeedItem GetFeedItem(string id)
        {
            return FeedItems.FirstOrDefault(i => i.Id == id);
        }
    }
}
