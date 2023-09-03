//using apistudy.interfaces;
//using apistudy.Seting;

//using MailKit.Net.Smtp;
//using MailKit.Security;

//using Microsoft.Extensions.Options;

//using MimeKit;

//namespace apistudy.Servesess
//{
//    public class MailingService : IMailingService
//    {
//        private readonly MailSettings _mailSettings;

//        public MailingService(IOptions<MailSettings> mailSettings)
//        {
//            _mailSettings = mailSettings.Value;
//        }

//        public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
//        {
//            var email = new MimeMessage
//            {
//                Sender = MailboxAddress.Parse(_mailSettings.Email),
//                Subject = subject
//            };

//            email.To.Add(MimeKit.MailboxAddress.Parse(mailTo));

//            var builder = new BodyBuilder();

//            if (attachments != null)
//            {
//                byte[] fileBytes;
//                foreach (var file in attachments)
//                {
//                    if (file.Length > 0)
//                    {
//                        using var ms = new MemoryStream();
//                        file.CopyTo(ms);
//                        fileBytes = ms.ToArray();

//                        builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(file.ContentType));
//                    }
//                }
//            }

//            builder.HtmlBody = body;
//            email.Body = builder.ToMessageBody();
//            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

//            using var smtp = new SmtpClient();
//            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
//            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
//            await smtp.SendAsync(email);

//            smtp.Disconnect(true);
//        }
//    }
//}
//using System;
//using System.IO;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using apistudy.interfaces;
//using apistudy.Seting;
//using MailKit.Net.Smtp;
//using MailKit.Security;
//using Microsoft.Extensions.Options;
//using MimeKit;

//namespace apistudy.Servesess
//{
//    public class MailingService : IMailingService
//    {
//        private readonly MailSettings _mailSettings;

//        public MailingService(IOptions<MailSettings> mailSettings)
//        {
//            _mailSettings = mailSettings.Value;
//        }

//        public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
//        {
//            try
//            {
//                var email = new MimeMessage
//                {
//                    Sender = MailboxAddress.Parse(_mailSettings.Email),
//                    Subject = subject
//                };

//                email.To.Add(MimeKit.MailboxAddress.Parse(mailTo));

//                var builder = new BodyBuilder();

//                if (attachments != null)
//                {
//                    byte[] fileBytes;
//                    foreach (var file in attachments)
//                    {
//                        if (file.Length > 0)
//                        {
//                            using var ms = new MemoryStream();
//                            file.CopyTo(ms);
//                            fileBytes = ms.ToArray();

//                            builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(file.ContentType));
//                        }
//                    }
//                }

//                builder.HtmlBody = body;
//                email.Body = builder.ToMessageBody();
//                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

//                using var smtp = new SmtpClient();
//                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
//                smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
//                await smtp.SendAsync(email);

//                smtp.Disconnect(true);
//            }
//            catch (Exception ex)
//            {
//                // Handle the exception here, e.g., log it or provide feedback to the user.
//                Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
//                throw; // You can rethrow the exception if needed for further handling.
//            }
//        }
//    }
//}
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using apistudy.interfaces;
using apistudy.Seting;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace apistudy.Servesess
{
    public class MailingService : IMailingService
    {
        private readonly MailSettings _mailSettings;
        private const int MaxRetries = 3; // Maximum number of retry attempts
        private const int RetryDelayMilliseconds = 1000; // Delay between retries in milliseconds

        public MailingService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
        {
            for (int retryCount = 0; retryCount < MaxRetries; retryCount++)
            {
                try
                {
                    var email = new MimeMessage
                    {
                        Sender = MailboxAddress.Parse(_mailSettings.Email),
                        Subject = subject
                    };

                    email.To.Add(MimeKit.MailboxAddress.Parse(mailTo));

                    var builder = new BodyBuilder();

                    if (attachments != null)
                    {
                        byte[] fileBytes;
                        foreach (var file in attachments)
                        {
                            if (file.Length > 0)
                            {
                                using var ms = new MemoryStream();
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();

                                builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(file.ContentType));
                            }
                        }
                    }

                    builder.HtmlBody = body;
                    email.Body = builder.ToMessageBody();
                    email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

                    using var smtp = new SmtpClient();
                    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                    smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
                    await smtp.SendAsync(email);

                    smtp.Disconnect(true);

                    // Email sent successfully, exit the loop
                    break;
                }
                catch (Exception ex)
                {
                    // Handle the exception here, e.g., log it or provide feedback to the user.
                    Console.WriteLine($"Attempt {retryCount + 1} failed. Error: {ex.Message}");

                    if (retryCount < MaxRetries - 1)
                    {
                        // Delay before the next retry
                        await Task.Delay(RetryDelayMilliseconds);
                    }
                    else
                    {
                        // Maximum number of retries reached, rethrow the exception
                        throw;
                    }
                }
            }
        }
    }
}
