using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App6.Models
{
    [Table("Speed")]
    public class Speed
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string AverageSpeed { get; set; }
        public string MininumSpeed { get; set; }
        public string MaxSpeed { get; set; }


        public Speed()
        {

        }

        public Speed(String mininumspeed, String averagespeed, String maxspeed)
        {
            this.MininumSpeed = mininumspeed;
            this.AverageSpeed = averagespeed;
            this.MaxSpeed = maxspeed;

        }

        public static Speed createObject(string[] atributes)
        {
            return new Speed(atributes[0], atributes[1], atributes[2]);
        }
    }
}
