using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BO
{
    public class Customer
    {
        public Customer(int id, string name, string phoneNum, Location customerLocation, List<ParcelForCustomer> fromCustomer, List<ParcelForCustomer> toCustomer)
        {
            Id = id;
            Name = name;
            PhoneNum = phoneNum;
            CustomerLocation = customerLocation;
            FromCustomer = fromCustomer;
            ToCustomer = toCustomer;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public Location CustomerLocation { get; set; }
        public List<ParcelForCustomer> FromCustomer { get; set; }
        public List<ParcelForCustomer> ToCustomer { get; set; }

        public override string ToString()
        {
            return ("ID:                        " + Id + "\n" +
                    "Name:                      " + Name + "\n" +
                    "Phone:                     " + PhoneNum + "\n" +
                    "Parcels From Customer:     " + (FromCustomer.Count() != 0 ? FromCustomer.ToString() : "-----------") + "\n" +
                    "Parcel To Customer:        " + (ToCustomer.Count() != 0 ? ToCustomer.ToString() : "-----------") + "\n");
        }
    }
}

