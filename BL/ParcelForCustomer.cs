using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class ParcelForCustomer
    {
        public ParcelForCustomer(int id, WeightCategories weight, Priorities priority, ParcelStatuses status, CustomerInParcel otherSide)
        {
            Id = id;
            Weight = weight;
            Priority = priority;
            Status = status;
            OtherSide = otherSide;
        }

        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses Status { get; set; }
        public CustomerInParcel OtherSide { get; set; }

        public override string ToString()
        {
            return
                ("ID:                " + Id + "\n"
                + "Weight:           " + Weight + "\n"
                + "Priority:         " + Priority + "\n"
                + "Status:           " + Status + "\n"
                + "Other Side        " + OtherSide + "\n");
        }
    }
}
