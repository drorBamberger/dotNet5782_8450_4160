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
        public void PickedParcelUp(int parcelId)
        {
            if (!Drones.Exists(x => x.Id == parcelId))
            {
                throw new BO.IdNotExistException(parcelId);
            }
            if (Drones.Find(x => x.Id == parcelId).Status != DroneStatuses.Shipping)
            {
                throw new BO.DroneIsntMaintenance(parcelId);
            }
            //TO DO: func and exp
        }
        public void ParcelDelivered(int parcelId)
        {
            if (!Drones.Exists(x => x.Id == parcelId))
            {
                throw new BO.IdNotExistException(parcelId);
            }
            if (Drones.Find(x => x.Id == parcelId).Status != DroneStatuses.Shipping)
            {
                throw new BO.DroneIsntMaintenance(parcelId);
            }
            //TO DO: func and exp
        }
    }
}
