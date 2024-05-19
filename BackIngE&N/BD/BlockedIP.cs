using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.BD;

[Table("BlockedIP")]
public partial class BlockedIp
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("ip")]
    [StringLength(255)]
    [Unicode(false)]
    public string Ip { get; set; } = null!;

    [Column("block_time", TypeName = "datetime")]
    public DateTime? BlockTime { get; set; }
}
