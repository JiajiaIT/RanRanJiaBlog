using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class SendE_MailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sendTo">收件人地址</param>
        /// <param name="sendCC">抄送人地址（多人）</param>
        /// <param name="fromEmail">发件人邮箱</param>
        /// <param name="fromName">发件人名称</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容（支持html格式）</param>
        public static void SendEmail(String sendTo, string sendCC, String fromEmail, String fromName, String title, String body)
        {
            MailMessage msg = new MailMessage();
            String[] sendCCArr = null;
            //设置收件人地址
            msg.To.Add(sendTo); //收件人地址 
            //设置抄送人地址
            if (sendCC != null)
            {
                sendCCArr = sendCC.Split(';');
                if (sendCCArr.Length > 0)
                {
                    foreach (String cc in sendCCArr)
                    {
                        msg.CC.Add(cc);
                    }
                }
            }
            //设置发件人邮箱及名称
            msg.From = new MailAddress(fromEmail, fromName);

            msg.Subject = title;//邮件标题 
            msg.SubjectEncoding = Encoding.UTF8; //标题格式为UTF8 

            msg.Body = body;//邮件内容
            msg.BodyEncoding = Encoding.UTF8; //内容格式为UTF8 
            msg.IsBodyHtml = true;//设置邮件格式为html格式

            //string filePath = @"E:\导出数据.xls";//添加附件
            //msg.Attachments.Add(new Attachment(filePath));

            SmtpClient client = new SmtpClient();

            //发送邮箱信息
            client.Host = "smtp.qq.com"; //SMTP服务器地址 
            client.Port = 587; //SMTP端口，QQ邮箱填写587 

            client.EnableSsl = true; //启用SSL加密 （使用除QQ邮箱之外的最好关闭）

            //发件人邮箱账号，授权码
            //授权码获取请自行百度
            client.Credentials = new System.Net.NetworkCredential(fromEmail, "hbwfunjixkygdebh");

            try
            {
                client.Send(msg); //发送邮件
                Console.WriteLine("发送成功!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送失败");
            }
        }
        /// <summary>
        /// 邮箱格式校验
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool EmailFormat(string address)
        {
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (r.IsMatch(address))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
