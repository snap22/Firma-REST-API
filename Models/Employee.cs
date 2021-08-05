using System;
using System.Collections.Generic;

#nullable disable

namespace FirmaRest.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Companies = new HashSet<Company>();
            Departments = new HashSet<Department>();
            Divisions = new HashSet<Division>();
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
