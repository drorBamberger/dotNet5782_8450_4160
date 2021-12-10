using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DAL
{
    public class DalFactory
    {
        public DalApi.IDal GetDal(string str)
        {
            if(str == "DalObject")
            {
                return new DalObject.DalObject();
            }
            else if(str == "DalXml")
            {
                return new DalObject.xml;
            }
            else
            {
                throw new dalTypeNotExist(str);
            }
        }
    }
}
