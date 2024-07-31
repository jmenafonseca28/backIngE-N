using BackIngE_N.Config.Jwt;
using BackIngE_N.Config.Messages;
using BackIngE_N.Config.Messages.User;
using BackIngE_N.Logic;
using BackIngE_N.Models;
using BackIngE_N.Models.DTO.UserrDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackIngE_N.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserrController : ControllerBase {

        public readonly UserrLogic _userrLogic;

        public UserrController(UserrLogic userrLogic) {
            _userrLogic = userrLogic;
        }

        [HttpPost]
        [Route("login")]
        public async Task<Response> Login([FromBody] UserBase user) {

            try {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                return await _userrLogic.Login(user, ipAddress);
            } catch (Exception e) {
                return new Response(UserrError.LOGIN_ERROR, false, e.Message);
            }

        }

        [HttpPost]
        [Route("register")]
        public async Task<Response> Register([FromBody] UserDTO user) {

            try {
                return await _userrLogic.Register(user);
            } catch (Exception e) {
                return new Response(UserrError.USER_NOT_CREATED, false, e.Message);
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("changePassword/{id}")]
        public async Task<IResponse<Object>> ChangePassword(Guid id, [FromBody] PasswordDTO changePassword) {

            try {
                return await _userrLogic.ChangePassword(id, changePassword);
            } catch (Exception e) {
                return new Response(UserrError.PASSWORD_NOT_UPDATED, false, e.Message);
            }

        }

        [Authorize]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IResponse<Object>> Update(Guid id, [FromBody] UserDTO user) {

            try {
                return await _userrLogic.Update(id, user);
            } catch (Exception e) {
                return new Response(UserrError.USER_NOT_UPDATED, false, e.Message);
            }

        }

    }
}
