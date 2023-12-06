using Async_Demo.DataBase;
using Async_Demo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly TestDb1Context _context1;
        private readonly TestDb2Context _context2;
        private readonly TestDb3Context _context3;
        public EmployeeController(TestDb1Context context1, TestDb2Context context2, TestDb3Context context3)
        {
            _context1 = context1;
            _context2 = context2;
            _context3 = context3;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees1 = await _context1.Employee.ToListAsync();
            var employees2 = await _context2.Employee.ToListAsync();
            var employees3 = await _context3.Employee.ToListAsync();

            var allEmployees = employees1.Concat(employees2).Concat(employees3).ToList();

            return Ok(allEmployees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context1.Employee.Add(employee);
            _context2.Employee.Add(employee);
            _context3.Employee.Add(employee);

            await Task.WhenAll(
                _context1.SaveChangesAsync(),
                _context2.SaveChangesAsync(),
                _context3.SaveChangesAsync()
            );

            return Ok();
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            // Check if the provided ID matches the employee's ID
            if (id != employee.Employee_PK)
            {
                return BadRequest("ID mismatch.");
            }

            // Update the employee in all three databases
            _context1.Entry(employee).State = EntityState.Modified;
            _context2.Entry(employee).State = EntityState.Modified;
            _context3.Entry(employee).State = EntityState.Modified;

            try
            {
                await Task.WhenAll(
                    _context1.SaveChangesAsync(),
                    _context2.SaveChangesAsync(),
                    _context3.SaveChangesAsync()
                );
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            // Find the employee by ID in one of the databases (assuming the ID is the same in all databases)
            var employee = await _context1.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Remove the employee from all three databases
            _context1.Employee.Remove(employee);
            _context2.Employee.Remove(employee);
            _context3.Employee.Remove(employee);

            await Task.WhenAll(
                _context1.SaveChangesAsync(),
                _context2.SaveChangesAsync(),
                _context3.SaveChangesAsync()
            );

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            // Check if an employee with the given ID exists in one of the databases (assuming the ID is the same in all databases)
            return _context1.Employee.Any(e => e.Employee_PK == id);
        }
  

        
    }
}
