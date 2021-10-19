using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Context
{
    public class EmployeeContext: DbContext
    {
       public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<User> User { get; set; }
    }
}
