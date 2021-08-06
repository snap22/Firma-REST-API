using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Models
{
    public class ModelConverter
    {
        #region employee
        public static Employee ConvertEmployeeDTOToEmployee(EmployeeDto employeeDto)
        {
            return new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Contact = employeeDto.Contact,
                Title = employeeDto.Title,
                CompanyId = employeeDto.CompanyId,
            };
        }
        public static EmployeeDto ConvertEmployeeToEmployeeDTO(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Title = employee.Title,
                Contact = employee.Contact,
                CompanyId = employee.CompanyId

            };
        }
        #endregion

        #region company
        public static Company ConvertCompanyDTOToCompany(CompanyDto companyDto)
        {
            return new Company
            {
                Title = companyDto.Title,
                Code = companyDto.Code,
                Director = companyDto.Director
            };
        }

        public static CompanyDto ConvertCompanyToCompanyDTO(Company company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                Title = company.Title,
                Code = company.Code,
                Director = company.Director
            };
        }

        #endregion company

        #region division

        #endregion division

        #region project

        #endregion project

        #region department

        #endregion department

    }
}
