using SeDailyXamarin.Controls;
using SeDailyXamarin.PageModels;
using SeDailyXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeDailyXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayListPage : ContentPage
    {
        private PlayListViewModel ViewModel
        {
            get { return BindingContext as PlayListViewModel; }
        }
        public PlayListPage(MenuType item)
        {

            InitializeComponent();

            scrollview.ItemsSource = new PlayListViewModel(item).FeedItems;

            //BindingContext = new PlayListViewModel(item);
          
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