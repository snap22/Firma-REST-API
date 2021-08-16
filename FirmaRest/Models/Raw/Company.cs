using System;
using System.Collections.Generic;

#nullable disable

namespace FirmaRest.Models
{
    public partial class Company
    {
        public Company()
        {
            Divisions = new HashSet<Division>();
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int Director { get; set; }

        public virtual Employee DirectorNavigation { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
