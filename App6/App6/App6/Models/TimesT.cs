using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLitePCL;

namespace App6.Models
{
    [Table("Time")]
    public class TimesT
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string TotalTime { get; set; }


        public TimesT(String starttime, String stoptime, String totaltime)
        {
            this.StartTime = starttime;
            this.StopTime = stoptime;
            this.TotalTime = totaltime;

        }

        public static TimesT createObject(string[] atributes)
        {
            return new TimesT(atributes[0], atributes[1], atributes[2]);
        }

        public TimesT()
        {
            

        }
    }
}
