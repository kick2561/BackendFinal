using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace StoreBuy.Utilities
{
    public class Utility
    {
        public static bool SendEmail(string Email,string Body,string Subject)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Resources.MailHost);
                mail.From = new MailAddress(Resources.FromMailAddress);
                mail.To.Add(Email);
                mail.Subject = Subject;
                mail.Body = Body;
                SmtpServer.Port = Int32.Parse(Resources.SMTPPort);
                SmtpServer.Credentials = new System.Net.NetworkCredential(Resources.FromMailAddress, Resources.MailPassword);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static long GetRandomNumber()
        {
            Random random = new Random();
            var OTP = random.Next(100000, 999999);
            return OTP;
        }

        

       
    }
}