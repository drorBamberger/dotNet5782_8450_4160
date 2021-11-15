using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class ParcelForList
        {
            public int Id { get; set; }
            public string Sender { get; set; }
            public string Receiver { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public ParcelStatuses Status { get; set; }
            public override string ToString()
            {
                return
                    ("ID:               " + Id + "\n"
                    +"Sender:           " + Sender + "\n"
                    +"Target:           " + Receiver + "\n"
                    +"Weight:           " + Weight + "\n"
                    +"Priority:         " + Priority + "\n"
                    +"Status:           " + Status + "\n");
            }
        }
    }
}
