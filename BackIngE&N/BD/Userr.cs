using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.BD;

[Table("Userr")]
[Index("Email", Name = "UQ_Userr_Email", IsUnique = true)]
public partial class Userr
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("last_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(512)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("token")]
    [StringLength(512)]
    [Unicode(false)]
    public string? Token { get; set; }

    [Column("role")]
    [StringLength(255)]
    [Unicode(false)]
    public string Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<PlayList> PlayLists { get; set; } = new List<PlayList>();
}
