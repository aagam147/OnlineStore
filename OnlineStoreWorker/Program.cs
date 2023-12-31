using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Dynamic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineStoreWorker
{
    class Program
    {
        static void Main(string[] args)
        {
             CreateHostBuilder(args).Build().Run();

           
            //var messageConsumer = new MessageConsumer();
            //messageConsumer.ConsumeRegistrationMessages();
            //messageConsumer.ConsumeProducts();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WorkerService>();
                });
    }
    //public class MessageConsumer
    //{
    //    private string HostName = "rabbitmq";
    //    public MessageConsumer()
    //    {
    //        var Hostname = Environment.GetEnvironmentVariable("HOSTNAME");
    //        HostName = string.IsNullOrEmpty(Hostname) ? "localhost" : Hostname;
    //    }

    //    public void ConsumeRegistrationMessages()
    //    {
    //        var factory = new ConnectionFactory
    //        {
    //            HostName = HostName, // Replace with your RabbitMQ server details
    //            UserName = "guest",
    //            Password = "guest"
    //        };

    //        using (var connection = factory.CreateConnection())
    //        using (var channel = connection.CreateModel())
    //        {
    //            channel.QueueDeclare("customers", exclusive: false);
    //            var consumer = new EventingBasicConsumer(channel);
    //            consumer.Received += (model, ea) =>
    //            {
    //                var body = ea.Body;
    //                var message = Encoding.UTF8.GetString(body.ToArray());

    //                Console.WriteLine($"Received interest registration: {message}");
    //                //channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    //                SendMail(message);
    //            };

    //            channel.BasicConsume(queue: "customers", autoAck: true, consumer: consumer);

    //            Console.WriteLine("interest registrations.");
    //            Console.Read();
    //        }
    //    }
    //    public void ConsumeProducts()
    //    {
    //        var factory = new ConnectionFactory
    //        {
    //            HostName = HostName,//"localhost",
    //            UserName = "guest",
    //            Password = "guest"
    //        };

    //        var connection = factory.CreateConnection();

    //        using
    //        var channel = connection.CreateModel();
    //        //declare the queue after mentioning name and a few property related to that
    //        channel.QueueDeclare("product", exclusive: false);
    //        //Set Event object which listen message from chanel which is sent by producer
    //        var consumer = new EventingBasicConsumer(channel);
    //        consumer.Received += (model, eventArgs) =>
    //        {
    //            var body = eventArgs.Body.ToArray();
    //            var message = Encoding.UTF8.GetString(body);
    //            Console.WriteLine($"Product message received: {message}");

    //            //channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
    //        };
    //        //read the message
    //        channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
    //        Console.Read();
    //    }
    //    private void SendMail(string registrationMessage)
    //    {
    //        var model=JsonConvert.DeserializeObject<Dictionary<string, string>>(registrationMessage);
    //        string senderEmail = "sgligishelp@gmail.com";
    //        string senderPassword = "mozbglhubviyvqsg";
            
    //        string recipientEmail = model["Email"];

    //        // Mail server and port
    //        string smtpServer = "smtp.gmail.com";
    //        int smtpPort = 587;
    //        var mailMessage = new MailMessage(senderEmail, recipientEmail)
    //        {
    //            Subject = "Welcome to Online Store",
    //            Body = "<h2>Welcome to Our Online Store!</h2>" +
    //                   "<p>Hello "+ model["Fullname"] +",</p>" +
    //                   "<p>Thank you for joining our Online Store. We are thrilled to have you on board!</p>" +
    //                   "<p>Explore our wide range of products and enjoy a seamless shopping experience.</p>" +
    //                   "<p>Happy shopping!</p>" +
    //                   "<p>Best Regards,<br>Your Online Store Team</p>",
    //            IsBodyHtml = true
    //        };

    //        using (var smtpClient = new SmtpClient(smtpServer))
    //        {
    //            smtpClient.Port = smtpPort;
    //            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
    //            smtpClient.EnableSsl = true;

    //            try
    //            {
    //                // Send the email
    //                smtpClient.Send(mailMessage);
    //                Console.WriteLine("Email sent successfully!");
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine($"Error sending email: {ex.Message}");
    //            }
    //        }
    //    }
    //}
}