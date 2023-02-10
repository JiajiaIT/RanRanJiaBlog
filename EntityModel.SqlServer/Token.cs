using System;
using System.Collections.Generic;

namespace EntityModel.SqlServer;

public partial class Token
{
    public int Id { get; set; }

    public int? AdminId { get; set; }

    public string? Token1 { get; set; }

    public DateTime? ExpirationTime { get; set; }

    public virtual Admin? Admin { get; set; }
}
