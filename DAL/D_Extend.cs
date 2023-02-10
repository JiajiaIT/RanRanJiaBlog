using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;
using IDAL;
using EntityModel.SqlServer;

namespace DAL
{
    public class D_Extend : I_Extend
    {
        RanRanJiaBlogContext _db;
        public D_Extend()
        {
            _db = new RanRanJiaBlogContext();
        }
        public bool Authorize(string token)
        {
            try
            {
                if (token != null)
                {
                    var isExist = _db.Tokens.Where(x => x.Token1 == token).Count() > 0;
                    var isExpiration = _db.Tokens.Where(x => x.Token1 == token).First().ExpirationTime > DateTime.Now;
                    var result = isExist && isExpiration;
                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsSuperAdmin(string token)
        {
            try
            {
                if (token != null)
                {
                    var adminId = _db.Tokens.First(x => x.Token1 == token).AdminId;
                    var admin = _db.Admins.First(x => x.Id == adminId).Account;
                    if (admin == "jiajia" || admin == "JiaJia" || admin == "Jiajia")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<LogEntityModel> Read(int page, int pagesize)
        {
            var number = (page - 1) * pagesize;
            var data = _db.Logs.OrderByDescending(x => x.Time).Skip(number).Take(pagesize).ToList();
            var result = (from log in data
                          select new LogEntityModel
                          {
                              ID = log.Id,
                              IP = log.Ip,
                              Time = log.Time,
                              What = log.What,
                              Who = log.Who
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

        public void WriteLog(LogEntityModel entity)
        {
            var adminId = _db.Tokens.First(x => x.Token1 == entity.Who).AdminId;
            var who = _db.Admins.First(x => x.Id == adminId).Account;
            var _Log = new Log
            {
                Ip = entity.IP,
                Time = DateTime.Now,
                What = entity.What,
                Who = who
            };
            _db.Logs.Add(_Log);
            _db.SaveChanges();
        }
    }
}
