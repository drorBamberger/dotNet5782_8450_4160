using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;

namespace BL
{
    public partial class BL : BLApi.IBL
    {
        public void DroneUpdate(int id, string name)
        {
            if(Drones.FindIndex(x=>x.Id == id) == -1)
            {
                throw new BO.IdNotExistException(id);
            }
            Drones.Find(x => x.Id == id).Model = name;
            var d = MyDal.DisplayDrone(id);
            d.Model = name;
            MyDal.UpDateDrone(d);
        }
        public void StationUpdate(int id, string name, int allChargeSlot)
        {
            if (MyDal.StationList(x => x.Id == id).Any(x=>true) == false)
            {
                throw new BO.IdNotExistException(id);
            }
            var d = MyDal.DisplayStation(id);
            int counter = 0;
            if(name != "")
            {
                d.Name = name;
            }
            if(allChargeSlot != -1)
            {
                foreach(var drone in Drones)
                {
                    if(drone.MyLocation.Longitude == d.Longitude && drone.MyLocation.Latitude ==  d.Lattitude && drone.Status == DroneStatuses.maintenance)
                    {
                        counter++;
                    }
                }
                d.ChargeSlots = allChargeSlot - counter;
            }
            MyDal.UpDateStation(d);
        }
        public void CustomerUpdate(int id, string name, string phone)
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
