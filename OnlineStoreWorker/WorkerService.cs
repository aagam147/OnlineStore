using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace OnlineStoreWorker
{
    public class WorkerService : BackgroundService
    {
        private string HostName = "rabbitmq"; // RabbitMQ server hostname
        private const string QueueName = "customers";

        private readonly ILogger<WorkerService> _logger;

        public WorkerService(ILogger<WorkerService> logger)// HttpClient httpClient)
        {
            _logger = logger;
            //httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var Hostname = Environment.GetEnvironmentVariable("HOSTNAME");
            HostName = string.IsNullOrEmpty(Hostname) ? "localhost" : Hostname;
            var factory = new ConnectionFactory
            {
                HostName = HostName,//"localhost",
                UserName = "guest",
                Password = "guest"
            };


            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("customers", exclusive: false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());

                    Console.WriteLine($"Received interest registration: {message}");
                    //channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    SendMail(message);
                };

                channel.BasicConsume(queue: "customers", autoAck: true, consumer: consumer);

                Console.WriteLine("interest registrations.");
                Console.Read();
            }



            var connection2 = factory.CreateConnection();

            using
            var channel2 = connection2.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel2.QueueDeclare("product", exclusive: false);
            //Set Event object which listen message from chanel which is sent by producer
            var consumer2 = new EventingBasicConsumer(channel2);
            consumer2.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Product message received: {message}");

                //channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            };
            //read the message
            channel2.BasicConsume(queue: "product", autoAck: true, consumer: consumer2);
        }

        private void SendMail(string registrationMessage)
        {
            var model = JsonConvert.DeserializeObject<Dictionary<string, string>>(registrationMessage);
            string senderEmail = "sgligishelp@gmail.com";
            string senderPassword = "mozbglhubviyvqsg";

            string recipientEmail = model["Email"];

            // Mail server and port
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            var mailMessage = new MailMessage(senderEmail, recipientEmail)
            {
                Subject = "Welcome to Online Store",
                Body = "<h2>Welcome to Our Online Store!</h2>" +
                       "<p>Hello " + model["Fullname"] + ",</p>" +
                       "<p>Thank you for joining our Online Store. We are thrilled to have you on board!</p>" +
                       "<p>Explore our wide range of products and enjoy a seamless shopping experience.</p>" +
                       "<p>Happy shopping!</p>" +
                       "<p>Best Regards,<br>Your Online Store Team</p>",
                IsBodyHtml = true
            };

            using (var smtpClient = new SmtpClient(smtpServer))
            {
                smtpClient.Port = smtpPort;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                try
                {
                    // Send the email
                    smtpClient.Send(mailMessage);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
        }

        //private void ProcessRegistrationRequest(CustomerRegistrationRequest request)
        //{
        //    // Save the registration to the database
        //    var customer = new Customer
        //    {
        //        Name = request.Name,
        //        Email = request.Email
        //        // Add other properties as needed
        //    };

        //    _dbContext.Customers.Add(customer);
        //    _dbContext.SaveChanges();

        //    // Add logic to send welcome email to the customer
        //    _logger.LogInformation($"Registered customer: {customer.Name}, {customer.Email}");
        //}
    }
}
