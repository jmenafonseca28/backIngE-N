using BackIngE_N.Config.Messages.Channel;
using BackIngE_N.Config.Messages.PlayList;
using BackIngE_N.Context;
using BackIngE_N.Models;
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
            StringBuilder sb = new();
            PlayList p = await _context.PlayLists.Where(p => p.Id == id).Include(p => p.Channels).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);
            List<Channel> channels = p.Channels.Where(c => c.State == true).ToList() ?? throw new Exception(ChannelError.CHANNELS_NOT_FOUND);

            sb.Append("#EXTM3U\n");

            foreach (Channel cp in channels) {
                sb.Append("#EXTINF: -1 tvg-id=\"" + cp.TvgId + "\" tvg-chno=\"" + cp.TvgChannelNumber + "\" tvg-logo=\"" + cp.Logo + "\" group-title=\"" + cp.GroupTitle + "\", " + cp.Title + "\n");
                sb.Append(cp.Url + "\n");
            }

            var content = Encoding.UTF8.GetBytes(sb.ToString());

            return new FileContentResult(content, "application/octet-stream") {
                FileDownloadName = "playlist.m3u"
            };

        }

        public async Task<Response> ImportPlayList(IFormFile file, string playListId) {

            using var reader = new StreamReader(file.OpenReadStream());
            string line;
            PlayList p = _context.PlayLists.Find(Guid.Parse(playListId)) ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);
            List<Channel> unFunctionalChannels = [];
            while ((line = reader.ReadLine()) != null) {

                if (line.Contains("#EXTINF:")) {

                    string tvgNumber = line.Contains("tvg-chno") ? line.Split("tvg-chno=\"")[1].Split("\"")[0] : null;

                    _ = int.TryParse(tvgNumber, out int tvgChannelNumber);

                    Channel ch = new() {
                        TvgId = line.Contains("tvg-id") ? line.Split("tvg-id=\"")[1].Split("\"")[0] : "",
                        TvgChannelNumber = tvgChannelNumber,
                        Logo = line.Contains("tvg-logo") ? line.Split("tvg-logo=\"")[1].Split("\"")[0] : null,
                        GroupTitle = line.Contains("group-title") ? line.Split("group-title=\"")[1].Split("\"")[0] : null,
                        Title = line.Split(",")[1].Trim() ?? "",
                        Url = reader.ReadLine() ?? "",
                        State = true,
                        OrderList = 0
                    };

                    if (ch.Title == "") ch.Title = ch.TvgId;

                    if (ch.Url == "" || ch.Title == "") {
                        unFunctionalChannels.Add(ch);
                        continue;
                    }

                    if (_context.Channels.All(c => c.Url != ch.Url)) {
                        p.Channels.Add(ch);
                    } else {
                        Channel chn = await _context.Channels.Where(c => c.Url == ch.Url).FirstOrDefaultAsync();
                        if (chn != null) p.Channels.Add(chn);
                        unFunctionalChannels.Add(ch);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return new Response(PlayListSuccess.PLAYLIST_CREATED, true, unFunctionalChannels);
        }
    }
}
