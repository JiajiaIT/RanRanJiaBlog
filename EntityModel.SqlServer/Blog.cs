using System;
using System.Collections.Generic;

namespace EntityModel.SqlServer;

public partial class Blog
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Img { get; set; }

    public string? Content { get; set; }

    public int? ViewNumBer { get; set; }

    public string? Category { get; set; }

    public string? Tag { get; set; }

    public DateTime? CreateTime { get; set; }

    public string? CreateBy { get; set; }

    public bool? IsShow { get; set; }
}
