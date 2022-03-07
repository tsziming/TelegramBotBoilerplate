# Telegram Boilerplate Scripts

"Scripts" in TelegramBoilerplate are simple tasks used for simplifying working with configurations and CLIs, you can sure perform all of these operations manually.

## Configuration

Scripts folder contains "config.json" file. Example:

```json
{
    // Relative path to source project from the solution root folder 
    "source_project_path": "Source",
    // Relative path to scripts folder from the solution root folder
    "scripts_folder_path": "Scripts",
    // Relative path to tests project from the solution root folder
    "tests_project_path": "Tests"
}
```

If you change folder names you should also change this file.

## Scripts List

1. [Common](#common)
2. [Database](#database)
3. [Bot](#bot)

### Common

- setup.py

> Used for first-time setup of telegram bot. User may delete it after successful installation complete.

- start.py

> Used for starting the bot (compilation instead recommended).

- config.py

> Used for script configuration setting or changing.

### Database

- database.py

> Used for changing or setting database configuration.

- migration.py

> Used for performing migrations after some DB-schema changing.

- database_update.py

> Used for updating database after migrations and etc.

### Bot

- token.py

> Used for setting up the telegram bot token to project secrets

## Core Package

Scripts in "core" package are the common tools for all plain scripts. Standalone run is not implemented.
