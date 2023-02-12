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
    public class TagController : ControllerBase
    {
        public B_Tag _B_Tag;
        public B_Extend _B_Extend;
        public TagController()
        {
            _B_Tag = new B_Tag();
            _B_Extend = new B_Extend();
        }
        /// <summary>
        /// 标签列表
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页个数</param>
        /// <returns></returns>
        [HttpGet, Route("List/{page}/{pagesize}")]
        public Result<List<TagEntityModel>> List(int page, int pagesize)
        {
            try
            {
                var data = _B_Tag.List(page, pagesize);
                var result = new Result<List<TagEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<TagEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据ID查询标签
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        [HttpGet, Route("FindByID/{id}")]
        public Result<TagEntityModel> FindByID(int id)
        {
            try
            {
                var data = _B_Tag.FindByID(id);
                var result = new Result<TagEntityModel>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<TagEntityModel>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="tagName">标签名</param>
        /// <returns></returns>
        [HttpPost, Route("Add")]
        public Result<object> Add([FromForm] string tagName)
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
                    What = "请求添加标签",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Tag.Add(tagName);
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
        /// 根据ID修改标签
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <param name="tagName">标签名</param>
        /// <returns></returns>
        [HttpPost, Route("Update/{id}")]
        public Result<object> Update(int id, [FromForm] string tagName)
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
                    What = "请求修改标签",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Tag.Update(id, tagName);
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
        /// 根据ID删除标签
        /// </summary>
        /// <param name="id">标签ID</param>
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
                    What = "请求删除标签",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Tag.Delete(id);
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
        /// 标签数量
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Count")]
        public Result<int> Count()
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
                    What = "请求查询标签数量",
                };
                _B_Extend.WriteLog(writeLog);
                var count = _B_Tag.Count();
                var result = new Result<int>
                {
                    Data = count
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
    }
}
