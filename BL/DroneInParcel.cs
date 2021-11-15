using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class DroneInParcel
        {
            public DroneInParcel(int id, double battery, Location myLocation)
            {
                Id = id;
                Battery = battery;
                MyLocation = myLocation;
            }

            public int Id { get; set; }
            public double Battery { get; set; }
            public Location MyLocation { get; set; }

            public override string ToString()
            {
                return ("ID:                    " + Id + "\n" +
                        "Battery:               " + Battery + "\n" +
                        "Drone Location:        " + MyLocation + "\n");
            }
        }
    }
}
