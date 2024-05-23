using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackIngE_N.Models.BD;

public partial class Channel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string? TvgId { get; set; }

    public string? TvgName { get; set; }

    public int? TvgChannelNumber { get; set; }

    public string? Logo { get; set; }

    public string? GroupTitle { get; set; }

    public int? OrderList { get; set; }

    public bool State { get; set; }

    public Guid PlaylistId { get; set; }

    [JsonIgnore]
    public virtual PlayList Playlist { get; set; } = null!;
}
