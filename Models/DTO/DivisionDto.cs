﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Models.DTO
{
    public class DivisionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int Leader { get; set; }
        public int CompanyId { get; set; }
    }
}
