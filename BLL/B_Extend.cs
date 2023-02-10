using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using DAL;
using Factory;
using EntityModel;

namespace BLL
{
    public class B_Extend : I_Extend
    {
        public bool Authorize(string token)
        {
            return CreateFactory.Get_Extend().Authorize(token);
        }

        public bool IsSuperAdmin(string token)
        {
            return CreateFactory.Get_Extend().IsSuperAdmin(token);
        }

        public List<LogEntityModel> Read(int page, int pagesize)
        {
            return CreateFactory.Get_Extend().Read(page, pagesize);
        }

        public void WriteLog(LogEntityModel entity)
        {
            CreateFactory.Get_Extend().WriteLog(entity);
        }
    }
}
