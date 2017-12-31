using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gastro
{
    public class Logging
    {
        bool write2log;
        string logfile;
        public Logging(bool write2log, string logfile)
        {

                this.write2log = write2log;
                this.logfile = logfile != null ? logfile : @"c:\Temp\DBTest.txt";
            if (write2log)
            {
                if (!Directory.Exists(Path.GetDirectoryName(logfile)))
                Directory.CreateDirectory(Path.GetDirectoryName(logfile));
                Console.WriteLine("Loglocation is:\n " + logfile);
            }
                
            else
                Console.WriteLine("Logging into file disabled");
        }

        public void WriteLine(string s)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine(time + "\t[GastroDLL]\t" + s);
            if (write2log)
            {
                FileStream fs = new FileStream(logfile, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(time + "\t[GastroDLL]\t" + s);
                sw.Flush();
                fs.Close();
            }   
        }

        public void UpdateProgress(int total, int done, bool newline = false)
        {
            string n = newline == true ? "\n" : "";
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            double p = Math.Round((double)100 / total * done);
            string s = "[";
            double pd = Math.Floor(p / 5);
            for(int i = 0; i < 20; i++)
                s += pd > i ? (char)88 : (char)32;
            s += "]";

            Console.Write("\r" + time + "\t[GastroDLL]\t" + s + " " + p.ToString() + "%" + n);
        }

        public void WriteException(Exception e, string process, string extra = "")
        {
            string msg = e.InnerException == null ? e.ToString() : e.Message + "\nInnerExeption: " + e.InnerException.ToString();
            WriteLine(string.Format("ERROR: Exception in {0}\nException: {1}", process, msg));
            //Mail(msg, "GastroDLL Exception " + process, "mohiuddin.kawsar@gmail.com", extra);

        }

        public static void Mail(String msg, String subject, string recipient, string extra = "")
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                MailAddress fromAdress = new MailAddress("mohiuddin.kawsar@gmail.com", "GastroDLL import tool");
                smtpClient.Host = "";
                smtpClient.Port = 25;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Timeout = 30000;
                message.From = fromAdress;
                message.IsBodyHtml = true;
                string[] recipients = recipient.Split(';');
                    message.Subject = "GastroDLL Exception: " + DateTime.Now;
                    foreach (String r in recipients)
                        message.To.Add(r);

                    message.Body = "GastroDLL Exception: " + DateTime.Now + "<br><br>" + msg;
                if(extra != null)message.Body += "<br>Extra Info:" + extra;
                message.Body += "<br><br><br><br><br><br>Auto generated Email";
                smtpClient.Send(message);
            }
            catch (Exception)
            {
                if (msg == null) msg = "null";
            }
        }

    }
}
