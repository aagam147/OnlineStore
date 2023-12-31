using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace OnlineStoreMQ.RabbitMQService
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName="guest",
                Password="guest"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare("product", exclusive: false);
            
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }

        public void SendRegistrationMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "customers",exclusive: false);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: "customers",  body: body);
                Console.WriteLine($"Sent interest registration: {message}");
            }
        }
    }
}
