using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class ParcelForCustomer
        {
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
}
