using System;
using System.Collections.Generic;
using System.Text;


namespace BO
{
    public class StationForList
    {
        public StationForList(int id, string name, int freeChargeSlots, int occupiedChargeSlots)
        {
            Id = id;
            Name = name;
            FreeChargeSlots = freeChargeSlots;
            OccupiedChargeSlots = occupiedChargeSlots;
        }

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
