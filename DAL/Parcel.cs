using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public Parcel(int id, int senderId, int targetId, WeightCategories weight, DateTime reuqested, DateTime delivered, DateTime pickedUp, DateTime scheduled, Priorities priority, int droneId)
            {
                Id = id;
                SenderId = senderId;
                TargetId = targetId;
                Weight = weight;
                Reuqested = reuqested;
                Delivered = delivered;
                PickedUp = pickedUp;
                Scheduled = scheduled;
                Priority = priority;
                DroneId = droneId;
            }
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

    }
}
