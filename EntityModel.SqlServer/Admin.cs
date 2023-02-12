using System;
using System.Collections.Generic;

namespace EntityModel.SqlServer;

public partial class Admin
{
    public int Id { get; set; }

    public string? Account { get; set; }

    public string? Password { get; set; }

    public string? EMail { get; set; }

    public string? Code { get; set; }

    public DateTime? ExpirationTime { get; set; }

    public virtual ICollection<AdminInfo> AdminInfos { get; } = new List<AdminInfo>();

    public virtual ICollection<Blog> Blogs { get; } = new List<Blog>();

    public virtual ICollection<Token> Tokens { get; } = new List<Token>();
}
