using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Exceptions
{
    public class CannotDeleteException : Exception
    {
        public CannotDeleteException()
        {
        }

        public CannotDeleteException(string message) : base(message)
        {
        }
    }
}
