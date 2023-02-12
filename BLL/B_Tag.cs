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
    public class B_Tag : I_Tag
    {
        public void Add(string tagName)
        {
            CreateFactory.GetTag().Add(tagName);
        }

        public int Count()
        {
            return CreateFactory.GetTag().Count();
        }

        public void Delete(int id)
        {
            CreateFactory.GetTag().Delete(id);
        }

        public TagEntityModel FindByID(int id)
        {
            return CreateFactory.GetTag().FindByID(id);
        }

        public List<TagEntityModel> List(int page, int pagesize)
        {
            return CreateFactory.GetTag().List(page, pagesize);
        }

        public void Update(int id, string tagName)
        {
            CreateFactory.GetTag().Update(id, tagName);
        }
    }
}
