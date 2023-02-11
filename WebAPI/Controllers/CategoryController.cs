using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Common;
using EntityModel;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 分类
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public B_Category _B_Category;
        public B_Extend _B_Extend;
        public CategoryController()
        {
            _B_Category = new B_Category();
            _B_Extend = new B_Extend();
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页个数</param>
        /// <returns></returns>
        [HttpGet, Route("List/{page}/{pagesize}")]
        public Result<List<CategoryEntityModel>> List(int page, int pagesize)
        {
            try
            {
                var data = _B_Category.List(page, pagesize);
                var result = new Result<List<CategoryEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<CategoryEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据ID查询分类
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        [HttpGet, Route("FindByID/{id}")]
        public Result<CategoryEntityModel> FindByID(int id)
        {
            try
            {
                var data = _B_Category.FindByID(id);
                var result = new Result<CategoryEntityModel>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<CategoryEntityModel>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="categoryName">分类名</param>
        /// <returns></returns>
        [HttpPost, Route("Add")]
        public Result<object> Add([FromForm] string categoryName)
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
                    What = "请求添加分类",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Category.Add(categoryName);
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
        /// 根据ID修改分类
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <param name="categoryName">分类名</param>
        /// <returns></returns>
        [HttpPost, Route("Update/{id}")]
        public Result<object> Update(int id, [FromForm] string categoryName)
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
                    What = "请求修改分类",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Category.Update(id, categoryName);
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
        /// 根据ID删除分类
        /// </summary>
        /// <param name="id">分类ID</param>
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
                    What = "请求删除分类",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Category.Delete(id);
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
