using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mumu_Server.Models;
using Mumu_Server.Models.Types;
using Mumu_Server.Query;
using Mumu_Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mumu_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MumuDbContext context;
        private readonly IConfiguration configuration;

        public MemberController(MumuDbContext _context, IConfiguration _configuration)
        {
            this.context = _context;
            this.configuration = _configuration;
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

                var token = CreateToken(foundMember);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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

        private string CreateToken(Member member)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,member.Username.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
