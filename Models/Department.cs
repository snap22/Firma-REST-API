using System;
using System.Collections.Generic;

#nullable disable

namespace FirmaRest.Models
{
    public partial class Department
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int Leader { get; set; }
        public int ProjectId { get; set; }

        public virtual Employee LeaderNavigation { get; set; }
        public virtual Project Project { get; set; }
    }
}
