using BackIngE_N.Config.Messages.PlayList;
using BackIngE_N.Context;
using BackIngE_N.Models;
using BackIngE_N.Models.BD;
using BackIngE_N.Models.DTO.PlayList;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.Logic
{
    public class PlayListLogic {

        private readonly IngenieriaeynContext _context;

        public PlayListLogic(IngenieriaeynContext context) {
            _context = context;
        }

        public async Task<Response> GetPlayList(Guid id) {
            //TODO: falta verificar que la playlist sea del usuario que envía el token
            PlayList p = await _context.PlayLists.Where(p => p.Id == id).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);
            return new Response(PlayListSuccess.PLAYLIST_GET, true, p);
        }

        public async Task<Response> GetPlayListByUserID(Guid idUser) {

            List<PlayList> p = await _context.PlayLists.Where(p => p.UserId == idUser).ToListAsync() ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);

            List<PlayListDTO> playList = [];

            foreach (PlayList pl in p) {
                PlayListDTO playListDTO = new() {
                    Id = pl.Id,
                    Name = pl.Name ?? "",
                    UserId = pl.UserId,
                };
                playList.Add(playListDTO);
            }

            return new Response(PlayListSuccess.PLAYLIST_GET, true, playList);
        }

        public async Task<Response> CreatePlayList(string name, Guid idUser) {
            PlayList p = new() {
                Name = name,
                UserId = idUser,
            };

            await _context.PlayLists.AddAsync(p);
            await _context.SaveChangesAsync();

            return new Response(PlayListSuccess.PLAYLIST_CREATED, true);
        }
        public async Task<Response> DeletePlayList(Guid id) {
            //VERIFICAR SI ES EL DUEÑO DE LA PLAYLIST
            PlayList p = await _context.PlayLists.Where(p => p.Id == id).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);

            _context.PlayLists.Remove(p);
            await _context.SaveChangesAsync();

            return new Response(PlayListSuccess.PLAYLIST_DELETED, true);
        }

        public async Task<Response> UpdatePlayList(PlayListDTORequest playList) {
            PlayList p = await _context.PlayLists.Where(p => p.Id == playList.Id).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);

            p.Name = playList.Name;

            _context.PlayLists.Update(p);
            await _context.SaveChangesAsync();

            return new Response(PlayListSuccess.PLAYLIST_UPDATED, true);
        }


    }
}
