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

        [HttpGet]
        public string MyGo()
        {
            int a = context.Members.ToList().Count();

            return "MyGO!" + a;
        }
    }
}
