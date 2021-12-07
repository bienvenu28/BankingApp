using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BankingApp.Logic.MOM
{
    class Consumer
    {
        private static string Queue_Name = "Admin";
        private static string HOST_NAME = "localhost";
        public static string NO_MESSAGE = "No message";
        public static string ReceiveMessage()
        {
            string message = NO_MESSAGE;
            var factory = new ConnectionFactory() { HostName = HOST_NAME};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: Queue_Name,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);
                };
                channel.BasicConsume(queue: Queue_Name,
                    autoAck: false,
                    consumer: consumer);
            }
            return message;
        }
    }
}
