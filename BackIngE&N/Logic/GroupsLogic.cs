using BackIngE_N.Models.BD;
using BackIngE_N.Context;
using BackIngE_N.Models;
using BackIngE_N.Config.Messages.Group;
using Microsoft.EntityFrameworkCore;
using BackIngE_N.Models.DTO.Group;

namespace BackIngE_N.Logic {
    public class GroupsLogic {

        private readonly IngenieriaeynContext _context;

        public GroupsLogic(IngenieriaeynContext context) {
            _context = context;
        }

        public async Task<Response> GetGroupsByPlayListId(Guid idPlayList) {

            List<Group> groups = await _context.Groups.Where(g => g.IdPlaylist == idPlayList).ToListAsync() ?? throw new Exception(GroupError.GROUPS_NOT_FOUND);

            return new Response(GroupSuccess.GROUPS_GET, true, groups);
        }

        public async Task<Response> CreateGroup(GroupDTO group) {

            if (string.IsNullOrEmpty(group.Name)) throw new Exception(GroupError.GROUP_NOT_CREATED);

            Group g = new() {
                Name = group.Name,
                IdPlaylist = group.IdPlaylist ?? throw new Exception(GroupError.GROUP_NOT_CREATED),
            };

            await _context.Groups.AddAsync(g);
            await _context.SaveChangesAsync();

            return new Response(GroupSuccess.GROUP_CREATED, true);
        }
         public async Task<Response> DeleteGroup(Guid id) {
             Group g = await _context.Groups.Where(g => g.Id == id).FirstOrDefaultAsync() ?? throw new Exception(GroupError.GROUP_NOT_FOUND);

             _context.Groups.Remove(g);
             await _context.SaveChangesAsync();

             return new Response(GroupSuccess.GROUP_DELETED, true);
         }

         public async Task<Response> UpdateGroup(GroupDTO group) {
             Group g = await _context.Groups.Where(g => g.Id == group.Id).FirstOrDefaultAsync() ?? throw new Exception(GroupError.GROUP_NOT_FOUND);

             g.Name = group.Name;

             await _context.SaveChangesAsync();

             return new Response(GroupSuccess.GROUP_UPDATED, true);
         }

        public async Task<Response> ChangeGroup(Guid idChannel, GroupDTO group) { 
            Channel ch = await _context.Channels.FindAsync(idChannel) ?? throw new Exception(GroupError.ERROR);
            Group g = await _context.Groups.FindAsync(group.Id) ?? throw new Exception(GroupError.GROUP_NOT_FOUND);

            ch.IdGroup = g.Id;
            ch.IdGroupNavigation = g;
            ch.GroupTitle = g.Name;

            await _context.SaveChangesAsync();

            return new Response(GroupSuccess.GROUP_UPDATED, true);
        }

         /*public async Task<Response> GetGroup(Guid idGroup) {
             return new Response(GroupSuccess.GROUP_GET, true, await _context.Groups.FindAsync(idGroup) ??
                 throw new Exception(GroupError.GROUP_NOT_FOUND));
         }*/

    }
}
