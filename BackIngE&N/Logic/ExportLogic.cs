using BackIngE_N.BD;
using BackIngE_N.Config.Messages.PlayList;
using BackIngE_N.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BackIngE_N.Logic {
    public class ExportLogic {


        private readonly IngenieriaeynContext _context;

        public ExportLogic(IngenieriaeynContext context) {
            _context = context;
        }

        public async Task<IActionResult> ExportPlayList(Guid id) {
            StringBuilder sb = new StringBuilder();
            PlayList p = await _context.PlayLists.Where(p => p.Id == id).Include(p => p.ChannelPlayLists)
                .ThenInclude(p => p.Channel).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLISTNOTFOUND);

            sb.Append("#EXTM3U\n");

            foreach (ChannelPlayList cp in p.ChannelPlayLists) {
                sb.Append("#EXTINF: -1 tvg-id=\"" + cp.Channel.TvgId + "\" tvg-chno=\"" + cp.Channel.TvgChannelNumber + "\" tvg-logo=\"" + cp.Channel.Logo + "\" group-title=\"General\", " + cp.Channel.Title + "\n");
                sb.Append(cp.Channel.Url + "\n");
            }

            var content = Encoding.UTF8.GetBytes(sb.ToString());

            return new FileContentResult(content, "application/octet-stream") {
                FileDownloadName = "playlist.m3u"
            };

        }
    }
}
