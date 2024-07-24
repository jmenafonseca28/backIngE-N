using BackIngE_N.Context;
using BackIngE_N.Models.DTO.Channel;
using BackIngE_N.Models;
using BackIngE_N.Config.Messages.Channel;
using BackIngE_N.Models.BD;
using Microsoft.EntityFrameworkCore;
using BackIngE_N.Config.Messages.PlayList;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace BackIngE_N.Logic {
    public class ChannelLogic {

        private readonly IngenieriaeynContext _context;
        private readonly HttpClient _httpClient;

        public ChannelLogic(IngenieriaeynContext context, HttpClient httpClient) {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<IResponse<Object>> GetChannelsByPlaylist(int page, Guid idPlaylist) {
            int pageSize = 10;
            int skipAmount = (page - 1) * pageSize;
            PlayList p = await _context.PlayLists.Where(c => c.Id == idPlaylist).Include(p => p.Channels).FirstOrDefaultAsync() ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);
            List<Channel> channels = p.Channels.Where(c => c.State == true).Skip(skipAmount).Take(pageSize).ToList() ?? throw new Exception(ChannelError.CHANNELS_NOT_FOUND);
            int count = p.Channels.Count;
            int totalPages = (int)Math.Ceiling((double)count / pageSize);
            return new Response(ChannelSuccess.CHANNELS_GET, true, new PaginationResponse(page, pageSize, totalPages, count, channels));
        }

        public async Task VerifyChannels() {
            List<Channel> channels = await _context.Channels.Where(c => c.State == true).ToListAsync() ?? throw new Exception(ChannelError.CHANNELS_NOT_FOUND);
            foreach (Channel ch in channels) {
                try {
                    HttpResponseMessage response = await _httpClient.GetAsync(ch.Url);
                    if (!response.IsSuccessStatusCode && ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)) {
                        await InactivateChannel(ch.Id);
                    }
                } catch (Exception) { }
            }
        }

        public async Task<bool> InactivateChannel(Guid idChannel) {
            Channel c = await _context.Channels.FindAsync(idChannel) ?? throw new Exception(ChannelError.CHANNEL_NOT_FOUND);
            c.State = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Response> ToggleOrder(Guid idChannel, int newOrder) {
            Channel c = await _context.Channels.FindAsync(idChannel) ?? throw new Exception(ChannelError.CHANNEL_NOT_FOUND);
            c.OrderList = newOrder;
            return await _context.SaveChangesAsync() > 0 ? new Response(ChannelSuccess.CHANNEL_UPDATED, true) : new Response(ChannelError.CHANNEL_NOT_UPDATED, false);
        }

        public async Task<Response> CreateChannel(ChannelDTO channel) {
            Channel c = new() {
                Title = channel.Title,
                Url = channel.Url,
                TvgName = channel.TvgName,
                Logo = channel.Logo,
                TvgId = channel.TvgId,
                TvgChannelNumber = channel.TvgChannelNumber,
                GroupTitle = channel.GroupTitle,
                OrderList = 0,
                State = true
            };

            if (c.Title == string.Empty || c.Url == string.Empty) throw new Exception(ChannelError.DATA_EMPTY);

            EntityEntry<Channel> r = await _context.Channels.AddAsync(c);
            c = r.Entity;
            if (c == null) throw new Exception(ChannelError.CHANNEL_NOT_CREATED);

            PlayList p = _context.PlayLists.Find(channel.PlayListId) ?? throw new Exception(PlayListError.PLAYLIST_NOT_FOUND);
            p.Channels.Add(c);

            if (await _context.SaveChangesAsync() == 0) throw new Exception(ChannelError.CHANNEL_NOT_CREATED);

            return new Response(ChannelSuccess.CHANNEL_CREATED, true, c);
        }

        public async Task<Response> DeleteChannel(Guid idChannel) {
            //TODO: VERIFICAR SI EL USUARIO QUE ESTA HACIENDO LA PETICION ES EL DUEÑO DEL CANAL
            Channel c = _context.Channels.Find(idChannel) ?? throw new Exception(ChannelError.CHANNEL_NOT_FOUND);
            _context.Channels.Remove(c);
            await _context.SaveChangesAsync();
            return new Response(ChannelSuccess.CHANNEL_DELETED, true);
        }

        public async Task<Response> UpdateChannel(ChannelDTO channel) {
            Channel c = _context.Channels.Find(channel.Id) ?? throw new Exception(ChannelError.CHANNEL_NOT_FOUND);
            c.Title = channel.Title;
            c.Url = channel.Url;
            c.TvgName = channel.TvgName;
            c.Logo = channel.Logo;
            c.TvgId = channel.TvgId;
            c.TvgChannelNumber = channel.TvgChannelNumber;

            if (await _context.SaveChangesAsync() == 0) throw new Exception(ChannelError.CHANNEL_NOT_UPDATED);

            return new Response(ChannelSuccess.CHANNEL_UPDATED, true);
        }

        public async Task<Response> GetChannel(Guid idChannel) {
            return new Response(ChannelSuccess.CHANNEL_GET, true, await _context.Channels.FindAsync(idChannel) ??
                throw new Exception(ChannelError.CHANNEL_NOT_FOUND));
        }

    }
}
