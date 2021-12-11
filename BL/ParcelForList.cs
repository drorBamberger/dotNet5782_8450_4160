using System;
using System.Collections.Generic;
using System.Text;

    namespace BO
    {
        public class ParcelForList
        {
            public ParcelForList(int id, string senderName, string targetName, WeightCategories weight, Priorities priority, ParcelStatuses status)
            {
                Id = id;
                SenderName = senderName;
                TargetName = targetName;
                Weight = weight;
                Priority = priority;
                Status = status;
            }

            public int Id { get; set; }
            public string SenderName { get; set; }
            public string TargetName { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public ParcelStatuses Status { get; set; }
            public override string ToString()
            {
                return
                    ("ID:               " + Id + "\n"
                    +"Sender:           " + SenderName + "\n"
                    +"Target:           " + TargetName + "\n"
                    +"Weight:           " + Weight + "\n"
                    +"Priority:         " + Priority + "\n"
                    +"Status:           " + Status + "\n");
            }
        }
    }

