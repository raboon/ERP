using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gastro
{
    public static class EMail
    {
        public enum Status
        {
            OK = 0,
            FAILED = 1,
            EXCEPTION = 2,
        }

        static string default_recipient = "mohiuddin.kawsar@gmail.com";
        public static string Mail(String msgtype, String msg, String subject, string recipient = null)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                MailAddress fromAdress = new MailAddress(default_recipient, "ERP UI");
                smtpClient.Host = "";
                smtpClient.Port = 25;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Timeout = 30000;
                message.From = fromAdress;
                message.IsBodyHtml = true;
                List<string> recipients = new List<string>();
                if(recipient != null)
                    foreach (string r in recipient.Split(';').ToList())
                        recipients.Add(r);

                if (recipients.Count == 0)
                    recipients.Add(default_recipient);
                if (msgtype.ToLower().Equals("report"))
                {
                    message.Subject = "Stadium Report for date: " + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                    foreach (string r in recipients)
                        message.To.Add(r);

                    message.Body = "Stadium Report for date: " + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "<br><br>";
                    message.Body += msg;
                }
                else if (msgtype.ToLower().Equals("exception"))
                {
                    message.Subject = " Exception: " + DateTime.Now;
                    foreach (String r in recipients)
                        message.To.Add(r);

                    message.Body = " Exception: " + DateTime.Now + "<br><br>" + msg;
                }
                else
                {
                    message.Subject = "Others: " + DateTime.Now;
                    foreach (String r in recipients)
                        message.To.Add(r);

                    message.Body = " Others: " + DateTime.Now + "<br><br>" + msg;
                }
                message.Body += "<br><br><br><br><br><br>Auto generated Email";
                smtpClient.Send(message);
                return Status.OK.ToString();
            }
            catch (Exception e)
            {
                if (msg == null) msg = "null";
                return Status.EXCEPTION.ToString();
            }
        }
    }
}
