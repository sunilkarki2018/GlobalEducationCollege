using GlobalCollege.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GlobalCollege.Frontend.Utility
{
    public static class MailHelper
    {
        public static async Task SendMail(AppointmentViewModel model)
        {
            string toEmailAdddress = ConfigurationManager.AppSettings["To"].ToString();
            string toName = ConfigurationManager.AppSettings["Name"].ToString();

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(toEmailAdddress, toName));
            msg.From = new MailAddress("admin@globalcollege.edu.np", "Online Appointment");
            msg.CC.Add(new MailAddress(model.Email, model.FullName));
            msg.Subject = "Online appointment request.";
            msg.Body = string.Format(@"<html>
                      <body>
                      <p>Dear Sir/Madam,</p>
                      <p>I {0} would like to request you to book appointment on {1} for {2} along with {3} visitors. Please note my mobile number {4} and email address {5} for further enquiry. </p>
                      <p>Please find my below questions.</P>   
                      <p>{6}</p>
                      <p>Sincerely,<br>-{0}</br></p>
                      </body>
                      </html>", model.FullName, model.Date.ToShortDateString(), model.PurposeofVisit, model.NumberofVisitors, model.MobileNumber, model.Email, model.ShortDescription);
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("admin@globalcollege.edu.np", "S@tellite4444");
            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            try
            {
                await client.SendMailAsync(msg);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
