using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EntityModel;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 博客
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        public B_Blogs _B_Blogs;
        public B_Extend _B_Extend;
        public BlogsController()
        {
            _B_Blogs = new B_Blogs();
            _B_Extend = new B_Extend();
        }
        /// <summary>
        /// 获取所有博客列表
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页个数</param>
        /// <returns></returns>
        [HttpGet, Route("GetAll/{page}/{pagesize}")]
        public Result<List<BlogEntityModel>> GetAll(int page, int pagesize)
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.IsSuperAdmin(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                var writeLog = new LogEntityModel
                {
                    IP = ip,
                    Who = Request.Headers["Token"],
                    What = "请求获取所有博客列表",
                };
                var data = _B_Blogs.GetAll(page, pagesize);
                var result = new Result<List<BlogEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<BlogEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 管理员获取博客列表
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页个数</param>
        /// <returns></returns>
        [HttpGet, Route("ManagementList/{page}/{pagesize}")]
        public Result<List<BlogEntityModel>> ManagementList(int page, int pagesize)
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.Authorize(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                var writeLog = new LogEntityModel
                {
                    IP = ip,
                    Who = Request.Headers["Token"],
                    What = "管理员获取博客列表",
                };
                _B_Extend.WriteLog(writeLog);
                var data = _B_Blogs.ManagementList(page, pagesize, Token);
                var result = new Result<List<BlogEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<BlogEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 博客列表
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页个数</param>
        /// <returns></returns>
        [HttpGet, Route("List/{page}/{pagesize}")]
        public Result<List<BlogEntityModel>> List(int page, int pagesize)
        {
            try
            {
                var data = _B_Blogs.List(page, pagesize);
                var result = new Result<List<BlogEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<BlogEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 标题模糊搜索查询
        /// </summary>
        /// <param name="name">标题</param>
        /// <returns></returns>
        [HttpGet, Route("Search")]
        public Result<List<BlogEntityModel>> Search([FromForm] string name)
        {
            try
            {
                var data = _B_Blogs.Search(name);
                var result = new Result<List<BlogEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<BlogEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据ID查询博客
        /// </summary>
        /// <param name="id">博客ID</param>
        /// <returns></returns>
        [HttpGet, Route("FindByID/{id}")]
        public Result<BlogEntityModel> FindByID(int id)
        {
            try
            {
                var data = _B_Blogs.FindByID(id);
                var result = new Result<BlogEntityModel>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<BlogEntityModel>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 管理员根据博客ID查询博客详情
        /// </summary>
        /// <param name="id">博客ID</param>
        /// <returns></returns>
        [HttpGet, Route("ManagementFindByID/{id}")]
        public Result<BlogEntityModel> ManagementFindByID(int id)
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.Authorize(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                var writeLog = new LogEntityModel
                {
                    IP = ip,
                    Who = Request.Headers["Token"],
                    What = "管理员根据博客ID查询博客详情",
                };
                _B_Extend.WriteLog(writeLog);
                var data = _B_Blogs.ManagementFindByID(id, Token);
                var result = new Result<BlogEntityModel>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<BlogEntityModel>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据分类名查询博客列表
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="pagesize">每页个数</param>
        /// <param name="categoryName">分类名</param>
        /// <returns></returns>
        [HttpGet, Route("FindByCategory/{page}/{pagesize}/{categoryName}")]
        public Result<List<BlogEntityModel>> FindByCategory(int page, int pagesize, string categoryName)
        {
            try
            {
                var data = _B_Blogs.FindByCategory(page, pagesize, categoryName);
                var result = new Result<List<BlogEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<BlogEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据分类查询博客的数量
        /// </summary>
        /// <param name="categoryName">分类名</param>
        /// <returns></returns>
        [HttpGet, Route("FindByCategoryCount/{categoryName}")]
        public Result<int> FindByCategoryCount(string categoryName)
        {
            try
            {
                var data = _B_Blogs.FindByCategoryCount(categoryName);
                var result = new Result<int>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<int>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据标签名查询博客列表
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="pagesize">每页个数</param>
        /// <param name="tagName">标签名</param>
        /// <returns></returns>
        [HttpGet, Route("FindByTag/{page}/{pagesize}/{tagName}")]
        public Result<List<BlogEntityModel>> FindByTag(int page, int pagesize, string tagName)
        {
            try
            {
                var data = _B_Blogs.FindByTag(page, pagesize, tagName);
                var result = new Result<List<BlogEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<BlogEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据标签查询博客的数量
        /// </summary>
        /// <param name="tagName">标签名</param>
        /// <returns></returns>
        [HttpGet, Route("FindByTagCount/{tagName}")]
        public Result<int> FindByTagCount(string tagName)
        {
            try
            {
                var data = _B_Blogs.FindByTagCount(tagName);
                var result = new Result<int>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<int>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 获取所有博客数量
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAllCount")]
        public Result<int> GetAllCount()
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.IsSuperAdmin(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                var writeLog = new LogEntityModel
                {
                    IP = ip,
                    Who = Request.Headers["Token"],
                    What = "请求获取所有博客数量",
                };
                var data = _B_Blogs.GetAllCount();
                var result = new Result<int>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<int>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 管理员获取博客数量
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ManagementListCount")]
        public Result<int> ManagementListCount()
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.Authorize(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                var writeLog = new LogEntityModel
                {
                    IP = ip,
                    Who = Request.Headers["Token"],
                    What = "管理员获取博客数量",
                };
                var data = _B_Blogs.ManagementListCount(Token);
                var result = new Result<int>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<int>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 博客数量
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ListCount")]
        public Result<int> ListCount()
        {
            try
            {
                var data = _B_Blogs.ListCount();
                var result = new Result<int>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<int>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="entity">博客</param>
        /// <returns></returns>
        [HttpPost, Route("Add")]
        public Result<object> Add([FromForm] BlogAddEntityModel entity)
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.Authorize(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                var writeLog = new LogEntityModel
                {
                    IP = ip,
                    Who = Request.Headers["Token"],
                    What = "请求添加博客",
                };
                _B_Extend.WriteLog(writeLog);
                entity.Token = Token;
                _B_Blogs.Add(entity);
                return new Result<object>();
            }
            catch (Exception ex)
            {
                var result = new Result<object>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id">博客ID</param>
        /// <returns></returns>
        [HttpPost, Route("Delete/{id}")]
        public Result<object> Delete(int id)
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.Authorize(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                var writeLog = new LogEntityModel
                {
                    IP = ip,
                    Who = Request.Headers["Token"],
                    What = "请求删除博客",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Blogs.Delete(id);
                return new Result<object>();
            }
            catch (Exception ex)
            {
                var result = new Result<object>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 博客修改
        /// </summary>
        /// <param name="id">博客ID</param>
        /// <param name="entity">博客</param>
        /// <returns></returns>
        [HttpPost, Route("Update/{id}")]
        public Result<object> Update(int id, [FromForm] BlogAddEntityModel entity)
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.Authorize(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                var writeLog = new LogEntityModel
                {
                    IP = ip,
                    Who = Request.Headers["Token"],
                    What = "请求修改博客",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Blogs.Update(id, entity);
                return new Result<object>();
            }
            catch (Exception ex)
            {
                var result = new Result<object>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
    }
}
