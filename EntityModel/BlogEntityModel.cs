using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class BlogEntityModel
    {
        public int? ID { get; set; }
        public string? Title { get; set; }
        public string? Img { get; set; }
        public string? Content { get; set; }
        public Nullable<int> ViewNumBer { get; set; }
        public string[]? Category { get; set; }
        public string[]? Tag { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string? CreateBy { get; set; }
        public Nullable<bool> IsShow { get; set; }
    }
}
