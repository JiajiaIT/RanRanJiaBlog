using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class Result<T>
    {
        public bool State { get; set; } = true;
        public string Msg { get; set; } = "成功";
        public T Data { get; set; }
    }
}