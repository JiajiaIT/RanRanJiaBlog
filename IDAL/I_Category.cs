using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;

namespace IDAL
{
    public interface I_Category
    {
        List<CategoryEntityModel> List(int page, int pagesize);
        CategoryEntityModel FindByID(int id);
        void Add(string categoryName);
        void Update(int id, string categoryName);
        void Delete(int id);
        int Count();
    }
}
