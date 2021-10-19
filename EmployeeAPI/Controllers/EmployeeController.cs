using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Context;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        readonly EmployeeContext _employeeContext;
        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            var data = _employeeContext.Employee.ToList();
            return data;
        }
        [HttpGet("{id}")]
        public IEnumerable<Employee> GetEmployeesById(int id)
        {
            var data = _employeeContext.Employee.Where(x => x.EmpId == id);
            return data;
        }
        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            _employeeContext.Employee.Add(employee);
            _employeeContext.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            _employeeContext.Employee.Update(employee);
            _employeeContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var data = _employeeContext.Employee.Where(x => x.EmpId == id).FirstOrDefault();
            _employeeContext.Employee.Remove(data);
            _employeeContext.SaveChanges();
            return Ok();
        }

    }
}
