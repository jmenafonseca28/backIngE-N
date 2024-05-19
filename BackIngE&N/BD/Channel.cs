using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.BD;

[Table("Channel")]
public partial class Channel
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("url")]
    [StringLength(255)]
    [Unicode(false)]
    public string Url { get; set; } = null!;

    [Column("tvg_id")]
    [StringLength(255)]
    [Unicode(false)]
    public string? TvgId { get; set; }

    [Column("tvg_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string? TvgName { get; set; }

    [Column("tvg_channel_number")]
    public int? TvgChannelNumber { get; set; }

    [Column("logo")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Logo { get; set; }

    [ForeignKey("ChannelId")]
    [InverseProperty("Channels")]
    [JsonIgnore]
    public virtual ICollection<PlayList> Playlists { get; set; } = new List<PlayList>();
}
