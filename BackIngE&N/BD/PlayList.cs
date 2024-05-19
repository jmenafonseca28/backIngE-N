using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.BD;

[Table("PlayList")]
public partial class PlayList
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("name")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("PlayLists")]
    public virtual Userr User { get; set; } = null!;

    [ForeignKey("PlaylistId")]
    [InverseProperty("Playlists")]
    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
}
