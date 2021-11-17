using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void DroneUpdate(int id, string name)
        {
            Drones.Find(x => x.Id == id).Model = name;
            var d = MyDal.DisplayDrone(id);
            d.Model = name;
            MyDal.UpDateDrone(d);
        }
        public void StationUpdate(int id, string name, int allChargeSlot)
        {
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
                    if(drone.MyLocation == new Location(d.Longitude, d.Lattitude) && drone.Status == DroneStatuses.maintenance)
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
            var d = MyDal.DisplayCustomer(id);
            if (name != "")
            {
                d.Name = name;
            }
            if (phone != "")
            {
                d.Phone = name;
            }
            MyDal.UpDateCustomer(d);
        }
    }
}
