using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Common;
using EntityModel;
using EntityModel.SqlServer;
using IDAL;
using Microsoft.IdentityModel.Tokens;
using static System.Reflection.Metadata.BlobBuilder;

namespace DAL
{
    public class D_Blogs : I_Blogs
    {
        RanRanJiaBlogContext _db;
        public D_Blogs()
        {
            _db = new RanRanJiaBlogContext();
        }
        public void Add(BlogAddEntityModel entity)
        {
            string _Category = "";
            string _Tag = "";
            if (entity.Category != null)
            {
                _Category = Tools.ArrToStr(entity.Category);
            }
            if (entity.Tag != null)
            {
                _Tag = Tools.ArrToStr(entity.Tag);
            }
            if (entity.IsShow == null)
            {
                entity.IsShow = true;
            }
            var _CreateBy = _db.Tokens.Where(x => x.Token1 == entity.Token).First().AdminId;
            var _Blog = new Blog
            {
                Title = entity.Title,
                Img = entity.Img,
                Content = entity.Content,
                Category = _Category,
                Tag = _Tag,
                ViewNumBer = 0,
                CreateTime = DateTime.Now,
                CreateBy = _CreateBy,
                IsShow = entity.IsShow
            };
            _db.Blogs.Add(_Blog);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var _Blog = _db.Blogs.First(x => x.Id == id);
            _db.Blogs.Remove(_Blog);
            _db.SaveChanges();
        }

        public List<BlogEntityModel> FindByCategory(int page, int pagesize, string categoryName)
        {
            var number = (page - 1) * pagesize;
            var data = _db.Blogs.Where(x => x.IsShow == true);
            var _Blogs = data.Where(x => x.Category.Contains(categoryName) || x.Category.StartsWith(categoryName) || x.Category.EndsWith(categoryName)).OrderByDescending(x => x.CreateTime).Skip(number).Take(pagesize).ToList();
            var _AdminInfo = _db.AdminInfos.ToList();
            var result = from res in _Blogs
                         join admininfo in _AdminInfo on res.CreateBy equals admininfo.AdminId
                         select new BlogEntityModel
                         {
                             ID = res.Id,
                             Title = res.Title,
                             Img = res.Img,
                             Content = res.Content,
                             Category = Tools.StrToArr(res.Category),
                             Tag = Tools.StrToArr(res.Tag),
                             ViewNumBer = res.ViewNumBer,
                             CreateTime = res.CreateTime,
                             CreateBy = admininfo.NickName,
                             IsShow = res.IsShow
                         };
            if (result.Count() > 0)
            {
                return result.ToList();
            }
            else
            {
                throw new Exception("没查询到");
            }
        }

        public int FindByCategoryCount(string categoryName)
        {

            var data = _db.Blogs.Where(x => x.IsShow == true);
            var count = data.Where(x => x.Category.Contains(categoryName) || x.Category.StartsWith(categoryName) || x.Category.EndsWith(categoryName)).Count();
            return count;
        }

        public BlogEntityModel FindByID(int id)
        {
            var _Blog = _db.Blogs.First(x => x.Id == id);
            if (_Blog.IsShow.Value)
            {
                var _CreateBy = _db.AdminInfos.First(x => x.AdminId == _Blog.CreateBy).NickName;
                var _Category = Tools.StrToArr(_Blog.Category);
                var _Tag = Tools.StrToArr(_Blog.Tag);
                var result = new BlogEntityModel
                {
                    ID = _Blog.Id,
                    Title = _Blog.Title,
                    Img = _Blog.Img,
                    Content = _Blog.Content,
                    Category = _Category,
                    Tag = _Tag,
                    CreateBy = _CreateBy,
                    IsShow = _Blog.IsShow,
                    CreateTime = _Blog.CreateTime,
                    ViewNumBer = _Blog.ViewNumBer
                };
                _Blog.ViewNumBer++;
                _db.SaveChanges();
                return result;
            }
            else
            {
                throw new Exception("没有查询到");
            }
        }

        public List<BlogEntityModel> FindByTag(int page, int pagesize, string tagName)
        {
            var number = (page - 1) * pagesize;
            var data = _db.Blogs.Where(x => x.IsShow == true);
            var _Blogs = data.Where(x => x.Tag.Contains(tagName) || x.Tag.StartsWith(tagName) || x.Tag.EndsWith(tagName)).OrderByDescending(x => x.CreateTime).Skip(number).Take(pagesize).ToList();
            var _AdminInfo = _db.AdminInfos.ToList();
            var result = from res in _Blogs
                         join admininfo in _AdminInfo on res.CreateBy equals admininfo.AdminId
                         select new BlogEntityModel
                         {
                             ID = res.Id,
                             Title = res.Title,
                             Img = res.Img,
                             Content = res.Content,
                             Category = Tools.StrToArr(res.Category),
                             Tag = Tools.StrToArr(res.Tag),
                             ViewNumBer = res.ViewNumBer,
                             CreateTime = res.CreateTime,
                             CreateBy = admininfo.NickName,
                             IsShow = res.IsShow
                         };
            if (result.Count() > 0)
            {
                return result.ToList();
            }
            else
            {
                throw new Exception("没查询到");
            }
        }

        public int FindByTagCount(string tagName)
        {
            var data = _db.Blogs.Where(x => x.IsShow == true);
            var count = data.Where(x => x.Tag.Contains(tagName) || x.Tag.StartsWith(tagName) || x.Tag.EndsWith(tagName)).Count();
            return count;
        }

        public List<BlogEntityModel> GetAll(int page, int pagesize)
        {
            var number = (page - 1) * pagesize;
            var _Blogs = _db.Blogs.OrderByDescending(x => x.CreateTime).Skip(number).Take(pagesize).ToList();
            var _AdminInfo = _db.AdminInfos.ToList();
            var result = from res in _Blogs
                         join admininfo in _AdminInfo on res.CreateBy equals admininfo.AdminId
                         select new BlogEntityModel
                         {
                             ID = res.Id,
                             Title = res.Title,
                             Img = res.Img,
                             Content = res.Content,
                             Category = Tools.StrToArr(res.Category),
                             Tag = Tools.StrToArr(res.Tag),
                             ViewNumBer = res.ViewNumBer,
                             CreateTime = res.CreateTime,
                             CreateBy = admininfo.NickName,
                             IsShow = res.IsShow
                         };
            if (result.Count() > 0)
            {
                return result.ToList();
            }
            else
            {
                throw new Exception("没查询到");
            }
        }

        public int GetAllCount()
        {
            int count = _db.Blogs.Count();
            return count;
        }

        public List<BlogEntityModel> List(int page, int pagesize)
        {
            var number = (page - 1) * pagesize;
            var _Blogs = _db.Blogs.Where(x => x.IsShow == true).OrderByDescending(x => x.CreateTime).Skip(number).Take(pagesize).ToList();
            var _AdminInfo = _db.AdminInfos.ToList();
            var result = from res in _Blogs
                         join admininfo in _AdminInfo on res.CreateBy equals admininfo.AdminId
                         select new BlogEntityModel
                         {
                             ID = res.Id,
                             Title = res.Title,
                             Img = res.Img,
                             Content = res.Content,
                             Category = Tools.StrToArr(res.Category),
                             Tag = Tools.StrToArr(res.Tag),
                             ViewNumBer = res.ViewNumBer,
                             CreateTime = res.CreateTime,
                             CreateBy = admininfo.NickName,
                             IsShow = res.IsShow
                         };
            if (result.Count() > 0)
            {
                return result.ToList();
            }
            else
            {
                throw new Exception("没查询到");
            }
        }

        public int ListCount()
        {
            var count = _db.Blogs.Where(x => x.IsShow == true).Count();
            return count;
        }

        public BlogEntityModel ManagementFindByID(int id, string token)
        {
            var adminId = _db.Tokens.First(x => x.Token1 == token).AdminId;
            var _Blog = _db.Blogs.First(x => x.Id == id && x.CreateBy == adminId);
            var _CreateBy = _db.AdminInfos.First(x => x.AdminId == _Blog.CreateBy).NickName;
            var _Category = Tools.StrToArr(_Blog.Category);
            var _Tag = Tools.StrToArr(_Blog.Tag);
            var result = new BlogEntityModel
            {
                ID = _Blog.Id,
                Title = _Blog.Title,
                Img = _Blog.Img,
                Content = _Blog.Content,
                Category = _Category,
                Tag = _Tag,
                CreateBy = _CreateBy,
                IsShow = _Blog.IsShow,
                CreateTime = _Blog.CreateTime,
                ViewNumBer = _Blog.ViewNumBer
            };
            _Blog.ViewNumBer++;
            _db.SaveChanges();
            return result;
        }

        public List<BlogEntityModel> ManagementList(int page, int pagesize, string token)
        {
            var adminId = _db.Tokens.First(x => x.Token1 == token).AdminId;
            var number = (page - 1) * pagesize;
            var _Blogs = _db.Blogs.Where(x => x.CreateBy == adminId).OrderByDescending(x => x.CreateTime).Skip(number).Take(pagesize).ToList();
            var _AdminInfo = _db.AdminInfos.ToList();
            var result = from res in _Blogs
                         join admininfo in _AdminInfo on res.CreateBy equals admininfo.AdminId
                         select new BlogEntityModel
                         {
                             ID = res.Id,
                             Title = res.Title,
                             Img = res.Img,
                             Content = res.Content,
                             Category = Tools.StrToArr(res.Category),
                             Tag = Tools.StrToArr(res.Tag),
                             ViewNumBer = res.ViewNumBer,
                             CreateTime = res.CreateTime,
                             CreateBy = admininfo.NickName,
                             IsShow = res.IsShow
                         };
            if (result.Count() > 0)
            {
                return result.ToList();
            }
            else
            {
                throw new Exception("没查询到");
            }
        }

        public int ManagementListCount(string token)
        {
            var adminId = _db.Tokens.First(x => x.Token1 == token).AdminId;
            var count = _db.Blogs.Where(x => x.CreateBy == adminId).Count();
            return count;
        }

        public List<BlogEntityModel> Search(string name)
        {
            var data = _db.Blogs.Where(x => x.IsShow == true);
            var _Blogs = data.Where(x => x.Title.Contains(name) || x.Title.StartsWith(name) || x.Title.EndsWith(name)).ToList();
            var _AdminInfo = _db.AdminInfos.ToList();
            var result = from res in _Blogs
                         join admininfo in _AdminInfo on res.CreateBy equals admininfo.AdminId
                         select new BlogEntityModel
                         {
                             ID = res.Id,
                             Title = res.Title,
                             Img = res.Img,
                             Content = res.Content,
                             Category = Tools.StrToArr(res.Category),
                             Tag = Tools.StrToArr(res.Tag),
                             ViewNumBer = res.ViewNumBer,
                             CreateTime = res.CreateTime,
                             CreateBy = admininfo.NickName,
                             IsShow = res.IsShow
                         };
            if (result.Count() > 0)
            {
                return result.ToList();
            }
            else
            {
                throw new Exception("没查询到");
            }
        }

        public void Update(int id, BlogAddEntityModel entity)
        {
            var _Blog = _db.Blogs.First(x => x.Id == id);
            if (!string.IsNullOrEmpty(entity.Title))
            {
                _Blog.Title = entity.Title;
            }
            if (!string.IsNullOrEmpty(entity.Img))
            {
                _Blog.Img = entity.Img;
            }
            if (!string.IsNullOrEmpty(entity.Content))
            {
                _Blog.Content = entity.Content;
            }
            if (entity.IsShow != null)
            {
                _Blog.IsShow = entity.IsShow;
            }
            if (entity.Category != null)
            {
                if (entity.Category.Length > 0)
                {
                    string _Category = "";
                    _Blog.Category = _Category = Tools.ArrToStr(entity.Category);
                }
            }
            if (entity.Tag != null)
            {
                if (entity.Tag.Length > 0)
                {
                    string _Tag = "";
                    _Blog.Tag = _Tag = Tools.ArrToStr(entity.Tag);
                }
            }
            _db.SaveChanges();
        }
    }
}
