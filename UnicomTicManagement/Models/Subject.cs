﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Models
{
   public class Subject
    {
        public int SuId { get; set; }
        public string SuName { get; set; }
        public int CId { get; set; } // Foreign key
        public string  CName { get; set; } // Navigation property





    }
}
