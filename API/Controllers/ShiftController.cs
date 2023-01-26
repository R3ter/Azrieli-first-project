
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly DataContext context;
        public ShiftController(DataContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<EmployeeShift>> get()
        {
            return await context.EmployeeShifts.Include(e => e.Employee).Include(e => e.Shift).ToListAsync();
        }
        [HttpPut()]
        public async Task<int> update([FromBody] Shift dot)
        {
            return await context.Shifts.Where(a => a.Id == dot.Id).ExecuteUpdateAsync(b => b.SetProperty(a => a.StartTime, dot.StartTime)
            .SetProperty(a => a.EndTime, dot.EndTime)
            .SetProperty(a => a.Date, dot.Date));
        }
        [HttpPost()]
        public async Task<bool> add([FromBody] Shift dot)
        {
            try
            {
                await context.Shifts.AddAsync(dot);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;

            }
            return true;
        }
        [HttpDelete()]
        public async Task<bool> delete([FromBody] Shift dot)
        {
            var department = await context.Shifts.SingleOrDefaultAsync(item => item.Id == dot.Id);

            if (department != null)
            {
                context.Shifts.Remove(department);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
