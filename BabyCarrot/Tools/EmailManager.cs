using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Configuration;


namespace BabyCarrot.Tools
{
    public class EmailManager
    {
        private MailMessage _MailMassage;
        private SmtpClient _SmtpClient;

        public EmailManager(string host, int port, string id, string pwd)
        {
            _SmtpClient = new SmtpClient(host, port);
            _SmtpClient.Credentials = new NetworkCredential(id, pwd);

            _MailMassage = new MailMessage();
            _MailMassage.IsBodyHtml = true;
            _MailMassage.Priority = MailPriority.Normal;
        }

        public string From
        {
            get { return _MailMassage.From == null ? string.Empty : _MailMassage.From.Address; }
            set { _MailMassage.From = new MailAddress(value); }
        }

        public MailAddressCollection To
        {
            get { return _MailMassage.To; }
        }

        public string Subject
        {
            get { return _MailMassage.Subject; }
            set { _MailMassage.Subject = value; }
        }

        public string Body
        {
            get { return _MailMassage.Body; }
            set { _MailMassage.Body = value; }
        }

        public void Send()
        {
            _SmtpClient.Send(_MailMassage);
        }

        public static void Send(string from, string to, string subject, string contents, string cc, string bcc)
        {
            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentException("sender is empty");
            }
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentException("to is empty");
            }


            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"];

            int smtpPort = 0;
            if (ConfigurationManager.AppSettings["SMTPPort"] == null ||
                int.TryParse(ConfigurationManager.AppSettings["SMTPPort"], out smtpPort) == false)
            {
                smtpPort = 25;
            }

            string smtpId = ConfigurationManager.AppSettings["SMTPID"];
            string smtpPwd = ConfigurationManager.AppSettings["SMTPPassword"];

            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(from);
            mailMsg.To.Add(new MailAddress(to));

            if (!string.IsNullOrEmpty(cc))
                mailMsg.CC.Add(cc);

            if (!string.IsNullOrEmpty(bcc))
                mailMsg.Bcc.Add(bcc);

            mailMsg.Subject = subject;
            mailMsg.IsBodyHtml = true;
            mailMsg.Body = contents;
            mailMsg.Priority = MailPriority.Normal;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential();
            smtpClient.Host = smtpHost;
            smtpClient.Port = smtpPort;
            smtpClient.Send(mailMsg);
        }

        public static void Send(string from, string to, string subject, string contents)
        {
            Send(from, to, subject, contents, null, null);
        }

        public static void Send(string to, string subject, string contents)
        {
            string sender = ConfigurationManager.AppSettings["SMTPSender"];
            Send(sender, to, subject, contents);
        }
    }
}
