using System;
using System.Collections.Generic;

namespace EntityModel.SqlServer;

public partial class Tag
{
    public int Id { get; set; }

    public string? TagName { get; set; }
}
