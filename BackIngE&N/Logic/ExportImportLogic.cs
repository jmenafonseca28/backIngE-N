using BackIngE_N.Config.Messages.PlayList;
using BackIngE_N.Context;
using BackIngE_N.Models.BD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BackIngE_N.Logic {
    public class ExportImportLogic {


        private readonly IngenieriaeynContext _context;

        public ExportImportLogic(IngenieriaeynContext context) {
            _context = context;
        }

        public async Task<IActionResult> ExportPlayList(Guid id) {
            StringBuilder sb = new StringBuilder();
            PlayList p = await _context.PlayLists.Where(p => p.Id == id).Include(p => p.Channels).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);

            sb.Append("#EXTM3U\n");

            foreach (Channel cp in p.Channels) {
                sb.Append("#EXTINF: -1 tvg-id=\"" + cp.TvgId + "\" tvg-chno=\"" + cp.TvgChannelNumber + "\" tvg-logo=\"" + cp.Logo + "\" group-title=\"" + cp.GroupTitle + "\", " + cp.Title + "\n");
                sb.Append(cp.Url + "\n");
            }

            var content = Encoding.UTF8.GetBytes(sb.ToString());

            return new FileContentResult(content, "application/octet-stream") {
                FileDownloadName = "playlist.m3u"
            };

        }
    }
}
