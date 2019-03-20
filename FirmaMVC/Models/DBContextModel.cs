using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaMVC.Models
{
    public class DBContextModel : DbContext
    {
        public DbSet<Employee.Address> Address { get; set; }
        public DbSet<Employee.Department> Department { get; set; }
        public DbSet<Employee.Employee> Employee { get; set; }
        public DbSet<Employee.Phone> Phone { get; set; }
        public DbSet<Employee.Room> Room { get; set; }
        public DbSet<Employee.Workplace> Workplace { get; set; }
        public DbSet<Employee.EmployeePosition> EmployeePosition { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=FirmaMVC;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

    }
}
