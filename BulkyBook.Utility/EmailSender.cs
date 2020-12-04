using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;

namespace BulkyBook.Utility
{
    //public class EmailSender : IEmailSender
    //{
    //    // get SendGridKey from EmailOptions (properties in EmailOptions have been populated by the appsettings.json file through configuration in startup.cs file)
    //    private readonly EmailOption emailOptions;

    //    // configure dependency injection to get the values
    //    public EmailSender(IOptions<EmailOption> options)
    //    {
    //        emailOptions = options.Value; 
    //    }
    //    // called from Register.cshtml.cs
    //    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    //    {
    //        return Execute(emailOptions.SendGridKey, subject, htmlMessage, email);
    //    }
    //    private Task Execute(string sendGridKEy, string subject, string message, string email)
    //    {
    //        var client = new SendGridClient(sendGridKEy);
    //        var from = new EmailAddress("admin@bulky.com", "Bulky Books");
    //        var to = new EmailAddress(email, "End User");
    //        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
    //        return client.SendEmailAsync(msg);
    //    }
    //}

    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions emailOptions;

        public EmailSender(IOptions<EmailOptions> options)
        {
            emailOptions = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(emailOptions.SendGridKey, subject, htmlMessage, email);
        }
        private Task Execute(string sendGridKEy, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKEy);
            var from = new EmailAddress("admin@bulky.com", "Bulky Books");
            var to = new EmailAddress(email, "End User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            return client.SendEmailAsync(msg);
        }
    }
}

