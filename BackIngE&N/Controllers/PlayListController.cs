using BackIngE_N.Config.Messages;
using BackIngE_N.Logic;
using BackIngE_N.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackIngE_N.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase {

        private readonly PlayListLogic _playListLogic;

        public PlayListController(PlayListLogic playListLogic) {
            _playListLogic = playListLogic;
        }

        [HttpGet]
        [Route("getPlayListsByUserId/{idUser}")]
        //[Authorize]
        public async Task<Response> GetPlayLists(Guid idUser) {
            try {
                return await _playListLogic.GetPlayListByUserID(idUser);
            } catch (Exception e) {
                return new Response(GeneralMessages.ERROR, false, e.Message);
            }
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<Response> GetPlayList(Guid id) {

            try {
                return await _playListLogic.GetPlayList(id);
            } catch (Exception e) {
                return new Response(GeneralMessages.ERROR, false, e.Message);
            }

        }

        /* [HttpPost]
         public async Task<IActionResult> CreatePlayList([FromBody] PlayListDTO playListDTO) {
             var playList = await _playListService.CreatePlayList(playListDTO);
             return Ok(playList);
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> UpdatePlayList(Guid id,[FromBody] PlayListDTO playListDTO) {
             var playList = await _playListService.UpdatePlayList(id,playListDTO);
             return Ok(playList);
         }

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeletePlayList(Guid id) {
             await _playListService.DeletePlayList(id);
             return Ok();
         }

         [HttpPost("AddChannel")]
         public async Task<IActionResult> AddChannel([FromBody] ChannelPlayListDTO channelPlayListDTO) {
             var channelPlayList = await _playListService.AddChannel(channelPlayListDTO);
             return Ok(channelPlayList);
         }

         [HttpDelete("RemoveChannel/{id}")]
         public async Task<IActionResult> RemoveChannel(Guid id) {
             await _playListService.RemoveChannel(id);
             return Ok();
         }*/

    }
}
