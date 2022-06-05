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
        /*******************************************************************************/

        public List<TimesT> execute()
        {
            return _database.ExecuteScalarAsync<List<TimesT>>("select * from Time").Result;
        }

        

        public Task<int> SaveTimeAsync(TimesT time)
        {
            return _database.InsertAsync(time);
        }



        /******************************************************************************/
        public Task<int> SaveDistanceAsync(DistanceT time)
        {
            return _database.InsertAsync(time);
        }

        public Task<List<DistanceT>> GetDistanceAsync()
        {
            return _database.Table<DistanceT>().ToListAsync();
        }


        /******************************************************************************/

        public Task<int> SaveSpeedAsync(SpeedT time)
        {
            return _database.InsertAsync(time);
        }

        public Task<List<SpeedT>> GetSpeedAsync()
        {
            return _database.Table<SpeedT>().ToListAsync();
        }
    }

}
