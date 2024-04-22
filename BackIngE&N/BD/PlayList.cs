using System;
using System.Collections.Generic;

namespace BackIngE_N.BD;

public partial class PlayList
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<ChannelPlayList> ChannelPlayLists { get; set; } = new List<ChannelPlayList>();

    public virtual Userr User { get; set; } = null!;
}
