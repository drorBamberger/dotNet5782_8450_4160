using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public Parcel(int senderId, int targetId, WeightCategories weight, Priorities priority, int droneId): this()
            {
                Id = DalObject.DataSource.ParcelId++;
                SenderId = senderId;
                TargetId = targetId;
                Weight = weight;
                Reuqested = DateTime.Now;
                Scheduled = DateTime.MinValue;
                PickedUp = DateTime.MinValue;
                Delivered = DateTime.MinValue;
                Priority = priority;
                DroneId = droneId;
            }
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public DateTime Reuqested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public Priorities Priority { get; set; }
            public int DroneId { get; set; }

            public override string ToString() { return 
                    ("ID:               " + Id + "\n"
                    + "Sender Id:       " + SenderId + "\n"
                    + "Target Id:       " + TargetId + "\n"
                    + "Drone Id:        " + DroneId + "\n"
                    + "Weight:          " + Weight + "\n"
                    + "Priority:        " + Priority + "\n"
                    + "Time Reuqested:  " + Reuqested.ToString() + "\n"
                    + "Time Scheduled:  " + Scheduled.ToString() + "\n"
                    + "Time Delivered:  " + Delivered.ToString() + "\n"
                    + "Time PickedUp:   " + PickedUp.ToString() + "\n"); }

        }

    }
}
