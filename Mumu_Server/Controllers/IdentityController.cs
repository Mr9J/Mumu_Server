using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mumu_Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Mumu_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private const string TokenSecret = "Ave Mujica";
        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);


        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] TokenGenerationRequest req)
        {

            return Ok();
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes(TokenSecret);

            //var claims = new List<Claim>
            //{
            //    new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            //    new(JwtRegisteredClaimNames.Sub,req.Email),
            //    new(JwtRegisteredClaimNames.Email,req.Email),
            //    new("userId",req.Username.ToString())
            //};

            //foreach (var claimPair in req.CustomClaims)
            //{
            //    var jsonElement = (JsonElement)claimPair.Value;
            //    var valueType = jsonElement.ValueKind switch
            //    {
            //        JsonValueKind.True => ClaimValueTypes.Boolean,
            //        JsonValueKind.False => ClaimValueTypes.Boolean,
            //        JsonValueKind.Number => ClaimValueTypes.Double,
            //        _ => ClaimValueTypes.String
            //    };

            //    var claim = new Claim(claimPair.Key, claimPair.Value.ToString()!, valueType);
            //    claims.Add(claim);
            //}

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow,
            //};
        }
        
    }
}
