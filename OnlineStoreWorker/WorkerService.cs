//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using RabbitMQ.Client.Events;
//using RabbitMQ.Client;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OnlineStoreWorker
//{
//    public class WorkerService : BackgroundService
//    {
//        private const string HostName = "rabbitmq"; // RabbitMQ server hostname
//        private const string QueueName = "customers";

//        private readonly ILogger<WorkerService> _logger;

//        public WorkerService(ILogger<WorkerService> logger)// HttpClient httpClient)
//        {
//            _logger = logger;
//            //httpClient = httpClient;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            var Hostname = Environment.GetEnvironmentVariable("HOSTNAME");
//            Hostname = string.IsNullOrEmpty(Hostname) ? "localhost" : Hostname
//            var factory = new ConnectionFactory
//            {
//                HostName = Hostname,//"localhost",
//                UserName = "guest",
//                Password = "guest"
//            };

//            using (var connection = factory.CreateConnection())
//            using (var channel = connection.CreateModel())
//            {
//                channel.QueueDeclare("customers", exclusive: false);
//                var consumer = new EventingBasicConsumer(channel);
//                consumer.Received += (model, ea) =>
//                {
//                    var body = ea.Body;
//                    var message = Encoding.UTF8.GetString(body.ToArray());

//                    Console.WriteLine($"Received interest registration: {message}");

//                };

//                channel.BasicConsume(queue: "customers", autoAck: true, consumer: consumer);

//                Console.WriteLine("Waiting for interest registrations. Press [Enter] to exit.");
//                //Console.ReadLine();
//            }
//        }

//        //private void ProcessRegistrationRequest(CustomerRegistrationRequest request)
//        //{
//        //    // Save the registration to the database
//        //    var customer = new Customer
//        //    {
//        //        Name = request.Name,
//        //        Email = request.Email
//        //        // Add other properties as needed
//        //    };

//        //    _dbContext.Customers.Add(customer);
//        //    _dbContext.SaveChanges();

//        //    // Add logic to send welcome email to the customer
//        //    _logger.LogInformation($"Registered customer: {customer.Name}, {customer.Email}");
//        //}
//    }
//}
