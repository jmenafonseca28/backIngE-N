using BackIngE_N.Context;
using BackIngE_N.Models.DTO.Channel;
using BackIngE_N.Models;
using BackIngE_N.Config.Messages.Channel;
using BackIngE_N.Models.BD;
using Microsoft.EntityFrameworkCore;


namespace BackIngE_N.Logic {
    public class ChannelLogic {

        private readonly IngenieriaeynContext _context;
        private readonly HttpClient _httpClient;

        public ChannelLogic(IngenieriaeynContext context) {
            _context = context;
            _httpClient = new HttpClient();
        }

        public async Task<Response> GetChannelsByPlaylist(Guid idPlaylist) {
            List<Channel> channels = await _context.Channels
                .Where(c => c.PlaylistId == idPlaylist && c.State == true).ToListAsync()
                ?? throw new Exception(ChannelError.CHANNELS_NOT_FOUND);

            return new Response(ChannelSuccess.CHANNELS_GET, true, channels);
        }

        public async Task<Response> FunctionalChannels(Guid idPlaylist) {

            List<Channel> channels = await _context.Channels
                .Where(c => c.PlaylistId == idPlaylist && c.State == true).ToListAsync()
                ?? throw new Exception(ChannelError.CHANNELS_NOT_FOUND);

            foreach (Channel ch in channels) {
                try {
                    HttpResponseMessage response = await _httpClient.GetAsync(ch.Url);
                    if (!response.IsSuccessStatusCode && ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)) {
                        await InactivateChannel(ch.Id);
                    }
                } catch (Exception) { }

            }
            return new Response(ChannelSuccess.CHANNELS_FUNCTIONAL, true);
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
                PlaylistId = channel.PlayListId,
                OrderList = channel.orderList
            };

            var r = await _context.Channels.AddAsync(c);

            c = r.Entity;

            if (c == null) throw new Exception(ChannelError.CHANNEL_NOT_CREATED);
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
            c.PlaylistId = channel.PlayListId;
            c.OrderList = channel.orderList;

            if (await _context.SaveChangesAsync() == 0) throw new Exception(ChannelError.CHANNEL_NOT_UPDATED);

            return new Response(ChannelSuccess.CHANNEL_UPDATED, true);
        }

        public async Task<Response> GetChannel(Guid idChannel) {
            return new Response(ChannelSuccess.CHANNEL_GET, true, await _context.Channels.FindAsync(idChannel) ??
                throw new Exception(ChannelError.CHANNEL_NOT_FOUND));
        }

    }
}
