using EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface I_Tag
    {
        List<TagEntityModel> List(int page, int pagesize);
        TagEntityModel FindByID(int id);
        void Add(string tagName);
        void Update(int id, string tagName);
        void Delete(int id);
        int Count();
    }
}
