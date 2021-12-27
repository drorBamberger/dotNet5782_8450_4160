using System;
using System.Collections.Generic;
using System.Text;


namespace BO
{
    public class Drone
    {
        public Drone(int id, string model, WeightCategories maxWeight, double battery, DroneStatuses status, ParcelOnDelivery myParcel, Location myLocation)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            Battery = battery;
            Status = status;
            MyParcel = myParcel;
            MyLocation = myLocation;
        }

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
                    "Maximum weight:        " + MaxWeight + "\n" +
                    "Battery:               " + Battery + "\n" +
                    "Status:                " + Status + "\n" +
                    "Parcel:                " + MyParcel + "\n" +
                    "Drone Location:        " + MyLocation + "\n");
        }
    }
}

