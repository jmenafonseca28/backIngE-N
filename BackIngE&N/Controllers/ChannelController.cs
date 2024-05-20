using BackIngE_N.Config.Messages.Channel;
using BackIngE_N.Logic;
using BackIngE_N.Models;
using BackIngE_N.Models.DTO.Channel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackIngE_N.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase {

        private readonly ChannelLogic _channelLogic;

        public ChannelController(ChannelLogic channelLogic) {
            _channelLogic = channelLogic;
        }

        [HttpPost]
        [Route("CreateChannel")]
        public async Task<Response> CreateChannel(ChannelDTO channel) {
            try {
                return await _channelLogic.CreateChannel(channel);
            } catch (Exception e) {
                return new Response(ChannelError.CHANNEL_NOT_CREATED, false, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteChannel/{idChannel}")]
        public async Task<Response> DeleteChannel(Guid idChannel) {
            try {
                return await _channelLogic.DeleteChannel(idChannel);
            } catch (Exception e) {
                return new Response(ChannelError.CHANNEL_NOT_DELETED, false, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateChannel")]
        public async Task<Response> UpdateChannel(ChannelDTO channel) {
            try {
                return await _channelLogic.UpdateChannel(channel);
            } catch (Exception e) {
                return new Response(ChannelError.CHANNEL_NOT_UPDATED, false, e.Message);
            }
        }

        [HttpGet]
        [Route("GetChannel/{idChannel}")]
        public async Task<Response> GetChannel(Guid idChannel) {
            try {
                return await _channelLogic.GetChannel(idChannel);
            } catch (Exception e) {
                return new Response(ChannelError.CHANNEL_NOT_FOUND, false, e.Message);
            }
        }

    }
}
