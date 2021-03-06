using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

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
            return "The id: " + Id + " isn't available";
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
            return "The id: " + Id + " isn't exist";
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
            return "The drone with id: " + Id + " isn't vacant";
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
            return "The drone with id: " + Id + " isn't maintenance";
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
            return "The drone with id: " + Id + " isn't shipping";
        }
        public int Id { get; set; }
    }
    public class ParcelPickedUpOrIsntScheduled : Exception
    {
        public ParcelPickedUpOrIsntScheduled(int id)
        {
            Id = id;
        }
        override public string ToString()
        {
            return "The parcel with id: " + Id + " picked up or isn't Scheduled";
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
            return "The parcel with id: " + Id + " delivered or isn't picked up";
        }
        public int Id { get; set; }
    }
    public class CantBeNegative : Exception
    {
        public CantBeNegative(double num)
        {
            Num = num;
        }
        override public string ToString()
        {
            return "The number: " + Num + " must be positive!!!";
        }
        public double Num { get; set; }
    }

    public class DroneHaveToLittleBattery : Exception
    {
        public DroneHaveToLittleBattery(int id)
        {
            Id = id;
        }
        override public string ToString()
        {
            return "The drone with id: " + Id + " have too little battery to travel that far";
        }
        public int Id { get; set; }
    }

    public class NoParcelMatch : Exception
    {
        public NoParcelMatch(int id)
        {
            Id = id;
        }
        override public string ToString()
        {
            return "The drone with id: " + Id + " coudn't find any proper parcel";
        }
        public int Id { get; set; }
    }
    public class InvalidInput : Exception
    {
        override public string ToString()
        {
            return "Invalid Input";
        }
    }
}
