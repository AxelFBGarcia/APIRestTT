using System;
using System.Collections.Generic;

namespace APIRestTT.Models;

public partial class User
{
    public int UserID { get; set; }

    public string LoginName { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public byte? Active { get; set; }
}
