using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EntityModel;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 管理员
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        public B_Admins _B_Admin;
        public B_Extend _B_Extend;
        public AdminsController()
        {
            _B_Admin = new B_Admins();
            _B_Extend = new B_Extend();
        }
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="entity">管理员</param>
        /// <returns></returns>
        [HttpPost, Route("Login")]
        public Result<string> Login([FromBody] LoginEntityModel entity)
        {
            try
            {
                var data = _B_Admin.Login(entity);
                var result = new Result<string>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<string>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页个数</param>
        /// <returns></returns>
        [HttpGet, Route("List/{page}/{pagesize}")]
        public Result<List<AdminInfoEntityModel>> List(int page, int pagesize)
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
                    What = "请求管理员列表",
                };
                _B_Extend.WriteLog(writeLog);
                var data = _B_Admin.List(page, pagesize);
                var result = new Result<List<AdminInfoEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<AdminInfoEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据ID查询管理员
        /// </summary>
        /// <param name="id">管理员ID</param>
        /// <returns></returns>
        [HttpGet, Route("FindByID/{id}")]
        public Result<AdminInfoEntityModel> FindByID(int id)
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
                    What = "请求根据ID查询管理员",
                };
                _B_Extend.WriteLog(writeLog);
                var data = _B_Admin.FindByID(id);
                var result = new Result<AdminInfoEntityModel>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<AdminInfoEntityModel>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 根据Token查询管理员
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        [HttpGet, Route("FindByToken/{token}")]
        public Result<AdminInfoEntityModel> FindByToken(string token)
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
                    What = "请求根据Token查询管理员",
                };
                _B_Extend.WriteLog(writeLog);
                var data = _B_Admin.FindByToken(token);
                var result = new Result<AdminInfoEntityModel>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<AdminInfoEntityModel>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }
        /// <summary>
        /// 管理员数量
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
                    What = "请求查询管理员数量",
                };
                _B_Extend.WriteLog(writeLog);
                var count = _B_Admin.Count();
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
        /// <summary>
        /// 管理员注册
        /// </summary>
        /// <param name="entity">管理员</param>
        /// <returns></returns>
        [HttpPost, Route("Register")]
        public Result<object> Register([FromForm] RegisterEntityModel entity)
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
                    What = "请求注册管理员",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Admin.Register(entity);
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
        /// 管理员删除
        /// </summary>
        /// <param name="id">管理员ID</param>
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
                    What = "请求删除管理员",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Admin.Delete(id);
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
        /// 管理员修改信息
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="entity">管理员信息</param>
        /// <returns></returns>
        [HttpPost, Route("Update/{token}")]
        public Result<object> Update(string token, [FromForm] AdminUpdateInfoEntityModel entity)
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
                    What = "请求修改管理员信息",
                };
                _B_Extend.WriteLog(writeLog);
                _B_Admin.Update(token, entity);
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
        /// 发送Code登录验证码
        /// </summary>
        /// <param name="e_Mail">邮箱</param>
        /// <returns></returns>
        [HttpPost, Route("SendCode")]
        public Result<object> SendCode([FromForm] string e_Mail)
        {
            try
            {
                _B_Admin.SendCode(e_Mail);
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
        /// 日志列表
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页个数</param>
        /// <returns></returns>
        [HttpGet, Route("ReadLogs/{page}/{pagesize}")]
        public Result<List<LogEntityModel>> ReadLogs(int page, int pagesize)
        {
            try
            {
                var Token = Request.Headers["Token"];
                if (!_B_Extend.IsSuperAdmin(Token))
                {
                    throw new Exception("没有权限访问");
                }
                var data = _B_Extend.Read(page, pagesize);
                var result = new Result<List<LogEntityModel>>
                {
                    Data = data
                };
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<List<LogEntityModel>>
                {
                    State = false,
                    Msg = ex.Message
                };
                return result;
            }
        }

    }
}
