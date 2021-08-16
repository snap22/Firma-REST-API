using System;
using System.Collections.Generic;

#nullable disable

namespace FirmaRest.Models
{
    public partial class Division
    {
        public Division()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int Leader { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Employee LeaderNavigation { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
