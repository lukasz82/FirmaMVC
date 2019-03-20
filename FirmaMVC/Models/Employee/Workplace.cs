using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaMVC.Models.Employee
{
    public class Workplace
    {
        public int WorkPlaceId { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public float YearSalary { get; set; }

        [ForeignKey("EmployeePosition")]
        public int EmplPositionId { get; set; }
        [ForeignKey("Phone")]
        public int PhoneId { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public virtual Phone Phone { get; set; }
        public virtual Room Room { get; set; }
        public virtual EmployeePosition EmployeePosition { get; set; }
    }
}
