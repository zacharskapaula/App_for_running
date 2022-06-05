using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App6.Models
{
    [Table("Distance")]
    public class DistanceT
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public double StartPointLat { get; set; }
        public double StartPointLong { get; set; }
        public double StopPointLat { get; set; }
        public double StopPointLong { get; set; }
        public double TrainingDistance { get; set; }


        public DistanceT()
        {

        }

        public DistanceT(Double startpointlat, Double startpointlong, Double stoppointlat, Double  stoppointlong, Double trainingdistance)
        {
            this.StartPointLat = startpointlat;
            this.StartPointLat = startpointlat;
            this.StopPointLat = stoppointlat;
            this.StopPointLong = stoppointlong;
            this.TrainingDistance = trainingdistance;

        }
        public static DistanceT createObject(double[] atributes)
        {
            return new DistanceT(atributes[0], atributes[1], atributes[2], atributes[3], atributes[4]);
        }

    }
}
