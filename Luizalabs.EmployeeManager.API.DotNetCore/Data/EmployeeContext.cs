using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luizalabs.EmployeeManager.API.DotNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Luizalabs.EmployeeManager.API.DotNetCore.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
        }
    }
}
