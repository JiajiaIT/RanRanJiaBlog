using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;
using EntityModel.SqlServer;
using IDAL;

namespace DAL
{
    public class D_Category : I_Category
    {
        RanRanJiaBlogContext _db;
        public D_Category()
        {
            _db = new RanRanJiaBlogContext();
        }

        public void Add(string categoryName)
        {
            var isExist = _db.Categories.Where(x => x.CategoryName == categoryName).Count() > 0;
            if (!isExist)
            {
                var _Category = new Category
                {
                    CategoryName = categoryName
                };
                _db.Categories.Add(_Category);
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("该分类已存在");
            }
        }

        public int Count()
        {
            var count = _db.Categories.Count();
            return count;
        }

        public void Delete(int id)
        {
            var _Category = _db.Categories.First(x => x.Id == id);
            _db.Categories.Remove(_Category);
            _db.SaveChanges();
        }

        public CategoryEntityModel FindByID(int id)
        {
            var _Category = _db.Categories.First(x => x.Id == id);
            var result = new CategoryEntityModel
            {
                ID = _Category.Id,
                CategoryName = _Category.CategoryName
            };
            return result;
        }

        public List<CategoryEntityModel> List(int page, int pagesize)
        {
            var number = (page - 1) * pagesize;
            var data = _db.Categories.OrderByDescending(x => x.Id).Skip(number).Take(pagesize).ToList();
            var result = (from res in data
                          select new CategoryEntityModel
                          {
                              ID = res.Id,
                              CategoryName = res.CategoryName
                          }).ToList();
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                throw new Exception("没查询到");
            }
        }

        public void Update(int id, string categoryName)
        {
            var _Category = _db.Categories.First(x => x.Id == id);
            var isExist = _db.Categories.Where(x => x.CategoryName == categoryName).Count() > 0;
            if (!isExist)
            {
                _Category.CategoryName = categoryName;
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("该分类已存在");
            }
        }
    }
}
