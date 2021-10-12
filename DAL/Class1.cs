using System;

namespace DAL
{
    namespace DO
    {
        #Dror
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
#aaa
    }
}

