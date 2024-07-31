using BackIngE_N.Config.Jwt;
using BackIngE_N.Config.Messages;
using Microsoft.EntityFrameworkCore;
using BackIngE_N.Models;
using _bCrypt = BCrypt.Net.BCrypt;
using System.Net;
using BackIngE_N.Config.Messages.User;
using BackIngE_N.Context;
using BackIngE_N.Models.DTO.UserrDto;
using BackIngE_N.Models.BD;

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
        public async Task<Response> Login(UserBase user, IPAddress? ip) {

            if (ip != null) {
                if (await _securityLogic.IsBlockedIP(ip)) throw new Exception(UserrError.IP_BLOCKED);
                await _securityLogic.ValidateIP(ip);
            }

            Userr u = await _context.Userrs.Where(u => u.Email == user.Email).FirstOrDefaultAsync() ?? throw new Exception(UserrError.USER_NOT_FOUND);

            if (user == null || !_bCrypt.Verify(user.Password, u.Password)) {
                if (ip != null) await _securityLogic.SaveIP(ip, false);
                throw new Exception(UserrError.LOGIN_ERROR);
            };

            if (ip != null) await _securityLogic.SaveIP(ip, true);

            Response r = _jwtConfig.generateToken(user, u);

            if (r.Success && r.Message.Equals(GeneralMessages.TOKEN_GENERATED)) {
                if (r.Data != null) await UpdateToken(user.Email, (string)r.Data);
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

            Userr u = await _context.Userrs.Where(u => u.Email == email).FirstOrDefaultAsync() ?? throw new Exception(UserrError.USER_NOT_FOUND);

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

            if (await _context.Userrs.Where(u => u.Email == user.Email).FirstOrDefaultAsync() != null) throw new Exception(UserrError.USER_ALREDY_EXIST);

            Userr u = new() {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Password = _bCrypt.HashPassword(user.Password),
                Role = "user",
                Token = null
            };

            _context.Userrs.Add(u);

            return await _context.SaveChangesAsync() > 0 ? new Response(UserrSuccess.USER_CREATED, true) : new Response(UserrError.USER_NOT_CREATED, false);

        }

        public async Task<IResponse<Object>> ChangePassword(Guid idUser, PasswordDTO pass) {
            Userr u = await _context.Userrs.Where(u => u.Id == idUser).FirstOrDefaultAsync() ?? throw new Exception(UserrError.USER_NOT_FOUND);

            if (!_bCrypt.Verify(pass.Password, u.Password)) throw new Exception(UserrError.INCORRECT_PASSWORD);

            u.Password = _bCrypt.HashPassword(pass.NewPassword);

            _context.Userrs.Update(u);

            return await _context.SaveChangesAsync() > 0 ? new Response(UserrSuccess.PASSWORD_UPDATED, true) : new Response(UserrError.PASSWORD_NOT_UPDATED, false);
        }

        public async Task<IResponse<Object>> Update(Guid idUser, UserDTO user) {
            Userr u = await _context.Userrs.Where(u => u.Id == idUser).FirstOrDefaultAsync() ?? throw new Exception(UserrError.USER_NOT_FOUND);

            u.Name = user.Name;
            u.LastName = user.LastName;
            u.Email = user.Email;

            _context.Userrs.Update(u);

            return await _context.SaveChangesAsync() > 0 ? new Response(UserrSuccess.USER_UPDATED, true, u) : new Response(UserrError.USER_NOT_UPDATED, false, user);
        }
    }
}
