using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public enum WeightCategories
        {
            Light,
            Medium,
            Heavy
        }


        public enum Priorities
        {
            Normal,
            Fast,
            Emergency
        }

        public enum DroneStatuses
        {
            vacant,
            maintenance,
            Shipping
        }

        public enum ParcelStatuses
        {
            Reuqested,
            Scheduled,
            PickedUp,
            Delivered
        }
        public enum ParcelOnDeliveryStatuses
        {
            Waiting,
            onTheWay
        }
    }
}
