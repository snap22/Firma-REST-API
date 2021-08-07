using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Exceptions
{
    public class EmployeeDifferentCompanyException : Exception
    {
        public EmployeeDifferentCompanyException()
        {
        }

        public EmployeeDifferentCompanyException(string message) : base(message)
        {
        }
    }
}
