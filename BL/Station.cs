using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BO
{
    public class Station
    {
        public Station(int id, string name, Location myLocation, int chargeSlots, List<DroneInCharge> drones)
        {
            Id = id;
            Name = name;
            MyLocation = myLocation;
            ChargeSlots = chargeSlots;
            Drones = drones;
        }

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
                    "Drones In Charge: " + (Drones.Count() != 0 ? Drones.ToString() : "--------") + "\n");
        }
    }
}

