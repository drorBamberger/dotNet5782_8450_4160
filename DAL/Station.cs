using System;
using System.Collections.Generic;
using System.Text;


namespace DO
{
    public struct Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
        public int ChargeSlots { get; set; }

        public Station(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            Id = id;
            Name = name;
            Longitude = longitude;
            Lattitude = lattitude;
            ChargeSlots = chargeSlots;
        }
        public override string ToString()
        {
            return ("ID:               " + Id + "\n" +
                    "Name:             " + Name + "\n" +
                    "Free ChargeSlots: " + ChargeSlots + "\n" +
                    "Longitude:        " + Longitude + "\n" +
                    "Lattitude:        " + Lattitude + "\n");
        }
    }
}

