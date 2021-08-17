using FirmaRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.DataValidation
{
    public class ContextChecker
    {
        private readonly RestDBContext _context;

        public ContextChecker(RestDBContext context)
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

        public bool CompanyCodeExists(string code)
        {
            return _context.Companies.Any(e => e.Code == code);
        }

        public bool DivisionCodeExists(string code)
        {
            return _context.Divisions.Any(e => e.Code == code);
        }

        public bool ProjectCodeExists(string code)
        {
            return _context.Projects.Any(e => e.Code == code);
        }

        public bool DepartmentCodeExists(string code)
        {
            return _context.Departments.Any(e => e.Code == code);
        }

        public bool EmployeeInTheCompany(int employeeId, int companyId)
        {
            return _context.Employees.Any(e => e.Id == employeeId && e.CompanyId == companyId);
        }

        public bool EmployeeIsUnemployed(int employeeId)
        {
            return _context.Employees.Any(e => e.Id == employeeId && e.CompanyId == null);
        }

        public bool EmplyeeEmailExists(string employeeEmail)
        {
            return _context.Employees.Any(e => e.Email == employeeEmail);
        }

        public bool EmployeeIsLeaderOfAnyNode(int employeeId)
        {
            bool isLeader = false;
            // ak je nezamestnany, ihned vyhodi false
            if (EmployeeIsUnemployed(employeeId))
                return false;

            isLeader |= _context.Companies.Any(c => c.Director == employeeId);
            isLeader |= _context.Divisions.Any(d => d.Leader == employeeId);
            isLeader |= _context.Projects.Any(p => p.Leader == employeeId);
            isLeader |= _context.Departments.Any(d => d.Leader == employeeId);

            return isLeader;
        }
    }
}
