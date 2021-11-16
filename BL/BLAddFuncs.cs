using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void AddStation(int id, string name, Location location, int chargeSlots)
        {
            MyDal.AddStation(id, name, location.Longitude, location.Latitude, chargeSlots);
        }

        public void AddDrone(int id, string model, int maxWeight, int stationId)
        {
            Random rnd = new Random();
            MyDal.AddDrone(id, model, maxWeight);
            Drones.Add(new DroneForList(id, model, (WeightCategories)maxWeight, rnd.NextDouble() * 20 + 20,
                DroneStatuses.maintenance, 0, new Location(MyDal.DisplayStation(stationId).Longitude,
                MyDal.DisplayStation(stationId).Lattitude)));
        }

        public void AddCustomer(int id, string name, string phone, Location location)
        {
            MyDal.AddCustomer(id, name, phone, location.Longitude, location.Latitude);
        }

        public void AddParcel(int senderId, int targetId, int maxWeight, int priority)
        {
            MyDal.AddParcel(senderId, targetId, maxWeight, priority, 0);
        }
    }
}
