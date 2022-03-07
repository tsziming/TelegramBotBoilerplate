# Installation Guide

## Prerequisites

- git [download](https://git-scm.com/)
- dotnet v5.0+ [download](https://dotnet.microsoft.com/)

## Installation

1. Install telegram-bot-boilerplate [manually](#manual-installation) or via [installation master](#using-installation-master).

2. Build the solution:

    ```bash
    dotnet build
    ```

3. Run the application with one of the following variants:
    - via compiled binary file ("Source/bin" subfolders)
    - via Visual Studio (generated *.sln file in root folder)
    - via script ("Scripts/start.py"):
    - via CLI command:

    ```bash
    dotnet run --project Source
    ```

### Using Installation Master

1. Create a bot in [@BotFather](https://t.me/BotFather) and obtain the bot token.

2. Clone repository to the place you want:

    ```bash
    git clone https://github.com/tsziming/telegram-bot-boilerplate.git
    ```

3. Run "Scripts/setup.py" file and follow the instructions:

    ```bash
    python Scripts/setup.py
    ```

### Manual Installation

1. Create a bot in [@BotFather](https://t.me/BotFather) and obtain the bot token.

2. Clone repository to the place you want:

    ```bash
    git clone https://github.com/tsziming/telegram-bot-boilerplate.git
    ```

3. Open *cmd* / *your IDE with cmd* in the project folder.  

4. Add your bot token from the first paragraph to dotnet user-secrets:

    ```bash
    dotnet user-secrets set BotToken "YOUR_TOKEN"
    ```

5. Create the solution:

    ```bash
    dotnet new solution --name "YOUR_SOLUTION_NAME"
    ```

6. Add all projects to the solution:

    ```bash
    dotnet solution add "Source"
    ```

    ```bash
    dotnet solution add "Tests"
    ```

7. Create "appsettings.json" in the source project folder and add the "DefaultConnection" and "Provider" properties:

    ```json
    {
        "ConnectionStrings": {
            "DefaultConnection": "HERE YOU PUT YOUR DATABASE CONNECTION STRING"
        },
        "Database": {
            "Provider": "CHOOSE A DATABASE PROVIDER (mysql/sqlite/postgresql/mssql)"
        }
    }
    ```

    **Example**

    ```json
    {
        "ConnectionStrings": {
            "DefaultConnection": "Data Source=telegram.db"
        },
        "Database": {
            "Provider": "sqlite"
        }
    }
    ```

8. Install Entity Framework Core Tool for dotnet:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

9. Create database migration:

    ```bash
    dotnet ef migrations add InitialCreate --project Source
    ```

10. Update database:

    ```bash
    dotnet ef database update --project Source
    ```
