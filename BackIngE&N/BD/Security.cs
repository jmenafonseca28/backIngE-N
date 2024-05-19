using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.BD;

[Table("Security")]
public partial class Security
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("ip")]
    [StringLength(255)]
    [Unicode(false)]
    public string Ip { get; set; } = null!;

    [Column("login_time", TypeName = "datetime")]
    public DateTime? LoginTime { get; set; }

    [Column("status_login")]
    public bool StatusLogin { get; set; }
}
