using System;
using System.Collections.Generic;

#nullable disable

namespace FirmaRest.Models
{
    public partial class Project
    {
        public Project()
        {
            Departments = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int Leader { get; set; }
        public int DivisionId { get; set; }

        public virtual Division Division { get; set; }
        public virtual Employee LeaderNavigation { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
