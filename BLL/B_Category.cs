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
    public class B_Category : I_Category
    {
        public void Add(string categoryName)
        {
            CreateFactory.GetCategory().Add(categoryName);
        }

        public void Delete(int id)
        {
            CreateFactory.GetCategory().Delete(id);
        }

        public CategoryEntityModel FindByID(int id)
        {
            return CreateFactory.GetCategory().FindByID(id);
        }

        public List<CategoryEntityModel> List(int page, int pagesize)
        {
            return CreateFactory.GetCategory().List(page, pagesize);
        }

        public void Update(int id, string categoryName)
        {
            CreateFactory.GetCategory().Update(id, categoryName);
        }
    }
}
