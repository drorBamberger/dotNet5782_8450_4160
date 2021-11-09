using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class Parcel
        {
            public int Id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Receiver { get; set; }
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
                    +"Target:           " + Receiver + "\n"
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
