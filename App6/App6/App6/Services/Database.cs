using System.IO;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using App6.Models;
using System.Linq;
using System;
using Xamarin.Forms;
using Xamarin.Essentials;


namespace App6
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        private static Database database;

        private Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TimesT>();
            _database.CreateTableAsync<DistanceT>();
            _database.CreateTableAsync<SpeedT>();
            _database.CreateTableAsync<Statistic>();

        }

        public static Database GetInstance()
        {
            /*var path = "/storage/emulated/0/Android/data/com.companyname.app6 ";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            */
            if(database == null)
            {
                //string dbPath = Path.Combine(path, "runningDatabase.db");
                string pathtel = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "runningDatabase.db");
                database = new Database(pathtel);
            }

            return database;
        }
        /****************************** TIME **********************************************/

        public List<TimesT> execute()
        {
            return _database.ExecuteScalarAsync<List<TimesT>>("select * from Time").Result;
        }

        public Task<int> SaveTimeAsync(TimesT time)
        {
            return _database.InsertAsync(time);
        }

        public Task<List<TimesT>> GetTimeAsync()
        {
            return _database.Table<TimesT>().ToListAsync();
        }

        public Task<int> DelateAllAsyncTime()
        {
            return _database.DeleteAllAsync<TimesT>();
        }
        

        /******************************* DISTANCE *********************************************/
        public Task<int> SaveDistanceAsync(DistanceT distane)
        {
            return _database.InsertAsync(distane);
        }

        public Task<List<DistanceT>> GetDistanceAsync()
        {
            return _database.Table<DistanceT>().ToListAsync();
        }

        public Task<int> DelateAllAsyncDistance()
        {
            return _database.DeleteAllAsync<DistanceT>();
        }

        /********************************* SPEED *********************************************/

        public Task<int> SaveSpeedAsync(SpeedT time)
        {
            return _database.InsertAsync(time);
        }

        public Task<List<SpeedT>> GetSpeedAsync()
        {
            return _database.Table<SpeedT>().ToListAsync();
        }

        public Task<int> DelateAllAsyncSpeed()
        {
            return _database.DeleteAllAsync<SpeedT>();
        }

        /********************************* STATISTIC *********************************************/

        public Task<int> SaveStatisticAsync(Statistic statistic)
        {
            return _database.InsertAsync(statistic);
        }

        public Task<List<Statistic>> GetStatisticAsync()
        {
            return _database.Table<Statistic>().ToListAsync();
        }

        public Task<int> DelateAllAsyncStatistic()
        {
            return _database.DeleteAllAsync<Statistic>();
        }

       
    }

}
