namespace BackIngE_N.BD;

public partial class ChannelPlayList
{
    public Guid Id { get; set; }

    public Guid ChannelId { get; set; }

    public Guid PlaylistId { get; set; }

    public virtual Channel Channel { get; set; } = null!;

    public virtual PlayList Playlist { get; set; } = null!;
}
