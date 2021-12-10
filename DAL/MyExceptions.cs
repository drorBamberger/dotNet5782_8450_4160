using System;
using System.Collections.Generic;
using System.Text;


namespace DO
{
    public class IdTakenException : Exception
    {
        public IdTakenException(int id)
        {
            Id = id;
        }
        override public string ToString()
        {
            return "The id: " + Id + "isn't available";
        }
        public int Id { get; set; }
    }
    public class IdNotExistException : Exception
    {
        public IdNotExistException(int id)
        {
            Id = id;
        }
        override public string ToString()
        {
            return "The id: " + Id + "isn't exist";
        }
        public int Id { get; set; }
    }

    public class dalTypeNotExist : Exception
    {
        public dalTypeNotExist(string id)
        {
            dalType = id;
        }
        override public string ToString()
        {
            return "The id: " + dalType + "isn't exist";
        }
        public string dalType { get; set; }
    }
}

