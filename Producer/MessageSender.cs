using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;



namespace demo_producer
{
  public class MessageSender : IMessageSender
  {
    private readonly IConfiguration _config;
    private readonly ILogger<MessageSender> _logger;
    private IConnection _connection;
    private IModel _channel;

    public MessageSender(ILogger<MessageSender> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      InitializeQueue();
    }
    private void InitializeQueue()
    {
      var factory = new ConnectionFactory { HostName = _config["RabbitMQHost"] };
      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();
    }

    public void Send(string message)
    {
      DeclareQueue();
      var body = Encoding.UTF8.GetBytes(message);
      _channel.BasicPublish(exchange: "", routingKey: "helloworld", basicProperties: null, body: body);
      _logger.LogInformation($"Sent message: {message}");
    }

    private void DeclareQueue()
    {
      _channel.QueueDeclare(
        queue: "helloworld",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);
    }
  }
}