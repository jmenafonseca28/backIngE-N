using BackIngE_N.Context;
using BackIngE_N.Models.DTO.Channel;
using BackIngE_N.Models;
using BackIngE_N.Config.Messages.PlayList;
using BackIngE_N.Config.Messages.Channel;
using BackIngE_N.Models.BD;

namespace BackIngE_N.Logic {
    public class ChannelLogic {

        private readonly IngenieriaeynContext _context;

        public ChannelLogic(IngenieriaeynContext context) {
            _context = context;
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
                orderList = channel.orderList
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
            c.orderList = channel.orderList;

            if (await _context.SaveChangesAsync() == 0) throw new Exception(ChannelError.CHANNEL_NOT_CREATED);

            return new Response(ChannelSuccess.CHANNEL_UPDATED, true);
        }

        public async Task<Response> GetChannel(Guid idChannel) {
            return new Response(ChannelSuccess.CHANNEL_GET, true, await _context.Channels.FindAsync(idChannel) ??
                throw new Exception(ChannelError.CHANNEL_NOT_FOUND));
        }

    }
}
