using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Exceptions
{
    public class CannotModifyException : Exception
    {
        public CannotModifyException()
        {
        }

        public CannotModifyException(string message) : base(message)
        {
        }
    }
}
