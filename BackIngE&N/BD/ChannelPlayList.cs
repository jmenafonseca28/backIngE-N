using System.Text.Json.Serialization;

namespace BackIngE_N.BD;

public partial class ChannelPlayList {

    public Guid ChannelId { get; set; }

    public Guid PlaylistId { get; set; }

    public virtual Channel Channel { get; set; } = null!;

    [JsonIgnore]
    public virtual PlayList Playlist { get; set; } = null!;
}
