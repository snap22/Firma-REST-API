using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Exceptions
{
    public class NotExistsException : Exception
    {
        public NotExistsException()
        {
        }

        public NotExistsException(string message) : base(message)
        {
        }
    }
}
