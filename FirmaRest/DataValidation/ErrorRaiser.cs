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

        public void RaiseErrorIfCompanyDoesntExist(int? companyId)
        {
            if (companyId != null && !_checker.CompanyExists(companyId.Value))
                throw new NotExistsException(_messageCreator.CreateCompanyNotExistsMessage(companyId.Value));
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

        public void RaiseErrorIfEmployeeEmailAlreadyInUse(string employeeEmail)
        {
            if (_checker.EmplyeeEmailExists(employeeEmail))
                throw new AlreadyUsedException(_messageCreator.CreateEmailAlreadyUsedMessage(employeeEmail));
        }

        public void RaiseErrorIfCompanyCodeAlreadyInUse(string code)
        {
            if (_checker.CompanyCodeExists(code))
                throw new AlreadyUsedException(_messageCreator.CreateCodeAlreadyUsedMessage(code));
        }

        public void RaiseErrorIfDivisionCodeAlreadyInUse(string code)
        {
            if (_checker.DivisionCodeExists(code))
                throw new AlreadyUsedException(_messageCreator.CreateCodeAlreadyUsedMessage(code));
        }

        public void RaiseErrortIfProjecCodeAlreadyInUse(string code)
        {
            if (_checker.ProjectCodeExists(code))
                throw new AlreadyUsedException(_messageCreator.CreateCodeAlreadyUsedMessage(code));
        }

        public void RaiseErrorIfDepartmentCodeAlreadyInUse(string code)
        {
            if (_checker.DepartmentCodeExists(code))
                throw new AlreadyUsedException(_messageCreator.CreateCodeAlreadyUsedMessage(code));
        }
    }
}
