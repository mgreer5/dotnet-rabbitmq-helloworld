using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

public class QueueConsumerService : BackgroundService
{
  private readonly ILogger<QueueConsumerService> _logger;
  private readonly IConfiguration _config;
  private IConnection _connection;
  private IModel _channel;

  public QueueConsumerService(ILogger<QueueConsumerService> logger, IConfiguration config)
  {
    _logger = logger;
    _config = config;
    InitializeQueue();
  }

  private void InitializeQueue()
  {
    var factory = new ConnectionFactory { HostName = _config["RabbitMQHost"] };
    _connection = factory.CreateConnection();
    _channel = _connection.CreateModel();
  }

  protected override Task ExecuteAsync(CancellationToken cancelToken)
  {
    cancelToken.ThrowIfCancellationRequested();

    DeclareQueue();
    var consumer = new EventingBasicConsumer(_channel);
    consumer.Received += (ch, args) => {
      var body = args.Body.ToArray();
      var content = Encoding.UTF8.GetString(body);
      HandleMessage(content);
      _channel.BasicAck(args.DeliveryTag, false);
    };

    _channel.BasicConsume("helloworld", false, consumer);
    return Task.CompletedTask;
  }

  private void HandleMessage(string content)
  {
    _logger.LogInformation($"Received: {content}");
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

  public override void Dispose()
  {
    _channel.Dispose();
    _connection.Dispose();
    base.Dispose();
  }
}