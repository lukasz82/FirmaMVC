using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaMVC.Models.Employee
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }

        public virtual Workplace Workplace { get; set; }
    }
}
