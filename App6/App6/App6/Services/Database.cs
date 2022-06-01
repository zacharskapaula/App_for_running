using System.IO;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using App6.Models;

namespace App6
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        private static Database database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Time>();
            _database.CreateTableAsync<Distance>();
            _database.CreateTableAsync<Speed>();

        }

        public static Database GetInstance()
        {
            var path = "C:/Users/Paula/Desktop/db ";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if(database == null)
            {
                string dbPath = Path.Combine(path, "mydb.db");
                database = new Database(dbPath);
            }

            return database;
        }

        public Task<List<Time>> GetTrainingAsync()
        {
            return _database.Table<Time>().ToListAsync();
        }

        public Task<int> SaveTrainingAsync(Time time)
        {
            return _database.InsertAsync(time);
        }
    }

}
