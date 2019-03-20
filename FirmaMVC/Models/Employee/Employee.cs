using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaMVC.Models.Employee
{
    public class Employee
    {
        [Key]
        public int EmployyeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual EmployeePosition EmployeePosition { get; set; }

    }
}
