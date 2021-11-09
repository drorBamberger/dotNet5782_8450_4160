using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class DroneInParcel
        {
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
