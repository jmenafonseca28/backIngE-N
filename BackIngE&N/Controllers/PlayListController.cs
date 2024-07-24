using BackIngE_N.Config.Messages;
using BackIngE_N.Config.Messages.PlayList;
using BackIngE_N.Logic;
using BackIngE_N.Models;
using BackIngE_N.Models.DTO.PlayList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackIngE_N.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayListController : ControllerBase {

        private readonly PlayListLogic _playListLogic;
        private readonly ExportImportLogic _exportLogic;

        public PlayListController(PlayListLogic playListLogic, ExportImportLogic exportLogic) {
            _playListLogic = playListLogic;
            _exportLogic = exportLogic;
        }

        [HttpGet]
        [Route("getPlayListsByUserId/{userId}")]
        [Authorize]
        public async Task<Response> GetPlayLists(Guid userId) {
            try {
                return await _playListLogic.GetPlayListByUserID(userId);
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

        [HttpPost]
        [Route("createPlayList")]
        public async Task<Response> CreatePlayList([FromBody] PlayListRequest p) {
            try {
                return await _playListLogic.CreatePlayList(p.Name, p.UserId);
            } catch (Exception e) {
                return new Response(GeneralMessages.ERROR, false, e.Message);
            }
        }

        [HttpDelete]
        [Route("deletePlayList/{id}")]
        public async Task<Response> DeletePlayList(Guid id) {
            try {
                return  await _playListLogic.DeletePlayList(id);
            } catch (Exception e) {
                return new Response(GeneralMessages.ERROR, false, e.Message);
            }
        }

        [HttpPut]
        [Route("updatePlayList/")]
        public async Task<Response> UpdatePlayList([FromBody] PlayListDTORequest playListDTO) {
            try {
                return await _playListLogic.UpdatePlayList(playListDTO);
            } catch (Exception e) {
                return new Response(GeneralMessages.ERROR, false, e.Message);
            }
        }


        [HttpGet]
        [Route("exportPlayList/{playlistId}")]
        public async Task<IActionResult> AddChannel(Guid playlistId) {
            try {
                return await _exportLogic.ExportPlayList(playlistId);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
