//using System.Net;
//using System.Net.Mail;
//using Castle.Core.Smtp;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;

//namespace Celestial.Agency.Service.Service.EmailSender;

//public class EmailSender : IEmailSender
//{
//    private readonly IConfiguration _config;
//    private readonly ILogger<EmailSender> _logger;

//    public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
//    {
//        _config = config;
//        _logger = logger;
//    }
//    a
//    public void Send(string from, string to, string subject, string messageText)
//    {
//        var message = new MailMessage(from, to, subject, messageText);
//        Send(message);
//    }

//    public void Send(MailMessage message)
//    {
//        try
//        {
//            using var smtpClient = new SmtpClient(_config["EmailSettings:SmtpServer"])
//            {
//                Port = int.Parse(_config["EmailSettings:Port"]!),
//                Credentials = new NetworkCredential(
//                    _config["EmailSettings:SenderEmail"],
//                    _config["EmailSettings:SenderPassword"]
//                ),
//                EnableSsl = true
//            };

//            smtpClient.Send(message);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError($"Error sending email: {ex.Message}");
//        }
//    }

//    public void Send(IEnumerable<MailMessage> messages)
//    {
//        foreach (var message in messages) Send(message);
//    }
//}

