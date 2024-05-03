using BackIngE_N.BD;
using BackIngE_N.Config.Jwt;
using BackIngE_N.Config.Messages;
using BackIngE_N.DTO.UserrDto;
using Microsoft.EntityFrameworkCore;
using BackIngE_N.Models;
using _bCrypt = BCrypt.Net.BCrypt;

namespace BackIngE_N.Logic.User {
    public class UserrLogic {

        private readonly IngenieriaeynContext _context;
        public readonly JwtConfig _jwtConfig;

        public UserrLogic(IngenieriaeynContext context, JwtConfig jwtConfig) {
            _context = context;
            _jwtConfig = jwtConfig;
        }

        public async Task<Response> Login(UserBase user) {
            Userr u = await _context.Userrs.Where(u => u.Email == user.Email).FirstOrDefaultAsync() ?? throw new Exception(UserrMessages.ErrorMessages.USERNOTFOUND);

            if (u == null) throw new Exception(UserrMessages.ErrorMessages.USERNOTFOUND);

            Response r = _jwtConfig.generateToken(user, u);

            if (r.Success && r.Message.Equals(GeneralMessages.TOKENGENERATED)) {
                await UpdateToken(user.Email, ((string)r.Data));
            }

            return r;
        }

        private async Task<bool> UpdateToken(string email, string token) {

            Userr u = await _context.Userrs.Where(u => u.Email == email).FirstOrDefaultAsync() ?? throw new Exception(UserrMessages.ErrorMessages.USERNOTFOUND);

            u.Token = token;

            _context.Userrs.Update(u);

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<Response> Register(UserDTO user) {

            if (await _context.Userrs.Where(u => u.Email == user.Email).FirstOrDefaultAsync() != null) throw new Exception(UserrMessages.ErrorMessages.USERALREDYEXIST);

            Userr u = new() {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Password = _bCrypt.HashPassword(user.Password),
                Role = "User",
                Token = null
            };

            _context.Userrs.Add(u);

            return await _context.SaveChangesAsync() > 0 ? new Response(UserrMessages.SuccessMessages.USERCREATED, true) : new Response(UserrMessages.ErrorMessages.USERNOTCREATED, false);

        }
    }
}
