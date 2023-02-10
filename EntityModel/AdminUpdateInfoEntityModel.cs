using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class AdminUpdateInfoEntityModel
    {
        public string? Img { get; set; }
        public string? NickName { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? E_Mail { get; set; }
    }
}
