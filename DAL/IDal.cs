using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        public void AddCustomer(int id, string name, string phone, double longitude, double lattitude);
        public void AddDrone(int id, string model, int maxWeight);
        public void AddParcel(int senderId, int targetId, int weight, int priority, int droneId);
        public void AddStation(int id, string name, double longitude, double lattitude, int chargeSlots);
        public void Attribution(int droneId, int parcelId);
        public void ChargeDrone(int droneId, int stationId);
        public IEnumerable<Customer> CustomerList();
        public void DisChargeDrone(int droneId, int stationId);
        public Customer DisplayCustomer(int id);
        public Drone DisplayDrone(int id);
        public Parcel DisplayParcel(int id);
        public Station DisplayStation(int id);
        public IEnumerable<Drone> DroneList();
        public IEnumerable<Station> FreeStations();
        public void ParcelDelivered(int parcelId);
        public IEnumerable<Parcel> ParcelList();
        public IEnumerable<Parcel> ParcelListNotTaken();
        public void PickedParcelUp(int parcelId);
        public IEnumerable<Station> StationList();
        public double[] AskForElectricity();
    }
}