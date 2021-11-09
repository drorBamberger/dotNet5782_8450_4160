using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location MyLocation { get; set; }
            public int ChargeSlots { get; set; }
            public List<DroneInCharge> Drones { get; set; }

            public override string ToString()
            {
                return ("ID:               " + Id + "\n" +
                        "Name:             " + Name + "\n" +
                        "Free ChargeSlots: " + ChargeSlots + "\n" +
                        "Location:         " + MyLocation + "\n" +
                        "Drones In Charge: " + Drones.ToString() + "\n");
            }
        }
    }
}
