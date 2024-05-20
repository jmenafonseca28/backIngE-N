using System;
using System.Collections.Generic;

namespace BackIngE_N.Models.BD;

public partial class BlockedIp {
    public Guid Id { get; set; }

    public string Ip { get; set; } = null!;

    public DateTime? BlockTime { get; set; }
}
