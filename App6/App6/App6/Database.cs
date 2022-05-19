using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace App6
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<RunningStatistics>();
        }

        public Task<List<RunningStatistics>> GetUsersDataAsync()
        {
            return _database.Table<RunningStatistics>().ToListAsync();
        }

        public Task<int> SaveUsersDataAsync(RunningStatistics runningStatistics)
        {
            return _database.InsertAsync(runningStatistics);
        }
    }

}
