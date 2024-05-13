using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mumu_Server.Models;
using Mumu_Server.Models.Types;
using Mumu_Server.Query;
using Mumu_Server.Services;

namespace Mumu_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MumuDbContext context;

        public MemberController(MumuDbContext _context, IConfiguration _configuration)
        {
            this.context = _context;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            try
            {
                string res = await (new MemberFactory()).SignIn(context, model);

                if (res == "註冊成功")
                {
                    return Ok("註冊成功");
                }

                return BadRequest(res);
            }
            catch (Exception)
            {
                return BadRequest("500 Internal Server Error");
                throw;
            }
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            try
            {
                Member? foundMember = await context.Members.FirstOrDefaultAsync(a => a.Username == model.username);

                if (foundMember == null) return BadRequest("帳號或密碼錯誤，請確認您輸入的資訊是否正確");

                if (!Hash.VerifyHashedPassword(model.password, foundMember.Password))
                {
                    return BadRequest("帳號或密碼錯誤，請確認您輸入的資訊是否正確");
                }

                return Ok("登入成功");
            }
            catch (Exception)
            {
                return BadRequest("500 Internal Server Error");
                throw;
            }
        }

        [HttpGet]
        public IEnumerable<Member> AllMember()
        {
            try
            {
                var result = from a in context.Members
                             select a;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
