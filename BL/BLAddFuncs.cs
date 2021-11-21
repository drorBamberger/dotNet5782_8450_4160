using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void AddStation(int id, string name, Location location, int chargeSlots)
        {
            try
            {
                MyDal.AddStation(id, name, location.Longitude, location.Latitude, chargeSlots);
            }
            catch(IDAL.DO.IdTakenException err)
            {
                throw new BO.IdTakenException(err.Id);
            }
        }

        public void AddDrone(int id, string model, int maxWeight, int stationId)
        {
            Random rnd = new Random();

            try
            {
                MyDal.AddDrone(id, model, maxWeight);
            }
            catch (IDAL.DO.IdTakenException err)
            {
                throw new BO.IdTakenException(err.Id);
            }

            Drones.Add(new DroneForList(id, model, (WeightCategories)maxWeight, rnd.NextDouble() * 20 + 20,
                DroneStatuses.maintenance, 0, new Location(MyDal.DisplayStation(stationId).Longitude,
                MyDal.DisplayStation(stationId).Lattitude)));
        }

        public void AddCustomer(int id, string name, string phone, Location location)
        {
            try
            {
                MyDal.AddCustomer(id, name, phone, location.Longitude, location.Latitude);
            }
            catch (IDAL.DO.IdTakenException err)
            {
                throw new BO.IdTakenException(err.Id);
            }
        }

        public void AddParcel(int senderId, int targetId, int maxWeight, int priority)
        {
            if(MyDal.CustomerList().Where(x=>x.Id == senderId).Any())
            {
                throw new BO.IdNotExistException(senderId);
            }
            if(MyDal.CustomerList().Where(x => x.Id == targetId).Any())
            {
                throw new BO.IdNotExistException(targetId);
            }
            MyDal.AddParcel(senderId, targetId, maxWeight, priority, 0);
        }
    }
}
