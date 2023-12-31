namespace OnlineStoreMQ.RabbitMQService
{
    public interface IRabitMQProducer
    {
        public void SendProductMessage<T>(T message);
        public void SendRegistrationMessage<T>(T message);
    }
}
