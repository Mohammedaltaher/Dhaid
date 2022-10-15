# Introduction  
Building  Microservices on .Net 6 for backend task for Dhaid

Used Asp.Net Web API, Docker, Ocelot API Gateway, service discovery(Consul), SqlServer, Entity Framework Core, CQRS and Mediator. Applying  Rate Limiting of request 

**Service Microservice**
- ASP.NET Web API application
- REST API principles, CRUD operations
- SqlServer database connection and containerization
- CQRS 
- Swagger Open API implementation

**Discount Microservice**
- ASP.NET Web API application
- REST API principles, CRUD operations
- SqlServer database connection and containerization
- CQRS 
- Swagger Open API implementation

**Order Microservice**
- ASP.NET Web API application
- REST API principles, CRUD operations
- SqlServer database connection and containerization
- CQRS 
- Swagger Open API implementation

**API Gateway Ocelot Microservice**
- Implement API Gateways with Ocelot
- Sample microservices/containers to reroute through the API Gateways
- apply Rate limit 
- using Consul for reading the service ports 

**Docker Compose establishment with all microservices on docker**
- Containerization of microservices
- Containerization of databases
- Override Environment variables


# Getting Started You can **launch microservices** as below urls:

* **Service API -> http://host.docker.internal:8001/swagger/index.html**
* **Discount API -> http://host.docker.internal:8002/swagger/index.html**
* **Order API -> http://host.docker.internal:8000/swagger/index.html**
* **API Gateway -> http://host.docker.internal:8010**
* **Consul -> http://host.docker.internal:8500**
