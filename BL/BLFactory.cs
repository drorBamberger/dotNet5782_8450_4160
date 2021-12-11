using System;
using System.Collections.Generic;
using System.Text;


namespace BL
{
    public class BLFactory
    {
        public BLApi.IBL GetBL()
        {
            return new BL();
        }
    }
}
