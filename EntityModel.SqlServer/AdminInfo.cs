using System;
using System.Collections.Generic;

namespace EntityModel.SqlServer;

public partial class AdminInfo
{
    public int Id { get; set; }

    public int? AdminId { get; set; }

    public string? NickName { get; set; }

    public string? Img { get; set; }

    public virtual Admin? Admin { get; set; }
}
