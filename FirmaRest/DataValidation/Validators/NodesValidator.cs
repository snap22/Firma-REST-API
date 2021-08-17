using FirmaRest.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.DataValidation.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {

        public CompanyValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);

            RuleFor(c => c.Code)
                .Length(4)
                .Matches("[a-zA-Z0-9,.]{4}");

            RuleFor(c => c.Director)
                .NotNull()
                .NotEmpty();
        }
    }

    public class DivisionValidator : AbstractValidator<DivisionDto>
    {

        public DivisionValidator()
        {
            RuleFor(d => d.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);

            RuleFor(d => d.Code)
                .Length(4)
                .Matches("[a-zA-Z0-9,.]{4}");

            RuleFor(d => d.CompanyId)
                .NotNull()
                .NotEmpty();

            RuleFor(d => d.Leader)
                .NotNull()
                .NotEmpty();
        }
    }

    public class ProjectValidator : AbstractValidator<ProjectDto>
    {

        public ProjectValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);

            RuleFor(p => p.Code)
                .Length(4)
                .Matches("[a-zA-Z0-9,.]{4}");

            RuleFor(p => p.DivisionId)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Leader)
                .NotNull()
                .NotEmpty();
        }
    }

    public class DepartmentValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentValidator()
        {
            RuleFor(d => d.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);

            RuleFor(d => d.Code)
                .Length(4)
                .Matches("[a-zA-Z0-9,.]{4}");

            RuleFor(d => d.ProjectId)
                .NotNull()
                .NotEmpty();

            RuleFor(d => d.Leader)
                .NotNull()
                .NotEmpty();
        }
    }
}
