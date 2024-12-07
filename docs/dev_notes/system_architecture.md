# System architecture

This document outlines the system architecture for the *Europhonium* project.

- [System architecture](#system-architecture)
  - [Vertical slice architecture](#vertical-slice-architecture)
  - [REST maturity](#rest-maturity)
  - [REPR pattern](#repr-pattern)
  - [Railway-oriented programming](#railway-oriented-programming)
  - [Technologies](#technologies)

## Vertical slice architecture

The system uses the **vertical slice architecture**. Code is grouped by feature.

| ![assembly architecture](media/assembly-architecture.png) |
|:---------------------------------------------------------:|
|                  Assembly architecture.                   |

The above diagram illustrates the 5 .NET assemblies that comprise the system, and their dependencies. The assemblies have the following roles:

| Assembly                            | Type          | Role                                                                                                   |
|:------------------------------------|:--------------|:-------------------------------------------------------------------------------------------------------|
| `Europhonium.Modules.Admin`         | Class library | *Admin Module* feature files, including endpoints, view models, handlers, etc.                         |
| `Europhonium.Modules.Public`        | Class library | *Public Module* feature files, including endpoints, view models, handlers, repositories, etc.          |
| `Europhonium.Shared.Domain`         | Class library | Application domain types.                                                                              |
| `Europhonium.Shared.Infrastructure` | Class library | Data access types, security settings, core (non-middleware) service dependencies for both modules.     |
| `Europhonium.WebApi`                | Web API       | Web app executable and global middleware.                                                              |

## REST maturity

The system aims for **Level 2 REST API maturity**. It does not include hypermedia navigation.

## REPR pattern

The system uses the **REPR pattern**. Each feature has its own separate endpoint, with its own request and response types.

## Railway-oriented programming

The system uses **Railway-oriented programming**. Every request enters the application pipeline and returns as *either* a result *or* an error. The endpoint maps the application pipeline return value to a successful HTTP response or an unsuccessful HTTP response containing a serialized `ProblemDetails` object.

## Technologies

The system is developed as a **.NET minimal web API**. As much as possible, the API endpoints and middleware use the native .NET libraries rather than third-party code.

The following libraries are used:

| Library     | Role                                     |
|:------------|:-----------------------------------------|
| MediatR     | Application command pipeline.            |
| EF Core     | Domain aggregate access and persistence. |
| Dapper      | Read-only data queries.                  |
| Swashbuckle | Swagger document generation.             |
| ErrorOr     | Railway-oriented method results.         |

The system uses an **Azure SQL Database** hosted in the cloud.
