using System;
using System.Collections.Generic;
using System.Text;


namespace BO
{
    public class Location
    {
        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public Location()
        {
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return ("Longitude:     " + Longitude + "\n" +
                    "Latitude:      " + Latitude + "\n");
        }
    }
}

