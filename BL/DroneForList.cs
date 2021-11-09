using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class DroneForList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            double Battery { get; set; }
            public DroneStatuses Status { get; set; }
            public int ParcelId { get; set; }
            public Location MyLocation { get; set; }


            public override string ToString()
            {
                return ("ID:                    " + Id + "\n" +
                        "Model:                 " + Model + "\n" +
                        "Maximum weight:        " + MaxWeight + "\n" +
                        "Battery:               " + Battery + "\n" +
                        "Status:                " + Status + "\n" +
                        "Parcel:                " + ParcelId + "\n" +
                        "Drone Location:        " + MyLocation + "\n");
            }
        }
    }
}
