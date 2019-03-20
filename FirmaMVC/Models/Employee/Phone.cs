using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaMVC.Models.Employee
{
    public class Phone
    {
        public int PhoneId { get; set; }
        public int PhoneNumber { get; set; }

        public virtual Workplace Workplace { get; set; }
    }
}
