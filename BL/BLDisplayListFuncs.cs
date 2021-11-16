﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public IEnumerable<StationForList> StationList()
        {
            IEnumerable<IDAL.DO.Station> original = MyDal.StationList();
            List<StationForList> comeBack = new List<StationForList>();
            int counter = 0;
            
            foreach(var item in original)
            {
                foreach (var drone in Drones)
                {
                    if (drone.MyLocation == new Location(item.Longitude, item.Lattitude))
                        counter++;
                        
                }
                comeBack.Add(new StationForList(item.Id, item.Name, item.ChargeSlots, counter));
                counter = 0;
            }
            return comeBack;
        }

        public IEnumerable<DroneForList> DroneList()
        {
            List<DroneForList> comeBack = new List<DroneForList>();
            foreach(var item in Drones)
            {
                comeBack.Add(new DroneForList(item.Id, item.Model, item.MaxWeight,
                    item.Battery, item.Status, item.ParcelId, item.MyLocation));
            }
            return comeBack;
        }

        public IEnumerable<CustomerForList> CustomerList()
        {
            IEnumerable<IDAL.DO.Customer> original = MyDal.CustomerList();
            List<CustomerForList> comeBack = new List<CustomerForList>();
            int counterSendProvide = 0, counterSendNotProvide = 0, counterGot = 0, counterOnTheWay = 0;
            foreach(var item in original)
            {
                foreach(var parcel in MyDal.ParcelList())
                {
                    if(parcel.SenderId == item.Id)
                    {
                        if (parcel.Delivered == DateTime.MinValue)
                            counterSendNotProvide++;
                        else
                            counterSendProvide++;
                    }
                    if (parcel.TargetId == item.Id)
                    {
                        if (parcel.Delivered == DateTime.MinValue)
                            counterOnTheWay++;
                        else
                            counterGot++;
                    }
                }
                comeBack.Add(new CustomerForList(item.Id, item.Name, item.Phone,
                    counterSendProvide, counterSendNotProvide, counterGot, counterOnTheWay));

                counterSendProvide = 0; counterSendNotProvide = 0; counterGot = 0; counterOnTheWay = 0;
            }
            return comeBack;
        }
        public IEnumerable<ParcelForList> ParcelList()
        {
            IEnumerable<IDAL.DO.Parcel> original = MyDal.ParcelList();
            List<ParcelForList> comeBack = new List<ParcelForList>();
            ParcelStatuses status;
            foreach (var item in original)
            {
                comeBack.Add(new ParcelForList(item.Id, MyDal.DisplayCustomer(item.SenderId).Name,
                    MyDal.DisplayCustomer(item.TargetId).Name, (WeightCategories)item.Weight,
                    (Priorities)item.Priority, GetParcelStatus(item.Id))); 
            }
            return comeBack;
        }

        public IEnumerable<ParcelForList> ParcelListNotTaken()
        {
            List<ParcelForList> comeBack = new List<ParcelForList>();
            foreach(var parcel in ParcelList())
            {
                if (parcel.Status == ParcelStatuses.Reuqested)
                    comeBack.Add(parcel);
            }
            return comeBack;
        }
        public IEnumerable<StationForList> FreeStations()
        {
            List<StationForList> comeBack = new List<StationForList>();
            foreach (var station in StationList())
            {
                if (station.FreeChargeSlots!=0)
                    comeBack.Add(station);
            }
            return comeBack;
        }
    }
}