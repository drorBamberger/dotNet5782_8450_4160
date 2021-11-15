using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class StationForList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int FreeChargeSlots { get; set; }
            public int OccupiedChargeSlots { get; set; }

            public override string ToString()
            {
                return ("ID:                          " + Id + "\n" +
                        "Name:                        " + Name + "\n" +
                        "Free ChargeSlots:            " + FreeChargeSlots + "\n" +
                        "Occupied ChargeSlots:        " + OccupiedChargeSlots + "\n");
            }
        }
    }
}
