﻿using System;

namespace DAL
{
    namespace DO
    {
        public class Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
        }

        public class DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }

        }

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }

            public override string ToString(){return Name;}
        }
        public class Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public DateTime Reuqested { get; set; }
            public DateTime Delivered { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Scheduled { get; set; }
            public Priorities Priority { get; set; }
            public int DroneId { get; set; }

        }
        public class Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlots { get; set; }

        }

    }
}

