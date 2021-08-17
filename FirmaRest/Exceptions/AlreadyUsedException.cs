using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Exceptions
{
    public class AlreadyUsedException : Exception
    {
        public AlreadyUsedException()
        {
        }

        public AlreadyUsedException(string message) : base(message)
        {
        }
    }
}
