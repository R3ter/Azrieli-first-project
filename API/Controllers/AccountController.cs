using API.Data;
using API.DOTs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext context;
        public AccountController(DataContext context)
        {
            this.context = context;
        }
        [HttpPost("login")]
        public async Task<bool> Login(LoginDot login)
        {
            var user = await context.Users.AnyAsync(x => x.UserName == login.username && x.Password == login.password);
            return user;
        }
    }
}