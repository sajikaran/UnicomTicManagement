using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Models
{
    public  class Exam
    {
        public int ExId { get; set; }
        public string ExName { get; set; }

       public int SuId { get; set; } // Foreign key
        public string SuName { get; set; }

    }
}
