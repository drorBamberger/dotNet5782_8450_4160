using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
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
        }
    }
}
