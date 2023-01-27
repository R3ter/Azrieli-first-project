using API.Data;
using API.DOTs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext context;
        public DepartmentController(DataContext context)
        {
            this.context = context;
        }
        public async Task<ObjectResult> get()
        {
            var result = await (from a in context.Departments
                                join c in context.Employees on a.ManagerID equals c.Id into s
                                from c in s.DefaultIfEmpty()
                                select new
                                {
                                    Department = a,
                                    Employee = c != null ? new { id = c.Id, firstName = c.FirstName, lastName = c.LastName } : null

                                }).ToListAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut()]
        public async Task<int> update([FromBody] Department dot)
        {
            return await context.Departments.Where(a => a.Id == dot.Id).ExecuteUpdateAsync(b =>
        b.SetProperty(a => a.Name, dot.Name).SetProperty(a => a.ManagerID, dot.ManagerID));
        }
        [HttpPost()]
        public async Task<bool> add([FromBody] DepartmentDot dot)
        {
            try
            {
                await context.Departments.AddAsync(new Department { Name = dot.Name, ManagerID = dot.ManagerID });
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;

            }
            return true;
        }
        [HttpDelete()]
        public async Task<bool> delete([FromBody] DepartmentDot dot)
        {
            var department = await context.Departments.SingleOrDefaultAsync(item => item.Id == dot.id);

            if (department != null)
            {
                context.Departments.Remove(department);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
