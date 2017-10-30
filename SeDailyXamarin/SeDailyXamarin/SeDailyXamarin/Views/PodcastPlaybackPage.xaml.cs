using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;
using Plugin.Share;
using SeDailyXamarin.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeDailyXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PodcastPlaybackPage : ContentPage
    {
        private IPlaybackController PlaybackController => CrossMediaManager.Current.PlaybackController;

        public PodcastPlaybackPage(FeedItem item)
        {
            InitializeComponent();

            BindingContext = item;


            var file = "https://audioboom.com/posts/5766044-follow-up-305.mp3?source=rss&amp;stitched=1";
            MediaFile mediaFile = new MediaFile(item.Mp3, MediaFileType.Audio);
           
            CrossMediaManager.Current.Play(mediaFile);
           
            CrossMediaManager.Current.PlayingChanged += (sender, e) =>
            {
               if(e.Duration.Milliseconds > 0)
               {
                    PlayBackSlider.Maximum = e.Duration.TotalMilliseconds;
               }
                
                PlayBackSlider.Value = e.Position.TotalMilliseconds;
                Elapsed.Text = GetFormattedTime(e.Position.TotalMilliseconds);
                Remaining.Text = GetFormattedTime(e.Duration.TotalMilliseconds - e.Position.TotalMilliseconds);
                debug1.Text = "maximum: " + GetFormattedTime(PlayBackSlider.Maximum);
                debug2.Text = "value: " + GetFormattedTime(PlayBackSlider.Value);
            };
            CrossMediaManager.Current.MediaFinished += (sender, e) =>
            {
                PlayBackSlider.Value = PlayBackSlider.Maximum;
                debug1.Text = "maximum: " + PlayBackSlider.Maximum.ToString();
                debug2.Text = "value: " + PlayBackSlider.Value.ToString();
            };

            webView.Source = new HtmlWebViewSource
            {
                Html = "Comments"
            };

            ToolbarItem share = new ToolbarItem
            {
                Command = new Command(() =>
                {
                    CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
                    {
                        Text = $"Listening to SEDaily {item.Title} {item.Link}",
                        Title = "Share",
                        Url = item.Link

                    });
                })
            };

            ToolbarItems.Add(share);

            play.Clicked += (sender, args) => PlaybackController.Play();
            pause.Clicked += (sender, args) => PlaybackController.Pause();
            stop.Clicked += (sender, args) => PlaybackController.Stop();
            forward.Clicked += (sender, args) =>
            {
                PlayBackSlider.Value += 30000;
                PlaybackController.SeekTo(PlayBackSlider.Value/1000);
            };
            rewind.Clicked += (sender, args) =>
            {
                if(PlayBackSlider.Value <= 10000)
                {
                    PlayBackSlider.Value = 0;

                }
                else
                {
                    PlayBackSlider.Value -= 10000;
                   
                }
                PlaybackController.SeekTo(PlayBackSlider.Value / 1000);
            };
           // PlayBackSlider.PropertyChanged += (sender, args) => PlaybackController.SeekTo(PlayBackSlider.Value);


        }

        public string GetFormattedTime(double value)
        {
            var span = TimeSpan.FromMilliseconds(value);
            if (span.Hours > 0)
            {
                return string.Format("{0}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds);
            }
            else
            {
                return string.Format("{0}:{1:00}", (int)span.Minutes, span.Seconds);
            }
        }
    }
}