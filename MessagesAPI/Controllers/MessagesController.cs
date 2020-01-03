using APIMensagens.Config;
using MessagesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MessagesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private static Counter _counter = new Counter();

        [HttpGet]
        public object Get()
        {
            return new
            {
                TotalMessagesSent = _counter.CurrentValue
            };
        }

        [HttpPost]
        public object Post([FromServices]RabbitMQConfig configurations, Content content)
        {
            lock (_counter)
            {
                _counter.Increment();

                var factory = new ConnectionFactory()
                {
                    HostName = configurations.HostName,
                    Port = configurations.Port,
                    UserName = configurations.UserName,
                    Password = configurations.Password
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Messages.Queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message =
                        $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - " +
                        $"Message's Content: {content.Message}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Messages.Queue",
                                         basicProperties: null,
                                         body: body);
                }

                return new
                {
                    Result = "Message published successfully."
                };
            }
        }
    }
}