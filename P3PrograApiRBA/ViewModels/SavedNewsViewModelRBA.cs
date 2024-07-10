using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using P3PrograApiRBA.Models;
using P3PrograApiRBA.Services;

namespace P3PrograApiRBA.ViewModels
{
    public class SavedNewsViewModelRBA : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<NewsRBA> savedNewsList;
        public ObservableCollection<NewsRBA> SavedNewsList
        {
            get { return savedNewsList; }
            set
            {
                if (savedNewsList != value)
                {
                    savedNewsList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DatabaseServiceRBA DatabaseService { get; set; }

        public SavedNewsViewModelRBA()
        {
            SavedNewsList = new ObservableCollection<NewsRBA>();
            DatabaseService = new DatabaseServiceRBA();
            LoadSavedNews();
        }

        private async void LoadSavedNews()
        {
            var news = await DatabaseService.GetSavedNewsAsync();
            foreach (var item in news)
            {
                SavedNewsList.Add(item);
            }
        }
    }
}




