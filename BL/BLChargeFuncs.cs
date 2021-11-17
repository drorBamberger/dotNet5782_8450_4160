using System;
using System.Collections.Generic;
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
            if (Drones.Find(x => x.Id == id).Status != DroneStatuses.vacant)
            {
                throw new BO.DroneIsntVacant(id);
            }
            //TO DO: func and exp if impossible
        }

        public void DisChargeDrone(int id, double time)
        {
            if (!Drones.Exists(x => x.Id == id))
            {
                throw new BO.IdNotExistException(id);
            }
            if (Drones.Find(x => x.Id == id).Status != DroneStatuses.maintenance)
            {
                throw new BO.DroneIsntMaintenance(id);
            }
            //TO DO: func and exp

        }
        public void Attribution(int droneId)
        {
            if (!Drones.Exists(x => x.Id == droneId))
            {
                throw new BO.IdNotExistException(droneId);
            }
            //TO DO: func and exp

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
