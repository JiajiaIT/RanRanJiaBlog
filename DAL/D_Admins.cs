using Common;
using EntityModel;
using EntityModel.SqlServer;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class D_Admins : I_Admins
    {
        RanRanJiaBlogContext _db;
        public D_Admins()
        {
            _db = new RanRanJiaBlogContext();
        }

        public int Count()
        {
            var count = _db.Admins.Count();
            return count;
        }

        public void Delete(int id)
        {
            var _Admin = _db.Admins.Where(x => x.Id == id).First();
            var _Token = _db.Tokens.First(x => x.AdminId == _Admin.Id);
            var _AdminInfo = _db.AdminInfos.Where(x => x.AdminId == _Admin.Id).First();
            _db.Tokens.Remove(_Token);
            _db.AdminInfos.Remove(_AdminInfo);
            _db.Admins.Remove(_Admin);
            _db.SaveChanges();
        }

        public AdminInfoEntityModel FindByID(int id)
        {
            var _Admin = _db.Admins.Where(x => x.Id == id).First();
            var _Token = _db.Tokens.First(x => x.AdminId == _Admin.Id);
            var _AdminInfo = _db.AdminInfos.Where(x => x.AdminId == _Admin.Id).First();
            var result = new AdminInfoEntityModel
            {
                ID = _Admin.Id,
                Account = _Admin.Account,
                E_mail = _Admin.EMail,
                Img = _AdminInfo.Img,
                NickName = _AdminInfo.NickName
            };
            return result;
        }

        public AdminInfoEntityModel FindByToken(string token)
        {
            var _Token = _db.Tokens.First(x => x.Token1 == token);
            var _Admin = _db.Admins.First(x => x.Id == _Token.AdminId);
            var _AdminInfo = _db.AdminInfos.First(x => x.AdminId == _Admin.Id);
            var result = new AdminInfoEntityModel()
            {
                ID = _Admin.Id,
                Account = _Admin.Account,
                E_mail = _Admin.EMail,
                Img = _AdminInfo.Img,
                NickName = _AdminInfo.NickName
            };
            return result;
        }

        public List<AdminInfoEntityModel> List(int page, int pagesize)
        {
            var number = (page - 1) * pagesize;
            var _Admin = _db.Admins.OrderByDescending(x => x.Id).Skip(number).Take(pagesize).ToList();
            var data = from admin in _Admin
                       join adminInfo in _db.AdminInfos on admin.Id equals adminInfo.AdminId
                       select new AdminInfoEntityModel
                       {
                           ID = admin.Id,
                           Account = admin.Account,
                           E_mail = admin.EMail,
                           Img = adminInfo.Img,
                           NickName = adminInfo.NickName
                       };
            var result = data.ToList();
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                throw new Exception("没查询到");
            }
        }

        public string Login(LoginEntityModel entity)
        {
            if (entity.Password != null&&entity.Password!="")
            {
                entity.Password = MD5Helper.MD5Encrypt64(entity.Password);
            }
            var isAccountLogin = entity.Account != null && entity.Account!="" && entity.Password != null &&entity.Password!="";
            var isE_MailLogin = entity.E_Mail != null && entity.Code != null;
            if (isAccountLogin)
            {
                var admins = _db.Admins.Where(x => x.Account == entity.Account && x.Password == entity.Password);
                var isLoginSuccess = admins.Count() > 0;
                if (isLoginSuccess)
                {
                    var _Admin = admins.First();
                    var _Token = _db.Tokens.First(x => x.AdminId == _Admin.Id);
                    var token = Guid.NewGuid().ToString().Split('-')[0];
                    _Token.Token1 = token;
                    _Token.ExpirationTime = DateTime.Now.AddHours(3);
                    _db.SaveChanges();
                    return token;
                }
                else
                {
                    throw new Exception("登录失败,用户名或密码有误");
                }
            }
            if (isE_MailLogin)
            {
                var admins = _db.Admins.Where(x => x.EMail == entity.E_Mail && x.Code == entity.Code);
                var isLoginSuccess = admins.Count() > 0;
                if (isLoginSuccess)
                {
                    var _Admin = admins.First();
                    var isExpire = _Admin.ExpirationTime > DateTime.Now;
                    if (isExpire)
                    {
                        var _Token = _db.Tokens.First(x => x.AdminId == _Admin.Id);
                        var token = Guid.NewGuid().ToString().Split('-')[0];
                        _Token.Token1 = token;
                        _Token.ExpirationTime = DateTime.Now.AddHours(3);
                        _db.SaveChanges();
                        return token;
                    }
                    else
                    {
                        throw new Exception("登录失败,Code已过期");
                    }
                }
                else
                {
                    throw new Exception("登录失败,邮箱或Code不正确");
                }
            }
            else
            {
                throw new Exception("登录失败");
            }
        }

        public void Register(RegisterEntityModel entity)
        {
            if (entity.Password != null)
            {
                entity.Password = MD5Helper.MD5Encrypt64(entity.Password);
            }
            var isExist = _db.Admins.Where(x => x.Account == entity.Account).Count() > 0;
            if (!isExist)
            {
                var _Admin = new Admin
                {
                    Account = entity.Account,
                    Password = entity.Password
                };
                _db.Admins.Add(_Admin);
                _db.SaveChanges();
                var adminID = _db.Admins.First(x => x.Account == entity.Account && x.Password == entity.Password).Id;
                var _Token = new Token
                {
                    AdminId = adminID
                };
                var _AdminInfo = new AdminInfo
                {
                    AdminId = adminID
                };
                _db.Tokens.Add(_Token);
                _db.AdminInfos.Add(_AdminInfo);
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("该管理员已存在!");
            }
        }

        public void SendCode(string E_Mail)
        {
            if (SendE_MailHelper.EmailFormat(E_Mail))
            {
                var admins = _db.Admins.Where(x => x.EMail == E_Mail);
                var isExist = admins.Count() > 0;
                if (isExist)
                {
                    Random random = new Random();
                    char[] chars = new char[6];
                    var str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
                    for (int i = 0; i < 6; i++)
                    {
                        chars[i] = str[random.Next(0, str.Length)];
                    }
                    string code = new String(chars);
                    SendE_MailHelper.SendEmail(E_Mail, "2748745861@qq.com", "2748745861@qq.com", "RanRanJiaBlog", "RanRanJiaBlog登录验证码", "<h2>验证码：" + code + "</h2><p>有效时间5分钟，如非本人使用，敬请忽略本信息</p>");
                    var _Admin = admins.First();
                    _Admin.Code = code;
                    _Admin.ExpirationTime = DateTime.Now.AddMinutes(5);
                    _db.SaveChanges();
                }
                else
                {
                    throw new Exception("发送失败,管理员中不存在该邮箱");
                }
            }
            else
            {
                throw new Exception("邮箱格式不正确");
            }
        }

        public void Update(string token, AdminUpdateInfoEntityModel entity)
        {
            var _Token = _db.Tokens.First(x => x.Token1 == token);
            var _Admin = _db.Admins.First(x => x.Id == _Token.AdminId);
            var _AdminInfo = _db.AdminInfos.First(x => x.AdminId == _Token.AdminId);
            if (entity.OldPassword != null && entity.NewPassword != null)
            {
                entity.OldPassword = MD5Helper.MD5Encrypt64(entity.OldPassword);
                entity.NewPassword = MD5Helper.MD5Encrypt64(entity.NewPassword);
                if (entity.OldPassword == _Admin.Password)
                {

                    _Admin.Password = entity.NewPassword;
                }
                else
                {
                    throw new Exception("抱歉,旧密码不正确");
                }
            }
            if (entity.E_Mail != null)
            {
                var isExist = _db.Admins.Where(x => x.EMail == entity.E_Mail).Count() > 0;
                if (!isExist)
                {
                    _Admin.EMail = entity.E_Mail;
                }
                else
                {
                    throw new Exception("该邮箱已被其他管理员使用");
                }

            }
            if (entity.Img != null)
            {
                _AdminInfo.Img = entity.Img;
            }
            if (entity.NickName != null)
            {
                _AdminInfo.NickName = entity.NickName;
            }
            _db.SaveChanges();
        }
    }
}
