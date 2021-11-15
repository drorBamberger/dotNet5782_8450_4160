using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class CustomerForList
        {
            public CustomerForList(int id, string name, string phoneNum, int sendProvided, int parcelArrived, int parcelOnTheWay)
            {
                Id = id;
                Name = name;
                PhoneNum = phoneNum;
                SendProvided = sendProvided;
                ParcelArrived = parcelArrived;
                ParcelOnTheWay = parcelOnTheWay;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNum { get; set; }
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
