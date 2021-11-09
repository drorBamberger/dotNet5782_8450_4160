using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        void addCustomer(int id, string name, string phone, double longitude, double lattitude);
        void addDrone(int id, string model, int maxWeight);
        void addParcel(int id, int senderId, int targetId, int weight, int priority, int droneId);
        void addStation(int id, string name, double longitude, double lattitude, int chargeSlots);
        void attribution(int droneId, int parcelId);
        void ChargeDrone(int droneId, int stationId);
        IEnumerable<Customer> customerList();
        void DisChargeDrone(int droneId, int stationId);
        Customer DisplayCustomer(int id);
        Drone DisplayDrone(int id);
        Parcel DisplayParcel(int id);
        Station displayStation(int id);
        IEnumerable<Drone> DroneList();
        IEnumerable<Station> freeStations();
        void ParcelDelivered(int parcelId);
        IEnumerable<Parcel> parcelList();
        IEnumerable<Parcel> parcelListNotTaken();
        void PickedParcelUp(int parcelId);
        IEnumerable<Station> StationList();

        double[] AskForElectricity();
    }
}