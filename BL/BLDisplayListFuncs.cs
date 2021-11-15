using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public IEnumerable<StationForList> StationList()
        {
            IEnumerable<IDAL.DO.Station> original = MyDal.StationList();
            List<StationForList> comeBack = new List<StationForList>();
            int counter = 0;
            
            foreach(var item in original)
            {
                foreach (var drone in Drones)
                {
                    if (drone.MyLocation == new Location(item.Longitude, item.Lattitude))
                        counter++;
                        
                }
                comeBack.Add(new StationForList(item.Id, item.Name, item.ChargeSlots, counter));
                counter = 0;
            }
            return comeBack;
        }
    }
}
