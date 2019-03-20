using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaMVC.Models.Employee
{
    public class EmployeePosition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Department Department { get; set; }
        public virtual Workplace Workplace { get; set; }
    }
}
