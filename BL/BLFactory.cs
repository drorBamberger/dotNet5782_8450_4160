using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;


namespace BL
{
    public class BLFactory
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BLApi.IBL GetBL()
        {
            return new BL();
        }
    }
}
