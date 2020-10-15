# Dotnet Core - RabbitMQ - Helloworld

A simple app to demonstrate using RabbitMQ with .Net Core

## Prereqs
- [.Net Core SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Docker](https://www.docker.com)

## Running demo
```shell
docker-compose up --build
```

Navigate to the Rabbit MQ Console and send a test message to the helloworld queue:
http://localhost:15672/#/queues/%2F/helloworld

The message should echo to stdout in the docker container