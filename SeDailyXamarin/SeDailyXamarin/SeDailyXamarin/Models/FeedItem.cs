using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeDailyXamarin.Models
{
    public class FeedItem : INotifyPropertyChanged

    {
        public int Id { get; set; }

        public string Link { get; set; }
        
        public string Description { get; set; }
        
        public string Categories { get; set; }
        
        public string Tag { get; set; }


        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;

            }
        }

        public string SEDailyDefaultImage {
            get
            {
                return "https://softwareengineeringdaily.com/wp-content/uploads/2015/08/sed_logo_updated.png";
            }
        }

        private string mainImage;
        public string MainImage
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(mainImage))
                {
                    return SEDailyDefaultImage;
                }

                string imageDataSourcePattern = @"http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?.(?:jpg|bmp|gif|png";
                Regex regex = new Regex(imageDataSourcePattern, RegexOptions.IgnoreCase);
                MatchCollection matches = regex.Matches(Description);

                if(matches.Count == 0)
                {
                    mainImage = SEDailyDefaultImage;
                } else
                {
                    mainImage = matches[0].Value;
                }

                return mainImage;
            }
        }
        public string CommentCount { get; set; }

        private string publishDate;
        public string PublishDate
        {
            get { return publishDate; }
            set
            {
                DateTime time;
                if (DateTime.TryParse(value, out time))
                    publishDate = time.ToLocalTime().ToString("D");
                else
                    publishDate = value;
            }
        }

        private decimal progress = 0.0M;
        public decimal Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyChanged("Progress"); }
        }

        public string Mp3Url { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;
       
        /// <summary>
        /// TODO: Document
        /// </summary>
        /// <param name="args"></param>
        private void OnPropertyChanged(string args)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(args));
        }
       
    }
}
