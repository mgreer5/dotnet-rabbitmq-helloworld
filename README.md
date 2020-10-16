# Dotnet Core - RabbitMQ - Helloworld

A simple app to demonstrate using RabbitMQ with .Net Core

## Prereqs
- [.Net Core SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Docker](https://www.docker.com)

## Install packages and build
```shell
dotnet restore
dotnet build
```
## Running demo

Start dependencies (RabbitMQ)
```shell
docker-compose up
```
**Note:** The RabbitMQ console can be found at http://localhost:15672 (default user/pass is guest:guest)

Start the consumer app
```shell
dotnet run --project Consumer
```

Start the producer app
```shell
dotnet run --project Producer
```