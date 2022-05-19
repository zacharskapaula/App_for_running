using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLitePCL;

namespace App6
{
    public class RunningStatistics
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Distance { get; set; }
        public string Speed{ get; set; }
        public string Time  { get; set; }
    }
}
