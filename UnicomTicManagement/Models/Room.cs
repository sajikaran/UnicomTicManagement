using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Models
{
    public class Room
    {
        public int RId { get; set; }
        public string RName { get; set; }

        public int TId { get; set; }

        public string TimeSlot { get; set; }

        public int SuId { get; set; }
        public string SuName { get; set; }

    }
}
