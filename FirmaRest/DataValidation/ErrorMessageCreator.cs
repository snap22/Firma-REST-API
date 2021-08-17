using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.DataValidation
{
    public class ErrorMessageCreator
    {
        public string CreateNotExistsMessage(string name, int id)
        {
            return string.Format("{0} with id: {1} does not exist!", name, id);
        }

        public string CreateEmployeeNotExistsMessage(int employeeId)
        {
            return CreateNotExistsMessage("Employee", employeeId);
        }

        public string CreateCompanyNotExistsMessage(int companyId)
        {
            return CreateNotExistsMessage("Company", companyId);
        }

        public string CreateDivisionNotExistsMessage(int divisionId)
        {
            return CreateNotExistsMessage("Division", divisionId);
        }

        public string CreateProjectNotExistsMessage(int projectId)
        {
            return CreateNotExistsMessage("Project", projectId);
        }

        public string CreateDepartmentNotExistsMessage(int departmentId)
        {
            return CreateNotExistsMessage("Department", departmentId);
        }

        public string CreateEmployeeDifferentCompanyMessage()
        {
            return "Employee is from a different company";
        }

        public string CreateEmployeeCannotBeDeletedMessage(int employeeId)
        {
            return string.Format("Employee id: {0} cannot be deleted, because he is the leader of at least one node.", employeeId);
        }

        public string CreateEmployeeCannotBeModifiedMessage(int employeeId)
        {
            return string.Format("Cannot change the company of employee with id: {0}, because he is the leader of at least one node.", employeeId);
        }

        public string CreateAlreadyUsedMessage(string type, string value)
        {
            return string.Format("{0}: {1} is already used", type, value);
        }

        public string CreateEmailAlreadyUsedMessage(string employeeEmail)
        {
            return CreateAlreadyUsedMessage("Email", employeeEmail);
        }

        public string CreateCodeAlreadyUsedMessage(string code)
        {
            return CreateAlreadyUsedMessage("Code", code);
        }
    }
}
