using SeDailyXamarin.Models;
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
                        feed = @"https://software-enginnering-daily-api.herokuapp.com/api";
                        break;
                    case MenuType.Twitter:
                        feed = @"https://feeds.podtrac.com/9dPm65vdpLL1";
                        break;
                }
                Debug.WriteLine("The feed value is " + feed);
                string responseString = await httpClient.GetStringAsync(feed);

                Debug.WriteLine("The responsestring is " + responseString);
                FeedItems.Clear();
                var items = await ParseFeed(responseString);
                foreach (var feedItem in items)
                {
                    FeedItems.Add(feedItem);
                }
            }
            catch
            {
                error = true;
            }

            if (error)
            {
                ContentPage page = new ContentPage();
                Task result = page.DisplayAlert("Error", "Unable to load podcast feed.", "OK");

            }


            IsBusy = false;

        }

        private async Task<List<FeedItem>> ParseFeed(string rss)
        {
            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(rss);
                var id = 0;
                return (from item in xdoc.Descendants("item")
                        let enclosure = item.Element("enclosure")
                        where enclosure != null
                        select new FeedItem
                        {
                            Title = (string)item.Element("title"),
                            Description = (string)item.Element("description"),
                            Link = (string)item.Element("link"),
                            PublishDate = (string)item.Element("pubDate"),
                            Categories = (string)item.Element("category"),
                            Mp3Url = (string)enclosure.Attribute("url"),
                            Id = id++
                        }).ToList();
            });
        }
        public FeedItem GetFeedItem(int id)
        {
            return FeedItems.FirstOrDefault(i => i.Id == id);
        }
    }
}
