using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class ParcelOnDelivery
        {
            public int Id { get; set; }
            public ParcelOnDeliveryStatuses Status { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Receiver { get; set; }
            public Location Collecting { get; set; }
            public Location Target { get; set; }
            public double TransferDistance { get; set; }

            public override string ToString()
            {
                return
                    ("ID:                     " + Id + "\n"
                    + "Status:                " + Status + "\n"
                    + "Weight:                " + Weight + "\n"
                    + "Priority:              " + Priority + "\n"
                    + "Weight:                " + Weight + "\n"
                    + "Priority:              " + Priority + "\n"
                    + "Sender:                " + Sender + "\n"
                    + "Receiver:              " + Receiver + "\n"
                    + "Collecting Location:   " + Collecting + "\n"
                    + "Target Location:       " + Target + "\n"
                    + "TransferDistance:      " + TransferDistance + "\n");
            }
        }
    }
}
