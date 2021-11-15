using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int PhoneNum { get; set; }
            public Location CustomerLocation { get; set; }
            public List<ParcelForCustomer> FromCustomer { get; set; }
            public List<ParcelForCustomer> ToCustomer { get; set; }

            public override string ToString()
            {
                return ("ID:        " + Id + "\n" +
                        "Name:      " + Name + "\n" +
                        "Phone:     " + PhoneNum + "\n" +
                        "Parcels From Customer: " + FromCustomer.ToString() + "\n" +
                        "Parcel To Customer: " + ToCustomer.ToString() + "\n");
            }
        }
    }
}
