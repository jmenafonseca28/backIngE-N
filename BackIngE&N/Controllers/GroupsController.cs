using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BackIngE_N.Logic;
using BackIngE_N.Models.DTO.Group;
using BackIngE_N.Config.Messages.Group;
using BackIngE_N.Models;


namespace BackIngE_N.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController:ControllerBase {

        private readonly GroupsLogic _groupsLogic;

        public GroupsController(GroupsLogic groupsLogic) {
            _groupsLogic = groupsLogic;
        }

        [HttpGet]
        [Route("GetGroupsByPlayListId/{idPlayList}")]
        public async Task<Response> GetGroupsByPlayListId(Guid idPlayList) {
            try {
                return await _groupsLogic.GetGroupsByPlayListId(idPlayList);
            } catch(Exception e) {
                return new Response(GroupError.GROUPS_NOT_FOUND, false, e.Message);
            }
        }

        [HttpPost]
        [Route("CreateGroup")]
        public async Task<Response> CreateGroup(GroupDTO group) {
            try {
                return await _groupsLogic.CreateGroup(group);
            } catch(Exception e) {
                return new Response(GroupError.GROUP_NOT_CREATED, false, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteGroup/{idGroup}")]
        public async Task<Response> DeleteGroup(Guid idGroup) {
            try {
                return await _groupsLogic.DeleteGroup(idGroup);
            } catch(Exception e) {
                return new Response(GroupError.GROUP_NOT_DELETED, false, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateGroup")]
        public async Task<Response> UpdateGroup(GroupDTO group) {
            try {
                return await _groupsLogic.UpdateGroup(group);
            } catch(Exception e) {
                return new Response(GroupError.GROUP_NOT_UPDATED, false, e.Message);
            }
        }

        [HttpPut]
        [Route("ChangeGroup/{idChannel}")]
        public async Task<Response> ChangeGroup(Guid idChannel, GroupDTO group) {
            try {
                return await _groupsLogic.ChangeGroup(idChannel, group);
            } catch(Exception e) {
                return new Response(GroupError.GROUP_NOT_UPDATED, false, e.Message);
            }
        }

        /* [HttpGet]
        [Route("GetGroup/{idGroup}")]
        public async Task<Response> GetGroup(Guid idGroup) {
            try {
                return await _groupsLogic.GetGroup(idGroup);
            } catch (Exception e) {
                return new Response(GroupError.GROUP_NOT_FOUND, false, e.Message);
            }
        }*/

    }
}
