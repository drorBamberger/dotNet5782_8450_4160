using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DAL
{
    public class DalFactory
    {
        static public DalApi.IDal GetDal(string str)
        {
            if(str == "DalObject")
            {
                return new DalObject.DalObject();
            }
            else if(str == "DalXml")
            {
                return new DalObject.DalObject();// return new DalObject.DalXml();
            }
            else
            {
                throw new dalTypeNotExist(str);
            }
        }
    }
}
