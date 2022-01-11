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
                return DalObject.DalObject.Instance;
            }
            else if(str == "DalXml")
            {
                return DalXml.DalXml.Instance;
            }
            else
            {
                throw new dalTypeNotExist(str);
            }
        }
    }
}
