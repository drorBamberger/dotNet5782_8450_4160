using System;

namespace DAL
{
    namespace DO
    {
        public class DataSource
        {
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}

