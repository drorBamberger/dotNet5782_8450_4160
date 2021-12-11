using System;
using System.Collections.Generic;
using System.Text;


namespace BO
{
    public class CustomerForList
    {
        public CustomerForList(int id, string name, string phoneNum, int sendProvided, int sendNotProvided, int parcelArrived, int parcelOnTheWay)
        {
            Id = id;
            Name = name;
            PhoneNum = phoneNum;
            SendProvided = sendProvided;
            SendNotProvided = sendNotProvided;
            ParcelsArrived = parcelArrived;
            ParcelsOnTheWay = parcelOnTheWay;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public int SendProvided { get; set; }
        public int SendNotProvided { get; set; }
        public int ParcelsArrived { get; set; }
        public int ParcelsOnTheWay { get; set; }

        public override string ToString()
        {
            return ("ID:                            " + Id + "\n" +
                    "Name:                          " + Name + "\n" +
                    "Phone:                         " + PhoneNum + "\n" +
                    "Parcels Send and Provided:     " + SendProvided + "\n" +
                    "Parcels Send and Not Provided: " + SendNotProvided + "\n" +
                    "Parcel Arrived:                " + ParcelsArrived + "\n" +
                    "Parcels On The Way:            " + ParcelsOnTheWay + "\n");
        }
    }
}

