using BackIngE_N.BD;
using BackIngE_N.Config.Messages;
using BackIngE_N.Models;

namespace BackIngE_N.Logic {
    public class PlayListLogic {

        private readonly IngenieriaeynContext _context;

        public PlayListLogic(IngenieriaeynContext context) {
            _context = context;
        }

        /*public async Task<Response> GetPlayList(int id) {
            PlayList p = await _context.PlayLists.Where(p => p.Id == id).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLISTNOTFOUND);

            return new Response(GeneralMessages.SUCCESS, true, new PlayListResponse(p.Id, p.Name, p.Description, p.Songs));
        }

        public async Task<Response> CreatePlayList(PlayListDTO playList) {
            PlayList p = new PlayList() {
                Name = playList.Name,
                Description = playList.Description,
                Songs = playList.Songs
            };

            await _context.PlayLists.AddAsync(p);
            await _context.SaveChangesAsync();

            return new Response(GeneralMessages.SUCCESS, true, new PlayListResponse(p.Id, p.Name, p.Description, p.Songs));
        }

        public async Task<Response> UpdatePlayList(int id, PlayListDTO playList) {
            PlayList p = await _context.PlayLists.Where(p => p.Id == id).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLISTNOTFOUND);

            p.Name = playList.Name;
            p.Description = playList.Description;
            p.Songs = playList.Songs;

            _context.PlayLists.Update(p);
            await _context.SaveChangesAsync();

            return new Response(GeneralMessages.SUCCESS, true, new PlayListResponse(p.Id, p.Name, p.Description, p.Songs));
        }

        public async Task<Response> DeletePlayList(int id) {
            PlayList p = await _context.PlayLists.Where(p => p.Id == id).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLISTNOTFOUND);

            _context.PlayLists.Remove(p);
            await _context.SaveChangesAsync();

            return new Response(GeneralMessages.SUCCESS, true);
        }
        */
    }
}
