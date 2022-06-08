using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App6.Models
{
    public class Statistic
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string StartTime { get; set; }
        public string TotalTime { get; set; }
        public string TrainingDistance { get; set; }
        public string AverageSpeed { get; set; }


        public Statistic()
        {

        }

        public Statistic(String start, String total, String distance, String speed)
        {
            this.StartTime = start;
            this.TotalTime = total;
            this.TrainingDistance = distance;
            this.AverageSpeed = speed;

        }
        public static Statistic createObject(String[] atributes)
        {
            return new Statistic(atributes[0], atributes[1], atributes[2], atributes[3]);
        }

    }
}
