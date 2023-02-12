using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;

namespace IDAL
{
    public interface I_Blogs
    {
        List<BlogEntityModel> GetAll(int page, int pagesize);
        List<BlogEntityModel> ManagementList(int page, int pagesize, string token);
        List<BlogEntityModel> List(int page, int pagesize);
        List<BlogEntityModel> Search(string name);
        List<BlogEntityModel> FindByCategory(int page, int pagesize, string categoryName);
        int FindByCategoryCount(string categoryName);
        List<BlogEntityModel> FindByTag(int page, int pagesize, string tagName);
        int FindByTagCount(string tagName);
        BlogEntityModel FindByID(int id);
        BlogEntityModel ManagementFindByID(int id, string token);
        void Add(BlogAddEntityModel entity);
        void Delete(int id);
        void Update(int id, BlogAddEntityModel entity);
        int GetAllCount();
        int ManagementListCount(string token);
        int ListCount();
    }
}
