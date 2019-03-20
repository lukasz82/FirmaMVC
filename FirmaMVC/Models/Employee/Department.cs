using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaMVC.Models.Employee
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int Hierarchy { get; set; }
        public int Parent { get; set; }

        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }
    }
}
