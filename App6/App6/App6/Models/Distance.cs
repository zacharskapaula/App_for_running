using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App6.Models
{
    [Table("Distance")]
    public class Distance
    {
        [PrimaryKey]
        public int ID { get; set; }
        
        public string OnRoutePoint { get; set; }
        public string TrainingDistance { get; set; }


        public Distance()
        {

        }

        public Distance(String onroutepoint, String trainingdistance)
        {
            
            this.OnRoutePoint = onroutepoint;
            this.TrainingDistance = trainingdistance;

        }
        public static Distance createObject(string[] atributes)
        {
            return new Distance(atributes[0], atributes[1]);
        }

    }
}
