using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : BLApi.IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationForList> StationList()
        {
            lock (MyDal)
            {
                return StationsToBL(MyDal.StationList(x => true));
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationForList> StationList(Predicate<StationForList> predictaion)
        {
            lock (MyDal)
            {
                return ((List<StationForList>)StationsToBL(MyDal.StationList(x => true))).FindAll(predictaion);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneForList> DroneList()
        {
            return Drones;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneForList> DroneList(Predicate<DroneForList> predictaion)
        {
            return Drones.FindAll(predictaion);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerForList> CustomerList()
        {
            lock (MyDal)
            {
                IEnumerable<DO.Customer> original = MyDal.CustomerList(x => true);
                List<CustomerForList> comeBack = new List<CustomerForList>();
                int counterSendProvide = 0, counterSendNotProvide = 0, counterGot = 0, counterOnTheWay = 0;
                foreach (var customer in original)
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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelForList> ParcelList()
        {
            lock (MyDal)
            {
                return ParcelsToBL(MyDal.ParcelList(x => true));
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelForList> ParcelList(Predicate<ParcelForList> predictaion)
        {
            lock (MyDal)
            {
                return ((List<ParcelForList>)ParcelsToBL(MyDal.ParcelList(x => true))).FindAll(predictaion);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelForList> ParcelListNotTaken()
        {
            lock (MyDal)
            {
                return ParcelsToBL(MyDal.ParcelList(x => x.Scheduled == null));
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationForList> FreeStations()
        {
            lock (MyDal)
            {
                return StationsToBL(MyDal.StationList(x => x.ChargeSlots != 0));
            }
        }
    }
}
