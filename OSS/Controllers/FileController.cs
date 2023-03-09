using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using OSS.Models;
using System.Drawing;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace OSS.Controllers
{
    /// <summary>
    /// OSS服务器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="imageInfo">图片信息</param>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        public Result<string> SaveImage([FromForm] ImageInfo imageInfo)
        {
            try
            {
                //图片存放地址
                var path = Directory.GetCurrentDirectory() + "\\Content\\" + imageInfo.Path;
                //判断路径存不存在
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path); //不存在就创建文件夹
                }

                //起名字
                var date = DateTime.Now;
                string datetime = date.Year.ToString() + date.Month.ToString() + date.Day + date.Hour + date.Minute +
                                  date.Second + date.Millisecond;
                string name = datetime + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10) + ".jpg";
                var file = path + "\\" + name;
                string code = imageInfo.Base64.Split(',')[1];
                byte[] buffer = Convert.FromBase64String(code);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    Bitmap bmp = new Bitmap(ms);
                    bmp.Save(file);
                }

                //系统留痕
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }

                var time = string.Format("{0:U}", date.AddHours(8));
                var logpath = Directory.GetCurrentDirectory() + "\\Content\\log.txt";
                if (!System.IO.File.Exists(Path.GetFullPath(logpath)))
                {
                    System.IO.File.Create(Path.GetFileName(logpath));
                }

                StreamWriter streamWriter = new StreamWriter(logpath, true);
                streamWriter.WriteLine(ip + "\t\t" + time + "\t\t" + "http://OSS.JiaJia.icu/Content/" + imageInfo.Path +
                                       "/" + name);
                streamWriter.Close();
                var result = new Result<string>
                {
                    Data = name
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
        /// 上传文件
        /// </summary>
        /// <param name="Path">目录</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost, Route("[action]/{Path}")]
        public Result<string> SaveFile(string Path, IFormFile file)
        {
            try
            {
                //图片存放地址
                var path = Directory.GetCurrentDirectory() + "\\Content\\" + Path;
                //判断路径存不存在
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path); //不存在就创建文件夹
                }
                //起名字
                var suffix = "";
                var split = file.FileName.TakeLast(6);
                foreach (var item in split)
                {
                    suffix = suffix + item;
                }
                suffix = "." + suffix.Split('.')[1];
                var date = DateTime.Now;
                string datetime = date.Year.ToString() + date.Month.ToString() + date.Day + date.Hour + date.Minute +
                                  date.Second + date.Millisecond;
                string name = datetime + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10) + suffix;
                var filename = path + "\\" + name;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                //系统留痕
                var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Connection.RemoteIpAddress.ToString();
                }

                var time = string.Format("{0:U}", date.AddHours(8));
                var logpath = Directory.GetCurrentDirectory() + "\\Content\\log.txt";
                if (!System.IO.File.Exists(System.IO.Path.GetFullPath(logpath)))
                {
                    System.IO.File.Create(System.IO.Path.GetFileName(logpath));
                }

                StreamWriter streamWriter = new StreamWriter(logpath, true);
                streamWriter.WriteLine(ip + "\t\t" + time + "\t\t" + "http://OSS.JiaJia.icu/Content/" + Path +
                                       "/" + name);
                streamWriter.Close();
                var result = new Result<string>
                {
                    Data = name
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
    }
}