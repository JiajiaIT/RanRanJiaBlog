using System;
using System.Collections.Generic;

namespace EntityModel.SqlServer;

public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }
}
