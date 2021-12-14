using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            try
            {
                MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["DefaultEmailAddress"].ToString(), message.Destination);
                SmtpClient client = new SmtpClient();
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.IsBodyHtml = true;
                await client.SendMailAsync(mail);
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
