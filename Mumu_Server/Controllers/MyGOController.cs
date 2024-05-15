using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mumu_Server.Models;

namespace Mumu_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyGOController : ControllerBase
    {
        private readonly MumuDbContext context;

        public MyGOController(MumuDbContext context)
        {
            this.context = context;
        }

        [HttpGet, Authorize]
        public ActionResult IsAuth()
        {
            return Ok(true);
        }
    }
}
