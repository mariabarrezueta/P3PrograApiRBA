using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using P3PrograApiRBA.Models;

namespace P3PrograApiRBA.Services
{
    public class DatabaseServiceRBA
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseServiceRBA()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "newsdb.db3");
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<NewsRBA>().Wait();
        }

        public Task<List<NewsRBA>> GetSavedNewsAsync()
        {
            return _database.Table<NewsRBA>().ToListAsync();
        }

        public Task<int> SaveNewsAsync(NewsRBA news)
        {
            return _database.InsertAsync(news);
        }
    }
}


