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
        public void AddStation(int id, string name, Location location, int chargeSlots)
        {
            try
            {
                MyDal.AddStation(id, name, location.Longitude, location.Latitude, chargeSlots);
            }
            catch (DO.IdTakenException err)
            {
                throw new BO.IdTakenException(err.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, int maxWeight, int stationId)
        {
            Random rnd = new Random();
            lock (MyDal)
            {
                if (MyDal.StationList(x => true).Any(x => x.Id == stationId) == false)
                {
                    throw new BO.IdNotExistException(stationId);
                }
                try
                {
                    MyDal.AddDrone(id, model, maxWeight);
                }
                catch (DO.IdTakenException err)
                {
                    throw new BO.IdTakenException(err.Id);
                }
                DO.Station a = MyDal.DisplayStation(stationId);
                a.ChargeSlots--;
                MyDal.DeleteStation(stationId);
                MyDal.AddStation(a.Id, a.Name, a.Longitude, a.Lattitude, a.ChargeSlots);
                Drones.Add(new DroneForList(id, model, (WeightCategories)maxWeight, rnd.NextDouble() * 20 + 20,
                    DroneStatuses.maintenance, 0, new Location(MyDal.DisplayStation(stationId).Longitude,
                    MyDal.DisplayStation(stationId).Lattitude)));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string name, string phone, Location location)
        {
            
            try
            {
                int.Parse(phone);
                MyDal.AddCustomer(id, name, phone, location.Longitude, location.Latitude);
            }
            catch (DO.IdTakenException err)
            {
                throw new BO.IdTakenException(err.Id);
            }
            catch
            {
                throw new BO.InvalidInput();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(int senderId, int targetId, int maxWeight, int priority)
        {
            lock (MyDal)
            {
                if (MyDal.CustomerList(x => x.Id == senderId).Any() == false)
                {
                    throw new BO.IdNotExistException(senderId);
                }
                if (MyDal.CustomerList(x => x.Id == targetId).Any() == false)
                {
                    throw new BO.IdNotExistException(targetId);
                }
                MyDal.AddParcel(senderId, targetId, maxWeight, priority, 0);
            }
        }
    }
}
