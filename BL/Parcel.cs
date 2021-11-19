using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public Parcel(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, 
                DroneInParcel myDrone, DateTime reuqested, DateTime scheduled, DateTime pickedUp, DateTime delivered)
            {
                Id = id;
                Sender = sender;
                Target = target;
                Weight = weight;
                Priority = priority;
                MyDrone = myDrone;
                Reuqested = reuqested;
                Scheduled = scheduled;
                PickedUp = pickedUp;
                Delivered = delivered;
            }

            public int Id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DroneInParcel MyDrone { get; set; }
            public DateTime Reuqested { get; set; }
            public DateTime Delivered { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Scheduled { get; set; }

            public override string ToString()
            {
                return
                    ("ID:               " + Id + "\n"
                    +"Sender:           " + Sender + "\n"
                    +"Target:           " + Target + "\n"
                    +"Drone Id:         " + MyDrone + "\n"
                    +"Weight:           " + Weight + "\n"
                    +"Priority:         " + Priority + "\n"
                    +"Time Reuqested:   " + Reuqested.ToString() + "\n"
                    +"Time Scheduled:   " + Scheduled.ToString() + "\n"
                    +"Time Delivered:   " + Delivered.ToString() + "\n"
                    +"Time PickedUp:    " + PickedUp.ToString() + "\n");
            }
        }
    }
}
