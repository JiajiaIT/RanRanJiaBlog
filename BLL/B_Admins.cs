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
    public class B_Admins : I_Admins
    {
        public int Count()
        {
            return CreateFactory.GetAdmins().Count();
        }

        public void Delete(int id)
        {
            CreateFactory.GetAdmins().Delete(id);
        }

        public AdminInfoEntityModel FindByID(int id)
        {
            return CreateFactory.GetAdmins().FindByID(id);
        }

        public AdminInfoEntityModel FindByToken(string token)
        {
            return CreateFactory.GetAdmins().FindByToken(token);
        }

        public List<AdminInfoEntityModel> List(int page, int pagesize)
        {
            return CreateFactory.GetAdmins().List(page, pagesize);
        }

        public string Login(LoginEntityModel entity)
        {
            return CreateFactory.GetAdmins().Login(entity);
        }

        public void Register(RegisterEntityModel entity)
        {
            CreateFactory.GetAdmins().Register(entity);
        }

        public void SendCode(string E_Mail)
        {
            CreateFactory.GetAdmins().SendCode(E_Mail);
        }

        public void Update(string token, AdminUpdateInfoEntityModel entity)
        {
            CreateFactory.GetAdmins().Update(token, entity);
        }
    }
}
