using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void ChargeDrone(int id)
        {
            if (!Drones.Exists(x => x.Id == id))
            {
                throw new BO.IdNotExistException(id);
            }
            var drone = Drones.Find(x => x.Id == id);
            if (drone.Status != DroneStatuses.vacant)
            {
                throw new BO.DroneIsntVacant(id);
            }
            var station = GetClosestStationWithChargeSlots(drone.MyLocation, (List<IDAL.DO.Station>)MyDal.StationList());
            Location stationLocation = new Location(station.Longitude, station.Lattitude);
            if (DistanceTo(drone.MyLocation, stationLocation )* Available>drone.Battery)
            {
                throw new BO.DroneCantGetToTheClosestStation(id);
            }
            drone.Battery -= DistanceTo(drone.MyLocation, stationLocation) * Available;
            drone.MyLocation = stationLocation;
            drone.Status = DroneStatuses.maintenance;
            Drones.RemoveAll(x => x.Id == drone.Id);
            Drones.Add(drone);
            MyDal.ChargeDrone(id, station.Id);
        }

        public void DisChargeDrone(int id, double time)
        {
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
            drone.Status = DroneStatuses.vacant;
            Drones.RemoveAll(x => x.Id == drone.Id);
            Drones.Add(drone);
            MyDal.DisChargeDrone(id,
                ((List<IDAL.DO.Station>)(MyDal.StationList())).Find(x => new Location(x.Longitude, x.Lattitude) == drone.MyLocation).Id);
        }
        public void Attribution(int droneId)
        {
            if (!Drones.Exists(x => x.Id == droneId))
            {
                throw new BO.IdNotExistException(droneId);
            }
            var drone = Drones.Find(x => x.Id == droneId);
            
            if(drone.Status != DroneStatuses.vacant)
            {
                throw new BO.DroneIsntVacant(droneId);
            }
            List<IDAL.DO.Parcel> parcelList = (List<IDAL.DO.Parcel>)MyDal.ParcelList();
            parcelList.OrderByDescending(x => (int)x.Priority * 10 + (int)x.Weight + 1/DistanceTo(drone.MyLocation, GetParcelSenderLocation(x.Id)));
            foreach(var parcel in parcelList)
            {
                Location senderLocation = GetParcelSenderLocation(parcel.Id), targetLocation = GetParcelTargetLocation(parcel.Id);
                var closestStationToTarget = GetClosestStation(targetLocation, (List<IDAL.DO.Station>)MyDal.StationList());
                if (DistanceTo(drone.MyLocation, senderLocation)*Available + 
                    GetElectricityPerKM(DistanceTo(senderLocation, targetLocation), (WeightCategories)parcel.Weight) + 
                    DistanceTo(targetLocation, new Location(closestStationToTarget.Longitude, closestStationToTarget.Lattitude))*Available < drone.Battery)
                {
                    drone.Status = DroneStatuses.Shipping;
                    Drones[Drones.FindIndex(x => x.Id == drone.Id)] = drone;
                    MyDal.Attribution(drone.Id, parcel.Id);
                }
            }
        }
        public void PickedParcelUp(int dronelId)
        {
            if (!Drones.Exists(x => x.Id == dronelId))
            {
                throw new BO.IdNotExistException(dronelId);
            }
            DroneForList drone = Drones.Find(x => x.ParcelId == dronelId);
            if (drone.Status != DroneStatuses.Shipping)
            {
                throw new BO.DroneIsntShipping(dronelId);
            }
            IDAL.DO.Parcel parcel;
            try
            {
                parcel = MyDal.DisplayParcel(drone.ParcelId);
            }
            catch (IDAL.DO.IdTakenException err)
            {
                throw new BO.IdTakenException(err.Id);
            }
            if(parcel.Scheduled != DateTime.MinValue || parcel.PickedUp == DateTime.MinValue)
            {
                throw new BO.ParcelDeliveredOrNotPickedUp(parcel.Id);
            }
            //TO DO: func and exp

        }
        public void ParcelDelivered(int dronelId)
        {
            if (!Drones.Exists(x => x.Id == dronelId))
            {
                throw new BO.IdNotExistException(dronelId);
            }
            DroneForList drone = Drones.Find(x => x.ParcelId == dronelId);
            if (drone.Status != DroneStatuses.Shipping)
            {
                throw new BO.DroneIsntShipping(dronelId);
            }
            IDAL.DO.Parcel parcel;
            try
            {
                parcel = MyDal.DisplayParcel(drone.ParcelId);
            }
            catch (IDAL.DO.IdTakenException err)
            {
                throw new BO.IdTakenException(err.Id);
            }
            if (parcel.PickedUp != DateTime.MinValue || parcel.Delivered == DateTime.MinValue)
            {
                throw new BO.ParcelPickedUpOrIsntBinded(parcel.Id);
            }
            //TO DO: func and exp
        }
    }
}
