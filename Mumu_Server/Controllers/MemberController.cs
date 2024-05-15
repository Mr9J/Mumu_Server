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
using System.Collections;

namespace Mumu_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MumuDbContext context;
        private readonly IConfiguration config;

        public MemberController(MumuDbContext _context, IConfiguration _config)
        {
            this.context = _context;
            this.config = _config;
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

                string message = "登入成功";
                string user = req.username;
                string token = "";

                Admin? foundAdmin = await context.Admins.FirstOrDefaultAsync(x => x.MemberId == foundMember.MemberId);

                if (foundAdmin == null)
                {
                    token = "Bearer " + CreateToken(req, "User");
                }
                else
                {
                    token= "Bearer " + CreateToken(req, "Admin");
                }

                return Ok(new { message, user, token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpGet("current-user"), Authorize]
        public async Task<IActionResult> CurrentUser()
        {
            string username = GetJwtPayload(ClaimTypes.Name);

            Member? member = await context.Members.FirstOrDefaultAsync(x => x.Username == username);

            if (member != null)
            {
                CurrentUserModel cu = new CurrentUserModel
                {
                    id = member.MemberId,
                    nickname = member.Nickname,
                    email = member.Email,
                    username = member.Username,
                    role = GetJwtPayload(ClaimTypes.Role),
                };

                return Ok(cu);
            }

            return BadRequest();
        }

        private string GetJwtPayload(string claimType)
        {
            var jwtToken = HttpContext.Request.Headers["Authorization"].ToString()
                            .Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadJwtToken(jwtToken);

            var payload = jsonToken.Payload;

            var claims = payload.Claims;

            var specificClaim = payload.Claims.FirstOrDefault(a => a.Type == claimType)?.Value;

            if (specificClaim != null)
            {
                return specificClaim;
            }

            return "Not found";
        }

        private string CreateToken(SignInModel user, string role)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, role),
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
