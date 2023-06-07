using Demo_LINQ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Net;
using System.Net.Mail;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using NuGet.Protocol.Plugins;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using System.Net.Mime;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo_LINQ.Controllers
{
    [ApiController]
    public class EmailController : Controller
    {

        private readonly MailDemoDbContext _dbContext;
        public EmailController(MailDemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

 

        [HttpPost]
        [Route("sendermail")]
        public async Task<IActionResult> SendMail(string senderMail)
        {
            var emailAttachment = new EmailAttachment()
            {
                SenderMailId = senderMail,
                Subject = ".Net Developer",
                Body = "I am Manas Yeole , Working at Cloudmoyo as Associate Software . There I am working on ICI - Contract Intelligence Platform ." +
                        "Design,Code,test and manage application. " +
                        "Collaborate with engineering team and productteam to establish best products. " +
                        "Follow outlined standards of quality related to code and systems. " +
                        "Producing code using .NET Core Framework (C#)." +
                        "Upgrading, configuring and debugging existing systems. " +
                        "Prepare and maintain code for various .Net applications and resolve anydefects in systems.",
                PdfAttachment = Encoding.ASCII.GetBytes("C:\\Manas_Projects\\Demo_LINQ\\Demo_LINQ\\Files\\Manas_Resume.pdf"),
                ImageAttachment = Encoding.ASCII.GetBytes("C:\\Manas_Projects\\Demo_LINQ\\Demo_LINQ\\Files\\Photograph_Manas.jpg")
            };

            var insertedIdParameter = new SqlParameter("@InsertedId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            //_dbContext.EmailAttachments.Add(emailAttachment);
            var Output_id =  await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"EXECUTE InsertEmailAttachment_OutputId
                            {emailAttachment.SenderMailId},
                            {emailAttachment.Subject},
                            {emailAttachment.Body},
                            {emailAttachment.PdfAttachment},
                            {emailAttachment.ImageAttachment},
                            {insertedIdParameter} OUTPUT");

            await _dbContext.SaveChangesAsync();
            // Send email
            //await SendEmail(senderMail, emailAttachment.Subject, emailAttachment.Body, emailAttachment.PdfAttachment, emailAttachment.ImageAttachment);
            await SendEmail(senderMail, emailAttachment.Subject, emailAttachment.Body);
            // Save to database
            int insertedId = (int)insertedIdParameter.Value;
            return Ok(insertedId);
          
        }

        private  Task SendEmail(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("yeolem0599@outlook.com", "manas223429")
            };
            var mailMessage = new MailMessage("yeolem0599@outlook.com", email, subject, message);

            // PDF attachment
            var pdfAttachment = new Attachment("C:\\Manas_Projects\\Demo_LINQ\\Demo_LINQ\\Files\\Manas_Resume.pdf", MediaTypeNames.Application.Pdf);
            mailMessage.Attachments.Add(pdfAttachment);

            // Image attachment
            var imageAttachment = new Attachment("C:\\Manas_Projects\\Demo_LINQ\\Demo_LINQ\\Files\\Photograph_Manas.jpg", MediaTypeNames.Image.Jpeg);
            mailMessage.Attachments.Add(imageAttachment);

            return client.SendMailAsync(mailMessage);
            
            //return  client.SendMailAsync(
            //    new MailMessage(from: "yeolem0599@outlook.com",
            //                    to: email,
            //                    subject,
            //                    message
            //                    ));
        }




    }
}
