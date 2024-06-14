using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackIngE_N.Models.BD;

public partial class Group {
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid IdPlaylist { get; set; }

    [JsonIgnore]
    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();

    public virtual PlayList IdPlaylistNavigation { get; set; } = null!;
}
