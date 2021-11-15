using IBL.BO;

namespace BL
{
    public interface IBL
    {
        void AddStation(int id, string name, Location location, int chargeSlots);
        Drone DisplayDrone(int id);
        Station DisplayStation(int id);
    }
}