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
        public void DroneUpdate(int id, string name)
        {
            if(Drones.FindIndex(x=>x.Id == id) == -1)
            {
                throw new BO.IdNotExistException(id);
            }
            Drones.Find(x => x.Id == id).Model = name;
            lock (MyDal)
            {
                var d = MyDal.DisplayDrone(id);
                d.Model = name;
                MyDal.UpDateDrone(d);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void StationUpdate(int id, string name, int allChargeSlot)
        {
            lock (MyDal)
            {
                if (MyDal.StationList(x => x.Id == id).Any(x=>true) == false)
            {
                throw new BO.IdNotExistException(id);
            }            
                var d = MyDal.DisplayStation(id);
                int counter = 0;
                if (name != "")
                {
                    d.Name = name;
                }
                if (allChargeSlot != -1)
                {
                    foreach (var drone in Drones)
                    {
                        if (drone.MyLocation.Longitude == d.Longitude && drone.MyLocation.Latitude == d.Lattitude && drone.Status == DroneStatuses.maintenance)
                        {
                            counter++;
                        }
                    }
                    d.ChargeSlots = allChargeSlot - counter;
                }
                MyDal.UpDateStation(d);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CustomerUpdate(int id, string name, string phone)
        {
            lock (MyDal)
            {
                if (MyDal.CustomerList(x => x.Id == id).Any(x => true) == false)
            {
                throw new BO.IdNotExistException(id);
            }
                var d = MyDal.DisplayCustomer(id);
                if (name != "")
                {
                    d.Name = name;
                }
                if (phone != "")
                {
                    d.Phone = phone;
                }
                MyDal.UpDateCustomer(d);
            }
        }
    }
}
