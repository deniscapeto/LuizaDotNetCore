using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luizalabs.EmployeeManager.API.DotNetCore.Models
{
    public class Employee
    {
        public long id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string department { get; set; }
    }
}
