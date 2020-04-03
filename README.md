# Poseidon
_[OpenClassrooms .NET Back-End Developer Path](https://openclassrooms.com/en/paths/156-back-end-developer-net), Project #7_

## Introduction

The overarching objective of this project is the implementation of a RESTful API. **The project specifications contain large gaps, and this project should be considered a technical exercise.**

Significantly:
- The entities are supplied in a non-specified SQL dialect, and as such their implementations herein are best guesses
- There is no indication whatsoever of entity relationships
- No functional requirements are specified, and as such 
    - the service layer is rather sparse as there are no business rules to implement
    - all validation and user authorization are completely arbitrary and implemented solely to demonstrate capability

## Overview

The project specifications are, essentially, to implement:
1. A secure identity layer for authentication and authorization
2. CRUD capability for several unrelated entities 

The project is implemented using:
- SQL Server
- ASP.NET Core (Web API + Blazor WebAssembly client)
- IdentityServer4

## How to use

### Requirements:
- SQL Server 2019
- Visual Studio 2019 
- .NET Core 3.1

Correct functionality can not be guaranteed for other versions.

### To use:
1. Clone or download this repo
2. Either:
    - Load the solution in Visual Studio and run using `Ctrl+F5` (the API, client, and IdentityServer together are configured as the default project)
    - Run the projects using the `dotnet` CLI 
3. In a web browser, navigate to
    - `https://localhost:5001` to view/test the API using Swagger UI
    - `https://localhost:5002` to view/test the API using the Blazor client

Upon running the ``Poseidon.IdentityServer`` project, the relevant database is seeded with two test users:
- **Admin**: admin@poseidon.test / Pass123$
- **User**: user@poseidon.test / Pass123$

If using the Swagger UI, in order to log in with a different user it's required to both log out and clear the site cookies before logging in again ([see this comment](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/436#issuecomment-443745345)).

You can also test the API using Postman. Request an access token by using the Client Credentials grant type, and fill in the relevant details as found in `Poseidon.IdentityServer/Config.cs`.

The application(s) can of course also be run using the `dotnet` CLI. 



 
