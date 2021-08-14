using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Exceptions
{
    [Serializable]
    public class PublihserNameException:Exception
    {
        public string PulisherName { get; set; }


        public PublihserNameException()
        {

        }

        public PublihserNameException(string message):base(message)
        {

        }
        public PublihserNameException(string message,Exception inner):base(message,inner)
        {

        }

        public PublihserNameException(string message,string publihserName):this(message)
        {
            PulisherName = publihserName;
        }

      
    }
}
