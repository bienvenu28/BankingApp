using System;
using System.Text;
using RabbitMQ.Client;

namespace BankingApp.Logic.MOM
{
    public class Producer
    {
        private static string Queue_Name = "Admin";
        private static string HOST_NAME = "localhost";
        public static bool SendMessage(string message)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = HOST_NAME };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: Queue_Name,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: Queue_Name,
                        basicProperties: null,
                        body: body);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
