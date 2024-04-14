using MassTransit;
using Message;
using Notification.Models;
namespace Notification.Services
{
    public class NotiConsumer : IConsumer<MessageTrans>
    {

        private readonly IEmailService _emailService;

        public NotiConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<MessageTrans> context)
        {
            try
            {
                var message = context.Message;
                MailRequest mailrequest = new MailRequest();
                mailrequest.ToEmail = message.UserEmail;
                string opt = message.Topic;
                switch (opt)
                {
                    case "Register":
                        mailrequest.Subject = "Welcome to Duckie Personal Financial Management";
                        mailrequest.Body = GetHtmlcontent();
                        break;
                    case "Exceed Budget":
                        mailrequest.Subject = "Exceed Budget";
                        mailrequest.Body = "You have exceeded your budget! PLease balance your expense";
                        break;
                }
                await _emailService.SendEmailAsync(mailrequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GetHtmlcontent()
        {
            string Response = "<div style=\"width:100%;background-color:#ffe993;text-align:center;margin:10px\">";
            Response += "<h1>Welcome to Duckie Personal Financial Management</h1>";
            Response += "<img src=\"https://yt3.googleusercontent.com/v5hyLB4am6E0GZ3y-JXVCxT9g8157eSeNggTZKkWRSfq_B12sCCiZmRhZ4JmRop-nMA18D2IPw=s176-c-k-c0x00ffffff-no-rj\" />";
            Response += "<h2>Thanks for subscribed us</h2>";
            Response += "<div><h1> Contact us : duckiiduckie@gmail.com</h1></div>";
            Response += "</div>";
            return Response;
        }
    }
}
