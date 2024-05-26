using System;
using System.Collections.Generic;

namespace BackIngE_N.Models.BD;

public partial class PlayList
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? Name { get; set; }

    public virtual Userr User { get; set; } = null!;

    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
}
