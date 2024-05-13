using Microsoft.EntityFrameworkCore;
using Mumu_Server.Models;
using Mumu_Server.Models.Types;
using Mumu_Server.Services;

namespace Mumu_Server.Query
{
    public class MemberFactory
    {
        public async Task<string> SignIn(MumuDbContext _context, SignUpModel model)
        {
            Member? foundMember = await _context.Members.FirstOrDefaultAsync(a => a.Username == model.username);

            if (foundMember != null)
            {
                return "帳號已被註冊";
            }

            Member newMember = new Member();
            newMember.Nickname = model.nickname;
            newMember.Username = model.username;
            newMember.Email = model.email;
            newMember.RegistrationTime = DateTime.Now;

            string hashedPassword = Hash.HashPassword(model.password);

            newMember.Password = hashedPassword;

            _context.Members.Add(newMember);
            await _context.SaveChangesAsync();

            return "註冊成功";
        }
    }
}
