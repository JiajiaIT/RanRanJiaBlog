using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;

namespace IDAL
{
    public interface I_Extend
    {
        bool Authorize(string token);
        void WriteLog(LogEntityModel entity);
        List<LogEntityModel> Read(int page, int pagesize);
        bool IsSuperAdmin(string token);
    }
}
