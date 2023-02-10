using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class LogEntityModel
    {
        public int ID { get; set; }

        public string? IP { get; set; }

        public string? Who { get; set; }

        public DateTime? Time { get; set; }

        public string? What { get; set; }
    }
}
