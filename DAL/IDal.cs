using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public interface IDal
    {
        void addCustomer(int id, string name, string phone, double longitude, double lattitude);
        void addDrone(int id, string model, int maxWeight);
        void addParcel(int senderId, int targetId, int weight, int priority, int droneId);
        void addStation(int id, string name, double longitude, double lattitude, int chargeSlots);
        void attribution(int droneId, int parcelId);
        void ChargeDrone(int droneId, int stationId);
        IEnumerator<Customer> customerList();
        void DisChargeDrone(int droneId, int stationId);
        Customer DisplayCustomer(int id);
        Drone DisplayDrone(int id);
        Parcel DisplayParcel(int id);
        Station displayStation(int id);
        IEnumerator<Drone> DroneList();
        IEnumerator<Station> freeStations();
        void ParcelDelivered(int parcelId);
        IEnumerator<Parcel> parcelList();
        IEnumerator<Parcel> parcelListNotTaken();
        void PickedParcelUp(int parcelId);
        IEnumerator<Station> StationList();

        double[] AskForElectricity(int droneId);
    }
}