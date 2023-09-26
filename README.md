# Ocelot.SignalR.Demo
Example of using the Ocelot Api Gateway and SignalR hubs behind a load balancer with a MassTransit RabbitMQ backplane.

![Ocelot.SignalR.Demo](/res/images/architecture.jpg?raw=true "Ocelot.SignalR.Demo")

**🛠️ Implementation**

- Clients: ASP.NET Core MVC applications + Apache JMeter
- API Gateway: Ocelot https://github.com/ThreeMammals/Ocelot
- Load Balancer: Ocelot Load Balancer with type "CookieStickySessions"
- SignalR Hubs: A couple of ASP.NET Core WebAPI applications
- SignalR Hubs Backplane: MassTransit + RabbitMQ

**🚀 Run using docker:**
```
docker-compose build
docker-compose up -d
```

RabbitMQ: 

http://localhost:15672/ (guest/guest)

Just for testing, try sending a POST request through the API Gateway using Postman: 

http://localhost:9500/hubs/chat/negotiate

Open both web clients and try to send some messages:

- first browser: http://localhost:5000/
- second browser: http://localhost:5001/

Each web client should create a cookie LbCookie. Load Balancer (on the Ocelot Api Gateway side) will route each request using LbCookie to the appropriate SignalR Hub. Do not forget to check the API Gateway logs:
- message: 200 (OK) status code, request uri: ws://hub1/chat/negotiate
- message: 200 (OK) status code, request uri: ws://hub2/chat/negotiate

MassTransit Backplane will take care of combining both Hubs.

**🚀 Testing using Apache JMeter**

Download and run the JMeter Test Plan 
https://github.com/drlivsi/Ocelot.SignalR.Demo/blob/main/res/JMeter/OcelotSignalR.jmx

![Ocelot.SignalR.Demo](/res/images/JMeter.jpg?raw=true "Ocelot.SignalR.Demo")

Check the API Gateway logs again.
