using FirmaRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.DataContext
{
    public class ContextChecker
    {
        private readonly TestDBContext _context;

        public ContextChecker(TestDBContext context)
        {
            _context = context;
        }

        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        public bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }

        public bool DivisionExists(int id)
        {
            return _context.Divisions.Any(e => e.Id == id);
        }

        public bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        public bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }

        public bool EmployeeInTheCompany(int employeeId, int companyId)
        {
            return _context.Employees.Any(e => e.Id == employeeId && e.CompanyId == companyId);
        }

        public bool EmployeeIsLeaderOfAnyNode(int employeeId)
        {
            bool isLeader = false;
            // ak je nezamestnany, ihned vyhodi false
            if (_context.Employees.Any(e => e.Id == employeeId && e.CompanyId == null))
                return isLeader;

            isLeader |= _context.Companies.Any(c => c.Director == employeeId);
            isLeader |= _context.Divisions.Any(d => d.Leader == employeeId);
            isLeader |= _context.Projects.Any(p => p.Leader == employeeId);
            isLeader |= _context.Departments.Any(d => d.Leader == employeeId);

            return isLeader;
        }
    }
}
