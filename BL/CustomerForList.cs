using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class CustomerForList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int PhoneNum { get; set; }
            public int SendProvided  { get; set; }
            public int ParcelArrived { get; set; }
            public int ParcelOnTheWay { get; set; }

            public override string ToString()
            {
                return ("ID:                            " + Id + "\n" +
                        "Name:                          " + Name + "\n" +
                        "Phone:                         " + PhoneNum + "\n" +
                        "Parcels Send and Provided:     " + SendProvided + "\n" +
                        "Parcels Send and Not Provided: " + SendNotProvided + "\n" +
                        "Parcel Arrived:                " + ParcelArrived + "\n" +
                        "Parcels On The Way:            " + ParcelOnTheWay + "\n");
            }
        }
    }
}
