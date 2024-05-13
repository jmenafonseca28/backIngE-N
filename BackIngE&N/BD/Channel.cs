namespace BackIngE_N.BD;

public partial class Channel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string? TvgId { get; set; }

    public string? TvgName { get; set; }

    public int? TvgChannelNumber { get; set; }

    public string? Logo { get; set; }

    public virtual ICollection<ChannelPlayList> ChannelPlayLists { get; set; } = new List<ChannelPlayList>();
}
