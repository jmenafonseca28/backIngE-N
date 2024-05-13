using BackIngE_N.BD;
using BackIngE_N.Config.Jwt;
using BackIngE_N.Config.Messages;
using BackIngE_N.DTO.UserrDto;
using Microsoft.EntityFrameworkCore;
using BackIngE_N.Models;
using _bCrypt = BCrypt.Net.BCrypt;
using System.Net;
using Microsoft.Extensions.Options;

namespace BackIngE_N.Logic {
    public class UserrLogic {

        private readonly IngenieriaeynContext _context;
        private readonly JwtConfig _jwtConfig;
        private readonly SecurityLogic _securityLogic;

        public UserrLogic(IngenieriaeynContext context, JwtConfig jwtConfig, SecurityLogic securityLogic) {
            _context = context;
            _jwtConfig = jwtConfig;
            _securityLogic = securityLogic;
        }

        /// <summary>
        /// Logs in a user with the provided credentials.
        /// </summary>
        /// <param name="user">The user credentials.</param>
        /// <returns>A <see cref="Task{Response}"/> representing the asynchronous operation. The task result contains the login response.</returns>
        public async Task<Response> Login(UserBase user, IPAddress ip) {

            if (ip != null) {
                if (await _securityLogic.isBlockedIP(ip)) throw new Exception(UserrError.IPBLOCKED);
                await _securityLogic.ValidateIP(ip);
            }

            Userr u = await _context.Userrs.Where(u => u.Email == user.Email).FirstOrDefaultAsync() ?? throw new Exception(UserrError.USERNOTFOUND);

            if (user == null || !_bCrypt.Verify(user.Password, u.Password)) {
                await _securityLogic.SaveIP(ip, false);
                throw new Exception(UserrError.LOGINERROR);
            };

            await _securityLogic.SaveIP(ip, true);

            Response r = _jwtConfig.generateToken(user, u);

            if (r.Success && r.Message.Equals(GeneralMessages.TOKENGENERATED)) {
                _ = UpdateToken(user.Email, (string)r.Data);
            }

            return new Response(r.Message, r.Success,
                new UserResponse(u.Id, u.Name, u.LastName, u.Email, u.Role, (string)r.Data));
        }



        /// <summary>
        /// Updates the token for a user with the specified email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="token">The new token to be assigned.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the token update was successful.</returns>
        private async Task<bool> UpdateToken(string email, string token) {

            Userr u = await _context.Userrs.Where(u => u.Email == email).FirstOrDefaultAsync() ?? throw new Exception(UserrError.USERNOTFOUND);

            u.Token = token;

            _context.Userrs.Update(u);

            return await _context.SaveChangesAsync() > 0;

        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user information to register.</param>
        /// <returns>A <see cref="Task{Response}"/> representing the asynchronous operation. The task result contains a <see cref="Response"/> object indicating the success or failure of the registration.</returns>
        public async Task<Response> Register(UserDTO user) {

            if (await _context.Userrs.Where(u => u.Email == user.Email).FirstOrDefaultAsync() != null) throw new Exception(UserrError.USERALREDYEXIST);

            Userr u = new() {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Password = _bCrypt.HashPassword(user.Password),
                Role = "User",
                Token = null
            };

            _context.Userrs.Add(u);

            return await _context.SaveChangesAsync() > 0 ? new Response(UserrSuccess.USERCREATED, true) : new Response(UserrError.USERNOTCREATED, false);

        }
    }
}
