using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mumu_Server.Models;
using Mumu_Server.Models.Types;
using Mumu_Server.Query;
using Mumu_Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MumuDbContext context;
        private readonly IConfiguration config;

        public MemberController(MumuDbContext _context, IConfiguration _configuration)
        {
            this.context = _context;
            this.config = _configuration;
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
        public async Task<IActionResult> SignIn(SignInModel req)
        {
            try
            {
                Member? foundMember = await context.Members.FirstOrDefaultAsync(x => x.Username == req.username);

                if (foundMember == null) return BadRequest("帳號或密碼錯誤，請確認您輸入的資訊是否正確");

                if (!Hash.VerifyHashedPassword(req.password, foundMember.Password))
                {
                    return BadRequest("帳號或密碼錯誤，請確認您輸入的資訊是否正確");
                }

                var token = CreateToken(req);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        private string CreateToken(SignInModel user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                config.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
