using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLitePCL;

namespace App6.Models
{
    [Table("Time")]
    public class Time
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string TotalTime { get; set; }


        public Time(String starttime, String stoptime, String totaltime)
        {
            this.StartTime = starttime;
            this.StopTime = stoptime;
            this.TotalTime = totaltime;

        }

        public static Time createObject(string[] atributes)
        {
            return new Time(atributes[0], atributes[1], atributes[2]);
        }
    }
}
