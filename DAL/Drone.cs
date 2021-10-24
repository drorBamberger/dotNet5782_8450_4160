using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public Drone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
            {
                Id = id;
                Model = model;
                MaxWeight = maxWeight;
                Status = status;
                Battery = battery;
            }

            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public override string ToString() { return ("ID:        " + Id + "\n" + "Model:      " + Model + "\n" + "Maximum weight:      " + MaxWeight + "\n" + "Drone status:      " + Status + "\n"  + "battery:        " + Battery); }
        }

    }
}

