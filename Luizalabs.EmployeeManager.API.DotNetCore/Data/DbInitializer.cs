using Luizalabs.EmployeeManager.API.DotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luizalabs.EmployeeManager.API.DotNetCore.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EmployeeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var employees = new Employee[]
            {
                new Employee{name="Rodrigo Carvalho",department="IntegraCommerce",email="rodrigo@luizalabs.com"},
                new Employee{name="Renato Pedigoni",department="Digital Platform",email="renato@luizalabs.com"},
                new Employee{name="Thiago Catoto",department="Mobile",email="catoto@luizalabs.com"},
            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();           
        }
    }
}
