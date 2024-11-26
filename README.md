# eShop Microservices

This repository contains the **eShop Microservices** project, built with **.NET 8**, leveraging **Domain-Driven Design (DDD)** principles, **CQRS**, and a **Vertical/Clean Architecture** approach. The project demonstrates a modern implementation of a microservices architecture, integrating key features for scalability, maintainability, and modular development.

---

## Features

- **Microservices Architecture**:
  - Each service is independently deployable and scalable.
  - Services include domain-specific logic and functionality.

- **Domain-Driven Design (DDD)**:
  - Encapsulation of business logic within well-defined boundaries.
  - Aggregates, entities, value objects, and domain events.

- **CQRS Pattern**:
  - Clear separation of **command** (write) and **query** (read) responsibilities.
  - Enhanced performance, scalability, and simplicity for data management.

- **API Gateway**:
  - Centralized entry point for all microservices.
  - Supports routing, load balancing, and service discovery.

- **Docker Integration**:
  - Simplified setup using `docker-compose`.
  - Containerized services for portability and ease of deployment.

- **Building Blocks**:
  - Shared components for logging, event handling, integration, and resilience.

- **Clean Architecture**:
  - Separation of concerns between core, application, and infrastructure layers.
  - Promotes testability and long-term maintainability.

---

## Project Structure

```plaintext
.
├── ApiGateways/       # API Gateway implementation
├── BuildingBlocks/    # Shared libraries and utilities
├── Services/          # Independent microservices
│   ├── Catalog/       # Product catalog management
│   ├── Basket/        # Shopping cart management
│   ├── Ordering/      # Order processing and management
│   └── Discount/      # Product discount management 
├── WebApps/           # Web front-end applications
├── data/              # Database or seed data
├── docker-compose.yml # Docker configuration for services
└── eshop-microservices.sln # Visual Studio solution file
```

---

## Prerequisites

- **.NET 8 SDK**: Ensure you have the latest .NET 8 SDK installed.
- **Docker**: Install Docker Desktop for containerized services.
- **SQL Server**: The application uses SQL Server for database operations.

---

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/mostafaelsayad745/EShopMicroservices.git
cd eShopMicroservices
```

### 2. Build and Run Services
Using Docker Compose:
```bash
docker-compose up --build
```

### 3. Access the Application
- API Gateway: [https://localhost:6064](https://localhost:6064)
- Swagger UI for each service is accessible via respective URLs.

---

## Key Technologies

- **.NET 8**: Framework for building high-performance, scalable applications.
- **Entity Framework Core**: ORM for database interactions.
- **PostgreSQL**: Database management system (ORDBMS)
- **RabbitMQ**: Message broker for event-driven communication.
- **Docker**: Containerization for development and deployment.

---

## How to Contribute

Contributions are welcome! Feel free to fork the repository, make your changes, and submit a pull request.

1. Fork the repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature/my-feature
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add my feature"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/my-feature
   ```
5. Open a pull request.

---

## License

This project is licensed under the [MIT License](LICENSE).

---
```

This file can be saved directly as `README.md` or any other `.md` file name. Let me know if there’s anything else you'd like to tweak!
