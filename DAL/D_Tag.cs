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
    public class D_Tag : I_Tag
    {
        RanRanJiaBlogContext _db;
        public D_Tag()
        {
            _db = new RanRanJiaBlogContext();
        }

        public void Add(string tagName)
        {
            var isExist = _db.Tags.Where(x => x.TagName == tagName).Count() > 0;
            if (!isExist)
            {
                var _Tag = new Tag
                {
                    TagName = tagName
                };
                _db.Tags.Add(_Tag);
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("该标签已存在");
            }
        }

        public int Count()
        {
            var count = _db.Tags.Count();
            return count;
        }

        public void Delete(int id)
        {
            var _Tag = _db.Tags.First(x => x.Id == id);
            _db.Tags.Remove(_Tag);
            _db.SaveChanges();
        }

        public TagEntityModel FindByID(int id)
        {
            var _Tag = _db.Tags.First(x => x.Id == id);
            var result = new TagEntityModel
            {
                ID = _Tag.Id,
                TagName = _Tag.TagName
            };
            return result;
        }

        public List<TagEntityModel> List(int page, int pagesize)
        {
            var number = (page - 1) * pagesize;
            var data = _db.Tags.OrderByDescending(x => x.Id).Skip(number).Take(pagesize).ToList();
            var result = (from res in data
                          select new TagEntityModel
                          {
                              ID = res.Id,
                              TagName = res.TagName
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

        public void Update(int id, string tagName)
        {
            var _Tag = _db.Tags.First(x => x.Id == id);
            var isExist = _db.Tags.Where(x => x.TagName == tagName).Count() > 0;
            if (!isExist)
            {
                _Tag.TagName = tagName;
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("该标签已存在");
            }
        }
    }
}
