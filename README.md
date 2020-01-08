# RabbitMQ + ASP.NET Core (Example)

This project was created to demonstrate how to use RabbitMQ as message broker. We have two small projects for it, the first one publish messages in the queue, and the second one
is the consumer of the queue.

## Prerequisites
* [Docker](https://www.docker.com/products/docker-desktop)
* [ASP.NET Core](https://dotnet.microsoft.com/download)

## How to run the project
1. In the root folder of the project, open you terminal and run the command 
'docker run -d --hostname rabbit-local --name testes-rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=admin2020 rabbitmq:3-management'
2. Navigate to the folder '/MessagesAPI/' and run the command 'dotnet run'
3. Navigate to the folder '/MessagesProcessor/' and run the command 'dotnet run'
4. You can see RabbitMQ running on this address: http://localhost:15672/ 
5. Publish messages in the queue by sending a post to this address : https://localhost:5001/api/messages
6. In the terminal opened in step 3 you can see the message being consumed.
 
## Built With
* [ASP.NET Core](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-3.1) - Used to build the backend API.
* [RabbitMQ] (https://www.rabbitmq.com/)
 
             
