using System;
using System.Collections.Generic;

namespace BackIngE_N.Models.BD;

public partial class Security
{
    public Guid Id { get; set; }

    public string Ip { get; set; } = null!;

    public DateTime? LoginTime { get; set; }

    public bool StatusLogin { get; set; }
}
