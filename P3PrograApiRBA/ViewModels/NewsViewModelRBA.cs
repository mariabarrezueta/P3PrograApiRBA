using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using P3PrograApiRBA.Models;
using P3PrograApiRBA.Services;

namespace P3PrograApiRBA.ViewModels
{
    public class NewsViewModelRBA : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<NewsRBA> newsList;
        public ObservableCollection<NewsRBA> NewsList
        {
            get { return newsList; }
            set
            {
                if (newsList != value)
                {
                    newsList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private NewsRBA selectedNews;
        public NewsRBA SelectedNews
        {
            get { return selectedNews; }
            set
            {
                if (selectedNews != value)
                {
                    selectedNews = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public NewsServiceRBA NewsService { get; set; }
        public DatabaseServiceRBA DatabaseService { get; set; }

        public Command SaveSelectedNewsCommand { get; }

        public NewsViewModelRBA()
        {
            NewsList = new ObservableCollection<NewsRBA>();
            NewsService = new NewsServiceRBA();
            DatabaseService = new DatabaseServiceRBA();
            SaveSelectedNewsCommand = new Command(async () => await SaveSelectedNewsAsync());
            LoadNews();
        }

        private async void LoadNews()
        {
            var news = await NewsService.GetNews();
            foreach (var item in news)
            {
                NewsList.Add(item);
            }
        }

        public async Task SaveSelectedNewsAsync()
        {
            if (SelectedNews != null)
            {
                await DatabaseService.SaveNewsAsync(SelectedNews);
                SelectedNews = null; // Deselect after saving
            }
        }
    }
}

