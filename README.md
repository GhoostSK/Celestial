🌌 Celestial.Agency – Modular .NET Platform (In Development)

Celestial.Agency is a modular, extensible .NET 9.0 solution built on clean architectural principles and enterprise-grade design patterns.
It’s part of the Celestial Project, focusing on providing a solid foundation for domain-driven, multi-layered applications.

⚙️ Note: This project is still under development and will continue to evolve as new modules and domain layers are added.

🧩 Project Structure
Celestial.Agency/
│
├── src/
│   ├── Celestial.Agency.Api/         # API Gateway (Presentation Layer)
│   │   ├── Controllers/              # API endpoints (Blog, Comment, Hotel, etc.)
│   │   ├── Areas/                    # Identity and Admin areas
│   │   ├── Models/                   # DTOs and ViewModels
│   │   ├── Data/                     # Identity & EF configurations
│   │   ├── appsettings.json          # Environment configuration
│   │   └── Program.cs                # Application entry point
│   │
│   ├── Celestial.Agency.Core/        # Domain & Core business logic
│   │   ├── DAL/                      # Core repositories and interfaces
│   │   │   ├── Model/                # Entity definitions
│   │   │   └── Repositories/         # Repository interfaces
│   │   ├── Enum/                     # Enumerations (e.g., LocalKeyGroupType)
│   │   ├── Attributes/               # Custom annotations and validators
│   │   ├── Services/                 # Domain service contracts
│   │   └── AppSetting.cs             # Global configurations
│   │
│   ├── Celestial.Agency.DAL.EF/      # Data Access Layer (EF Core)
│   │   ├── Context/                  # DbContext and model builder
│   │   ├── Configuration/            # EntityType configurations
│   │   ├── DependencyInjection/      # DI setup for EF
│   │   ├── Seeder/                   # Seed data for initial setup
│   │   ├── _01-add_migrations.cmd    # Helper script for adding migrations
│   │   ├── _02-update_db.cmd         # Helper script for applying migrations
│   │   └── _03-remove_db.cmd         # Helper script for resetting DB
│   │
│   └── Celestial.Agency.Service/     # Application Layer
│       ├── Command/                  # CQRS Commands
│       ├── CommandHandler/           # Handlers (MediatR)
│       ├── Behavior/                 # Pipeline behaviors (logging, validation)
│       ├── Service/                  # Application services
│       └── Dependencies/             # DI and service registration

🧱 Architecture

The system follows Clean Architecture and CQRS with MediatR, designed for modularity, scalability, and separation of concerns:

API Layer (Presentation): ASP.NET Core controllers exposing endpoints

Service Layer (Application): Commands, Handlers, and orchestration logic

Core Layer (Domain): Business entities, interfaces, and enums

EF Layer (Infrastructure): Data persistence with Entity Framework Core

🧠 Design Principles

CQRS + MediatR pattern

Repository + Unit of Work

Dependency Injection

Localization and multi-language support

Separation of Concerns

High scalability and extensibility

🧰 Technologies
Layer	Technology
Framework	.NET 9.0
ORM	Entity Framework Core
API	ASP.NET Core Web API
Architecture	Clean Architecture, CQRS, DDD
Libraries	MediatR, Mapster
Database	Microsoft SQL Server
Testing	xUnit (planned)
Logging	Serilog (planned)
