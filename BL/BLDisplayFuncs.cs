using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        //displays
        public string DisplayStation(int id)
        {
            return GetStation(id).ToString();
        }

        public string DisplayDrone(int id)
        {
            return GetDrone(id).ToString();
        }

        public string DisplayCustomer(int id)
        {
            IDAL.DO.Customer StructToClass = MyDal.DisplayCustomer(id);
            List<ParcelForCustomer> fromCustomer = new List<ParcelForCustomer>;
            List<ParcelForCustomer> toCustomer = new List<ParcelForCustomer>;
            foreach(var item in MyDal.ParcelList())
            {
                if(item.SenderId == id)
                {
                    fromCustomer.Add(new ParcelForCustomer(item.Id, (WeightCategories)item.Weight), (Priorities)item.Priority, , new CustomerInParcel(item.TargetId, ))
                }
                if(item.TargetId == id)
                {

                }
            }


            return new Customer(StructToClass.Id, StructToClass.Name, StructToClass.Phone, new Location(StructToClass.Longitude, StructToClass.Lattitude), ).ToString;
        }
    }
}
