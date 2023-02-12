using EntityModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface I_Admins
    {
        string Login(LoginEntityModel entity);
        void Register(RegisterEntityModel entity);
        List<AdminInfoEntityModel> List(int page, int pagesize);
        AdminInfoEntityModel FindByID(int id);
        AdminInfoEntityModel FindByToken(string token);
        void Update(string token,AdminUpdateInfoEntityModel entity);
        void Delete(int id);
        int Count();
        void SendCode(string E_Mail);
    }
}
