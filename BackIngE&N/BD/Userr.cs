using System;
using System.Collections.Generic;

namespace BackIngE_N.BD;

public partial class Userr
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Token { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<PlayList> PlayLists { get; set; } = new List<PlayList>();
}
