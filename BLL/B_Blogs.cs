using Factory;
using EntityModel;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class B_Blogs : I_Blogs
    {
        public void Add(BlogAddEntityModel entity)
        {
            CreateFactory.GetBlogs().Add(entity);
        }

        public void Delete(int id)
        {
            CreateFactory.GetBlogs().Delete(id);
        }

        public List<BlogEntityModel> FindByCategory(int page, int pagesize, string categoryName)
        {
            return CreateFactory.GetBlogs().FindByCategory(page, pagesize, categoryName);
        }

        public int FindByCategoryCount(string categoryName)
        {
            return CreateFactory.GetBlogs().FindByCategoryCount(categoryName);
        }

        public BlogEntityModel FindByID(int id)
        {
            return CreateFactory.GetBlogs().FindByID(id);
        }

        public List<BlogEntityModel> FindByTag(int page, int pagesize, string tagName)
        {
            return CreateFactory.GetBlogs().FindByTag(page, pagesize, tagName);
        }

        public int FindByTagCount(string tagName)
        {
            return CreateFactory.GetBlogs().FindByTagCount(tagName);
        }

        public List<BlogEntityModel> GetAll(int page, int pagesize)
        {
            return CreateFactory.GetBlogs().GetAll(page, pagesize);
        }

        public int GetAllCount()
        {
            return CreateFactory.GetBlogs().GetAllCount();
        }

        public List<BlogEntityModel> List(int page, int pagesize)
        {
            return CreateFactory.GetBlogs().List(page, pagesize);
        }

        public int ListCount()
        {
            return CreateFactory.GetBlogs().ListCount();
        }

        public BlogEntityModel ManagementFindByID(int id, string token)
        {
            return CreateFactory.GetBlogs().ManagementFindByID(id, token);
        }

        public List<BlogEntityModel> ManagementList(int page, int pagesize, string token)
        {
            return CreateFactory.GetBlogs().ManagementList(page, pagesize, token);
        }

        public int ManagementListCount(string token)
        {
            return CreateFactory.GetBlogs().ManagementListCount(token);
        }

        public List<BlogEntityModel> Search(string name)
        {
            return CreateFactory.GetBlogs().Search(name);
        }

        public void Update(int id, BlogAddEntityModel entity)
        {
            CreateFactory.GetBlogs().Update(id, entity);
        }
    }
}
