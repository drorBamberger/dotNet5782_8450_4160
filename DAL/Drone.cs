using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public Drone(int id, string model, WeightCategories maxWeight)
            {
                Id = id;
                Model = model;
                MaxWeight = maxWeight;
            }

            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public override string ToString() { 
                return ("ID:             " + Id + "\n" + 
                        "Model:          " + Model + "\n" + 
                        "Maximum weight: " + MaxWeight + "\n"); }
        }

    }
}

