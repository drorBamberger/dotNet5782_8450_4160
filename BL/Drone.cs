using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses Status { get; set; }
            public ParcelOnDelivery MyParcel { get; set; }
            public Location MyLocation { get; set; }


            public override string ToString()
            {
                return ("ID:                    " + Id + "\n" +
                        "Model:                 " + Model + "\n" +
                        "Maximum weight:        " + MaxWeight + "\n"+
                        "Battery:               " + Battery +"\n"+
                        "Status:                " + Status + "\n" +
                        "Parcel:                " + MyParcel + "\n" +
                        "Drone Location:        " + MyLocation + "\n");
            }
        }
    }
}
