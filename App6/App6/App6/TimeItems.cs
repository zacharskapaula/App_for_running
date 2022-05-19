using System;
using System.Collections.Generic;
using System.Text;

namespace App6
{
    public class TimeItems
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        public TimeSpan TotalTime
        {
            get => Stop - Start;
        }
    }
}
