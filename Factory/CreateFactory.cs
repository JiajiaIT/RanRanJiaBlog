using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;//反射命名空间
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace Factory
{
    public class CreateFactory
    {
        public static I_Admins GetAdmins()
        {
            return (I_Admins)Assembly.Load("DAL").CreateInstance("DAL.D_Admins");
        }
        public static I_Extend Get_Extend()
        {
            return (I_Extend)Assembly.Load("DAL").CreateInstance("DAL.D_Extend");
        }
        public static I_Category GetCategory()
        {
            return (I_Category)Assembly.Load("DAL").CreateInstance("DAL.D_Category");
        }
    }
}
