using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class BlogAddEntityModel
    {

        public string? Title { get; set; }

        public string? Img { get; set; }

        public string? Content { get; set; }

        public string[]? Category { get; set; }

        public string[]? Tag { get; set; }

        public bool? IsShow { get; set; }
        public string? Token { get; set; }
    }
}
