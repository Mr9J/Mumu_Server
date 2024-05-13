using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mumu_Server.Models;
using Mumu_Server.Models.Types;
using Mumu_Server.Services;

namespace Mumu_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MumuDbContext context;

        public MemberController(MumuDbContext _context)
        {
            this.context = _context;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel model)
        {
            try
            {
                Member? foundMember = await context.Members.FirstOrDefaultAsync(a => a.Username == model.username);

                if (foundMember != null)
                {
                    return BadRequest("帳號已被註冊");
                }

                Member newMember = new Member();
                newMember.Nickname = model.nickname;
                newMember.Username = model.username;
                newMember.Email = model.email;
                newMember.RegistrationTime = DateTime.Now;

                string hashedPassword = Hash.HashPassword(model.password);

                newMember.Password = hashedPassword;

                context.Members.Add(newMember);
                await context.SaveChangesAsync();

                return Ok("註冊成功");
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
    }
}
