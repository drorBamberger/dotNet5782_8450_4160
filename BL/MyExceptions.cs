using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    namespace BO
    {
        public class IdTakenException : Exception
        {
            public IdTakenException(int id)
            {
                Id = id;
            }
            override public string ToString()
            {
                return "The id: " + Id + "isn't available";
            }
            public int Id { get; set; }
        }
        public class IdNotExistException : Exception
        {
            public IdNotExistException(int id)
            {
                Id = id;
            }
            override public string ToString()
            {
                return "The id: " + Id + "isn't exist";
            }
            public int Id { get; set; }
        }
        public class DroneIsntVacant : Exception
        {
            public DroneIsntVacant(int id)
            {
                Id = id;
            }
            override public string ToString()
            {
                return "The drone with id: " + Id + "isn't vacant";
            }
            public int Id { get; set; }
        }
        public class DroneIsntMaintenance : Exception
        {
            public DroneIsntMaintenance(int id)
            {
                Id = id;
            }
            override public string ToString()
            {
                return "The drone with id: " + Id + "isn't maintenance";
            }
            public int Id { get; set; }
        }
        public class DroneIsntShipping : Exception
        {
            public DroneIsntShipping(int id)
            {
                Id = id;
            }
            override public string ToString()
            {
                return "The drone with id: " + Id + "isn't shipping";
            }
            public int Id { get; set; }
        }
        public class ParcelPickedUpOrIsntBinded : Exception
        {
            public ParcelPickedUpOrIsntBinded(int id)
            {
                Id = id;
            }
            override public string ToString()
            {
                return "The parcel with id:" + Id + " picked up or isn't binded";
            }
            public int Id { get; set; }
        }
        public class ParcelDeliveredOrNotPickedUp : Exception
        {
            public ParcelDeliveredOrNotPickedUp(int id)
            {
                Id = id;
            }
            override public string ToString()
            {
                return "The parcel with id:" + Id + " delivered of isn't picked up";
            }
            public int Id { get; set; }
        }
    }
}
