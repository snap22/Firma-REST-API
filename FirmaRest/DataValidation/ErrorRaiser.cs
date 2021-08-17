using FirmaRest.Exceptions;
using FirmaRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.DataValidation
{
    public class ErrorRaiser
    {
        private readonly ContextChecker _checker;
        private readonly ErrorMessageCreator _messageCreator;

        public ErrorRaiser(RestDBContext context)
        {
            _checker = new ContextChecker(context);
            _messageCreator = new ErrorMessageCreator();
        }

        public void RaiseErrorIfEmployeeDoesntExist(int employeeId)
        {
            if (!_checker.EmployeeExists(employeeId))
                throw new NotExistsException(_messageCreator.CreateEmployeeNotExistsMessage(employeeId));
        }

        public void RaiseErrorIfEmployeeLeaderOfAnyNode(int employeeId, bool toDelete = true)
        {
            if (_checker.EmployeeIsLeaderOfAnyNode(employeeId))
            {
                if (toDelete)
                    throw new CannotDeleteException(_messageCreator.CreateEmployeeCannotBeDeletedMessage(employeeId));
                else
                    throw new CannotModifyException(_messageCreator.CreateEmployeeCannotBeModifiedMessage(employeeId));
            }
        }

        public void RaiseErrorIfCompanyDoesntExist(int companyId)
        {
            if (!_checker.CompanyExists(companyId))
                throw new NotExistsException(_messageCreator.CreateCompanyNotExistsMessage(companyId));
        }

        public void RaiseErrorIfDivisionDoesntExist(int divisionId)
        {
            if (!_checker.DivisionExists(divisionId))
                throw new NotExistsException(_messageCreator.CreateDivisionNotExistsMessage(divisionId));
        }
        public void RaiseErrorIfProjectDoesntExist(int projectId)
        {
            if (!_checker.ProjectExists(projectId))
                throw new NotExistsException(_messageCreator.CreateProjectNotExistsMessage(projectId));
        }
        public void RaiseErrorIfDepartmentDoesntExist(int departmentId)
        {
            if (!_checker.DepartmentExists(departmentId))
                throw new NotExistsException(_messageCreator.CreateDepartmentNotExistsMessage(departmentId));
        }

        public void RaiseErrorIfEmployeeNotInTheCompany(int employeeId, int companyId)
        {
            if (!_checker.EmployeeInTheCompany(employeeId, companyId))
                throw new EmployeeDifferentCompanyException(_messageCreator.CreateEmployeeDifferentCompanyMessage());
        }

        public void RaiseErrorIfEmployeeNotUnemployed(int employeeId)
        {
            if (!_checker.EmployeeIsUnemployed(employeeId))
                throw new EmployeeDifferentCompanyException(_messageCreator.CreateEmployeeDifferentCompanyMessage());
        }

    }
}
