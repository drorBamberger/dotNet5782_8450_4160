using System;
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
            foreach(var station in original)
            {
                counter = Drones.FindAll(x => x.MyLocation == new Location(station.Longitude, station.Lattitude) &&
                x.Status == DroneStatuses.maintenance).Count();
                comeBack.Add(new StationForList(station.Id, station.Name, station.ChargeSlots, counter));
            }
            return comeBack;
        }

        public IEnumerable<DroneForList> DroneList()
        {
            return Drones;
        }

        public IEnumerable<CustomerForList> CustomerList()
        {
            IEnumerable<IDAL.DO.Customer> original = MyDal.CustomerList();
            List<CustomerForList> comeBack = new List<CustomerForList>();
            int counterSendProvide = 0, counterSendNotProvide = 0, counterGot = 0, counterOnTheWay = 0;
            foreach(var customer in original)
            {
                counterSendProvide = MyDal.ParcelList().Where(x => x.SenderId == customer.Id && x.Delivered != DateTime.MinValue).Count();
                counterSendNotProvide = MyDal.ParcelList().Where(x => x.SenderId == customer.Id && x.Delivered == DateTime.MinValue).Count();
                counterGot = MyDal.ParcelList().Where(x => x.TargetId == customer.Id && x.Delivered != DateTime.MinValue).Count();
                counterOnTheWay = MyDal.ParcelList().Where(x => x.TargetId == customer.Id && x.Delivered == DateTime.MinValue).Count();
                comeBack.Add(new CustomerForList(customer.Id, customer.Name, customer.Phone,
                    counterSendProvide, counterSendNotProvide, counterGot, counterOnTheWay));
            }
            return comeBack;
        }

        public IEnumerable<ParcelForList> ParcelList()
        {
            IEnumerable<IDAL.DO.Parcel> original = MyDal.ParcelList();
            List<ParcelForList> comeBack = new List<ParcelForList>();
            foreach (var parcel in original)
            {
                comeBack.Add(new ParcelForList(parcel.Id, MyDal.DisplayCustomer(parcel.SenderId).Name,
                    MyDal.DisplayCustomer(parcel.TargetId).Name, (WeightCategories)parcel.Weight,
                    (Priorities)parcel.Priority, GetParcelStatus(parcel.Id))); 
            }
            return comeBack;
        }

        public IEnumerable<ParcelForList> ParcelListNotTaken()
        {
            return ParcelList().Where(x => x.Status == ParcelStatuses.Reuqested);
        }
        public IEnumerable<StationForList> FreeStations()
        {
            return StationList().Where(x => x.FreeChargeSlots != 0);
        }
    }
}
