using FirmaRest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Repository
{
    public interface IEmployeeRepository
    {
        public Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees();
        public Task<ActionResult<EmployeeDto>> GetEmployeeById(int id);
        public Task<ActionResult<IEnumerable<EmployeeDto>>> GetUnemployed();
        public Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeDto employeeDto);
        public Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto);
        public Task<ActionResult<EmployeeDto>> DeleteEmployee(int id);
        public bool CheckIfEmployeeExists(int id);

    }
}
