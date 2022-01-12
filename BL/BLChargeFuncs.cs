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
        public void ChargeDrone(int id)
        {
            if (Drones.Exists(x => x.Id == id) == false)
            {
                throw new BO.IdNotExistException(id);
            }
            var drone = Drones.Find(x => x.Id == id);
            if (drone.Status != DroneStatuses.vacant)
            {
                throw new BO.DroneIsntVacant(id);
            }
            lock (MyDal)
            {
                var station = GetClosestStationWithChargeSlots(drone.MyLocation, (List<DO.Station>)MyDal.StationList(x => true));
                Location stationLocation = new Location(station.Longitude, station.Lattitude);
                if (DistanceTo(drone.MyLocation, stationLocation) * Available > drone.Battery)
                {
                    throw new BO.DroneHaveToLittleBattery(id);
                }
                drone.Battery -= DistanceTo(drone.MyLocation, stationLocation) * Available;
                drone.MyLocation = stationLocation;
                drone.Status = DroneStatuses.maintenance;
                Drones[Drones.FindIndex(x => x.Id == id)] = drone;
                MyDal.ChargeDrone(id, station.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisChargeDrone(int id, double time)
        {
            if (time <= 0)
            {
                throw new BO.CantBeNegative(time);
            }
            if (!Drones.Exists(x => x.Id == id))
            {
                throw new BO.IdNotExistException(id);
            }
            var drone = Drones.Find(x => x.Id == id);
            if (drone.Status != DroneStatuses.maintenance)
            {
                throw new BO.DroneIsntMaintenance(id);
            }
            drone.Battery += time * ChargePerHour;
            drone.Battery = drone.Battery > 100 ? 100 : drone.Battery;
            drone.Status = DroneStatuses.vacant;
            Drones[Drones.FindIndex(x => x.Id == id)] = drone;
            lock (MyDal)
            {
                int stationId = MyDal.StationList(x => true).ToList().Find(x => x.Longitude == drone.MyLocation.Longitude && x.Lattitude == drone.MyLocation.Latitude).Id;
                MyDal.DisChargeDrone(id, stationId);
            }
        }
        
    }
}
