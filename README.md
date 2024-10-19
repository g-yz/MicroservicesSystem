# Microservices Project
- Client Microservice
- Account Microservice

## Tech Stack
- .NET
- Entity Framework
- SQL Server
- RabbitMQ
- MassTransit
- Docker
- Docker Compose

## Requirements
- .NET SDK
- Docker
- Docker Compose

## Instructions

### Running the Project
1. Clone the repository:
```
https://github.com/g-yz/MicroservicesSystem.git
```
2. Open the project in Visual Studio.
3. Run the project using Docker Compose.
4. Verify that the services are active and the initial data load has been executed in the database.
5. Open the services in the browser:
* Client API: https://localhost:6011/swagger/index.html
* Account API: https://localhost:6010/swagger/index.html
6. Open the collections at the /Collections path in Postman:
    * /Collections/Clients API.postman_collection.json
    * /Collections/Accounts API.postman_collection.json

### Running Integration Tests

1. Run the project using Docker Compose.
2. Execute the integration tests.

### Modifying the Database

1. Open the corresponding schema files:
    * \ClientApp.Api\Db\DataBase.sql
    * \ClientApp.Api\Db\DataBase.Seeding.sql
    * \AccountApp.Api\Db\DataBase.sql
    * \AccountApp.Api\Db\DataBase.Seeding.sql
2. Modify and save the files.
3. Update the models using UpdateModels.sh.
4. Run the project using Docker Compose.
5. Open the services in the browser:
    * Client API: https://localhost:6011/swagger/index.html
    * Account API: https://localhost:6010/swagger/index.html


```mermaid
flowchart LR
    %% Broker Subgraph
    subgraph USER["User"]
        user
    end

    %% Broker Subgraph
    subgraph BROKER["Message Broker"]
        broker
    end
    
    %% Database Subgraph
    subgraph DATABASE["Database"]
        db
    end

    %% User Microservice Subgraph
    subgraph CLIENT["User Microservice"]
        user -->|POST| createUserApi["Create User API"]
        user -->|GET| getUserApi["Get User API"]
        user -->|GET| getAllUsersApi["Get All Users API"]
        user -->|PUT| updateUserApi["Update User API"]
        user -->|DELETE| deleteUserApi["Delete User API"]

        %% Service Actions for User
        createUserApi -->|Trigger Create| createUserService["Create User Service"]
        getUserApi -->|Trigger Get| getUserService["Get User Service"]
        getAllUsersApi -->|Trigger Get All| getAllUsersService["Get All Users Service"]
        updateUserApi -->|Trigger Update| updateUserService["Update User Service"]
        deleteUserApi -->|Trigger Delete| deleteUserService["Delete User Service"]

        %% Retrieve Information from DB
        getUserService -->|Retrieve User| db
        getAllUsersService -->|Retrieve All Users| db

        %% Publish Events for User
        createUserService -->|Publish UserCreated| broker
        updateUserService -->|Publish UserUpdated| broker
        deleteUserService -->|Publish UserDeleted| broker

        %% Consumer Processing for User
        broker -->|Route to Create User Consumer| createUserConsumer["Create User Consumer"]
        broker -->|Route to Update User Consumer| updateUserConsumer["Update User Consumer"]
        broker -->|Route to Delete User Consumer| deleteUserConsumer["Delete User Consumer"]

        %% Consumer Actions for User
        createUserConsumer -->|Insert User| db
        updateUserConsumer -->|Update User| db
        deleteUserConsumer -->|Delete User| db
    end

    %% Account Microservice Subgraph
    subgraph ACCOUNT["Account Microservice"]

        %% Account Subgraph
        subgraph ACCOUNT_SERVICE["Account Service"]
            user -->|POST| createAccountApi["Create Account API"]
            user -->|GET| getAccountApi["Get Account API"]
            user -->|GET| getAllAccountsApi["Get All Accounts API"]
            user -->|PUT| updateAccountApi["Update Account API"]
            user -->|DELETE| deleteAccountApi["Delete Account API"]

            %% Service Actions for Account
            createAccountApi -->|Trigger Create| createAccountService["Create Account Service"]
            getAccountApi -->|Trigger Get| getAccountService["Get Account Service"]
            getAllAccountsApi -->|Trigger Get All| getAllAccountsService["Get All Accounts Service"]
            updateAccountApi -->|Trigger Update| updateAccountService["Update Account Service"]
            deleteAccountApi -->|Trigger Delete| deleteAccountService["Delete Account Service"]

            %% Retrieve Information from DB for Account
            getAccountService -->|Retrieve Account| db
            getAllAccountsService -->|Retrieve All Accounts| db

            %% Publish Events for Account
            createAccountService -->|Publish AccountCreated| broker
            updateAccountService -->|Publish AccountUpdated| broker
            deleteAccountService -->|Publish AccountDeleted| broker

            %% Consumer Processing for Account
            broker -->|Route to Create Account Consumer| createAccountConsumer["Create Account Consumer"]
            broker -->|Route to Update Account Consumer| updateAccountConsumer["Update Account Consumer"]
            broker -->|Route to Delete Account Consumer| deleteAccountConsumer["Delete Account Consumer"]

            %% Consumer Actions for Account
            createAccountConsumer -->|Insert Account| db
            updateAccountConsumer -->|Update Account| db
            deleteAccountConsumer -->|Delete Account| db
        end

        %% Movement Subgraph
        subgraph MOVEMENT_SERVICE["Movement Service"]
            user -->|POST| createMovementApi["Create Movement API"]
            user -->|GET| getMovementApi["Get Movement API"]
            user -->|GET| getAllMovementsApi["Get All Movements API"]
            user -->|PUT| updateMovementApi["Update Movement API"]
            user -->|DELETE| deleteMovementApi["Delete Movement API"]

            %% Service Actions for Movement
            createMovementApi -->|Trigger Create| createMovementService["Create Movement Service"]
            getMovementApi -->|Trigger Get| getMovementService["Get Movement Service"]
            getAllMovementsApi -->|Trigger Get All| getAllMovementsService["Get All Movements Service"]
            updateMovementApi -->|Trigger Update| updateMovementService["Update Movement Service"]
            deleteMovementApi -->|Trigger Delete| deleteMovementService["Delete Movement Service"]

            %% Retrieve Information from DB for Movement
            getMovementService -->|Retrieve Movement| db
            getAllMovementsService -->|Retrieve All Movements| db

            %% Publish Events for Movement
            createMovementService -->|Publish MovementCreated| broker
            updateMovementService -->|Publish MovementUpdated| broker
            deleteMovementService -->|Publish MovementDeleted| broker

            %% Consumer Processing for Movement
            broker -->|Route to Create Movement Consumer| createMovementConsumer["Create Movement Consumer"]
            broker -->|Route to Update Movement Consumer| updateMovementConsumer["Update Movement Consumer"]
            broker -->|Route to Delete Movement Consumer| deleteMovementConsumer["Delete Movement Consumer"]

            %% Consumer Actions for Movement
            createMovementConsumer -->|Insert Movement| db
            updateMovementConsumer -->|Update Movement| db
            deleteMovementConsumer -->|Delete Movement| db
        end

    end
