using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void DroneUpdate(int id, string name)
        {
            Drones.Find(x => x.Id == id).Model = name;
        }

    }
}
