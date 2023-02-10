using System;
using System.Collections.Generic;

namespace EntityModel.SqlServer;

public partial class Log
{
    public int Id { get; set; }

    public string? Ip { get; set; }

    public string? Who { get; set; }

    public DateTime? Time { get; set; }

    public string? What { get; set; }
}
