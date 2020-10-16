using System;
using Microsoft.Extensions.Logging;

namespace demo_producer
{
  public class App
  {
    private readonly ILogger<App> _logger;
    private IMessageSender _sender;
    public App(ILogger<App> logger, IMessageSender sender)
    {
      _logger = logger;
      _sender = sender;
    }

    public void Run()
    {
      _logger.LogInformation($"App is running..");
      for (int i=1; i<10000; i++){
        _sender.Send($"Helloworld {i}");
      }
      
    }
  }
}