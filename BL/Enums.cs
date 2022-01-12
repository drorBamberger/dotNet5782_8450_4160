using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace BO
{
    public enum WeightCategories
    {
        Light = 0,
        Medium,
        Heavy
    }


    public enum Priorities
    {
        Normal = 0,
        Fast,
        Emergency
    }

    public enum DroneStatuses
    {
        vacant = 0,
        maintenance,
        Shipping
    }

    public enum ParcelStatuses
    {
        Reuqested = 0,
        Scheduled,
        PickedUp,
        Delivered
    }
    public enum ParcelOnDeliveryStatuses
    {
        Waiting = 0,
        onTheWay
    }

    public enum UpdateFuncs
    {

    }
}

