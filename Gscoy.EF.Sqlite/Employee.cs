using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Gscoy.EF.Sqlite
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class NorthwindContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
