
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
    }
}