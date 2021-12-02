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
            return StationsToBL(MyDal.StationList(x => true));
        }

        public IEnumerable<DroneForList> DroneList()
        {
            return Drones;
        }

        public IEnumerable<CustomerForList> CustomerList()
        {
            IEnumerable<IDAL.DO.Customer> original = MyDal.CustomerList(x=>true);
            List<CustomerForList> comeBack = new List<CustomerForList>();
            int counterSendProvide = 0, counterSendNotProvide = 0, counterGot = 0, counterOnTheWay = 0;
            foreach(var customer in original)
            {
                counterSendProvide = MyDal.ParcelList(x => x.SenderId == customer.Id && x.Delivered != null).Count();
                counterSendNotProvide = MyDal.ParcelList(x => x.SenderId == customer.Id && x.Delivered == null).Count();
                counterGot = MyDal.ParcelList(x => x.TargetId == customer.Id && x.Delivered != null).Count();
                counterOnTheWay = MyDal.ParcelList(x => x.TargetId == customer.Id && x.Delivered == null).Count();
                comeBack.Add(new CustomerForList(customer.Id, customer.Name, customer.Phone,
                    counterSendProvide, counterSendNotProvide, counterGot, counterOnTheWay));
            }
            return comeBack;
        }

        public IEnumerable<ParcelForList> ParcelList()
        {
            return ParcelsToBL(MyDal.ParcelList(x => true));
        }

        public IEnumerable<ParcelForList> ParcelListNotTaken()
        {
            return ParcelsToBL(MyDal.ParcelList(x => x.Scheduled == null));
        }
        public IEnumerable<StationForList> FreeStations()
        {
            return StationsToBL(MyDal.StationList(x=>x.ChargeSlots != 0));
        }
    }
}
