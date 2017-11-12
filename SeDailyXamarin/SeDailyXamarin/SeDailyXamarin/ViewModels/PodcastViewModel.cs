using SeDailyXamarin.PageModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using SeDailyXamarin.Services;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SeDailyXamarin.ViewModels
{
    public class PodcastViewModel : BaseViewModel
    {
        MenuType item;
        public PodcastViewModel(MenuType item)
        {
            this.item = item;

            switch (item)
            {
                case MenuType.Podcast:
                    Title = "Hear Me";
                    break;
                
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

        private FeedItem selectedFeedItem;
        public FeedItem SelectedItem
        {
            get
            {
                return selectedFeedItem;
            }
            set
            {
                selectedFeedItem = value;
                OnPropertyChanged();
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

        private async Task ExecuteLoadItemsCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            bool error = false;
            try
            {
                HttpClient httpClient = new HttpClient();
                string feed = string.Empty;
                switch (item)
                {
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
            catch(Exception e)
            {
                error = true;
                ContentPage page = new ContentPage();
                Task result = page.DisplayAlert("Error", $"Unable to load podcast feed.{e.Message}", "OK");
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
