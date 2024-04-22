using BackIngE_N.Config.Jwt;
using BackIngE_N.Config.Messages;
using BackIngE_N.DTO.UserrDto;
using BackIngE_N.Logic.User;
using BackIngE_N.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
                return await _userrLogic.Login(user);
            } catch (Exception e) {
                return new Response(e.Message, false);
            }

        }

        //[Authorize] // Para si o si tener un token

    }
}
