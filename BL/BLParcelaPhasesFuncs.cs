﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void Attribution(int droneId)
        {
            if (!Drones.Exists(x => x.Id == droneId))
            {
                throw new BO.IdNotExistException(droneId);
            }
            var drone = Drones.Find(x => x.Id == droneId);

            if (drone.Status != DroneStatuses.vacant)
            {
                throw new BO.DroneIsntVacant(droneId);
            }
            List<IDAL.DO.Parcel> parcelList = (List<IDAL.DO.Parcel>)MyDal.ParcelList();
            parcelList.RemoveAll(x => (int)x.Weight > (int)drone.MaxWeight);//removing all the to heavy parcels
            if (parcelList.Count == 0)
            {
                throw new BO.NoParcelMatch(droneId);
            }
            //now i will order the list so the priority is the main influancer then the weight(bigger better) and then the distance(smaller better)
            parcelList.OrderByDescending(x => (int)x.Priority * 10 + (int)x.Weight + 1 / DistanceTo(drone.MyLocation, GetParcelSenderLocation(x.Id)));
            foreach (var parcel in parcelList)
            {
                Location senderLocation = GetParcelSenderLocation(parcel.Id), targetLocation = GetParcelTargetLocation(parcel.Id);
                var closestStationToTarget = GetClosestStation(targetLocation, (List<IDAL.DO.Station>)MyDal.StationList());
                if (DistanceTo(drone.MyLocation, senderLocation) * Available +
                    GetElectricityPerKM(DistanceTo(senderLocation, targetLocation), (WeightCategories)parcel.Weight) +
                    DistanceTo(targetLocation, new Location(closestStationToTarget.Longitude, closestStationToTarget.Lattitude)) * Available < drone.Battery)
                {
                    drone.Status = DroneStatuses.Shipping;
                    Drones[Drones.FindIndex(x => x.Id == drone.Id)] = drone;
                    MyDal.Attribution(drone.Id, parcel.Id);
                    return;
                }
            }
            throw new BO.DroneHaveToLittleBattery(droneId);
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
            if (parcel.Scheduled != DateTime.MinValue || parcel.PickedUp == DateTime.MinValue)
            {
                throw new BO.ParcelDeliveredOrNotPickedUp(parcel.Id);
            }
            Location parcelLocation = new Location(MyDal.DisplayCustomer(parcel.SenderId).Longitude, MyDal.DisplayCustomer(parcel.SenderId).Lattitude);
            drone.Battery -= DistanceTo(drone.MyLocation, parcelLocation) * Available;
            drone.MyLocation = parcelLocation;
            Drones[Drones.FindIndex(x => x.Id == dronelId)] = drone;
            MyDal.PickedParcelUp(parcel.Id);
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
            Location senderLocation = new Location(MyDal.DisplayCustomer(parcel.SenderId).Longitude, MyDal.DisplayCustomer(parcel.SenderId).Lattitude);
            Location targetLocation = new Location(MyDal.DisplayCustomer(parcel.TargetId).Longitude, MyDal.DisplayCustomer(parcel.TargetId).Lattitude);

            drone.Battery -= GetElectricityPerKM(DistanceTo(senderLocation, targetLocation), (WeightCategories)parcel.Weight);
            drone.MyLocation = targetLocation;
            drone.Status = DroneStatuses.vacant;
            Drones[Drones.FindIndex(x => x.Id == dronelId)] = drone;
            MyDal.ParcelDelivered(parcel.Id);
        }
    }
}