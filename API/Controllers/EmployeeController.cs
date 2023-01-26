
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext context;
        public EmployeeController(DataContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Employee>> get()
        {
            return await context.Employees.Include(e => e.department).ToListAsync();
        }
        [HttpPut()]
        public async Task<int> update([FromBody] Employee dot)
        {
            return await context.Employees.Where(a => a.Id == dot.Id).ExecuteUpdateAsync(b =>
        b.SetProperty(a => a.FirstName, dot.FirstName).SetProperty(a => a.LastName, dot.LastName).SetProperty(a => a.DepartmentID, dot.DepartmentID)
        .SetProperty(a => a.StartWorkYear, dot.StartWorkYear));
        }
        [HttpPost()]
        public async Task<bool> add([FromBody] Employee dot)
        {
            try
            {
                await context.Employees.AddAsync(dot);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;

            }
            return true;
        }
        [HttpDelete()]
        public async Task<bool> delete([FromBody] Employee dot)
        {
            var department = await context.Employees.SingleOrDefaultAsync(item => item.Id == dot.Id);

            if (department != null)
            {
                context.Employees.Remove(department);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}