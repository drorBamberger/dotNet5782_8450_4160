using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class DroneInCharge
    {
        public DroneInCharge(int id, double battery)
        {
            Id = id;
            Battery = battery;
        }

        public int Id { get; set; }
        public double Battery { get; set; }

        public override string ToString()
        {
            return ("ID:                    " + Id + "\n" +
                    "Battery:               " + Battery + "\n");
        }
    }
}

