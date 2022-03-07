"""This module is used for first-time setup of telegram bot. User may delete it after successful installation complete."""
from importlib import import_module
from config import config
from core.color import Color
from core.console import prompt, exit, run_at
from core.workdir import workdir_reset

workdir_reset()

print(f"{Color.B}Started Telegram Bot Boilerplate Installation Master!{Color.W}")

# 1. Building Project
name = prompt(f"Enter your project name")
run_at(f"dotnet new sln --name {name}", "")
run_at(f"dotnet sln add {config.source_project_path}", "")
run_at(f"dotnet sln add {config.tests_project_path}", "")

# 2. Saving Bot Token To Secrets
import_module("token")

# 3. Database Setup
import_module("database")

# 4. Finish!
print(
    f"{Color.G}Success! Now you can start your bot via \"start.py\" script"+
    f" or CLI command {Color.B}dotnet run --project {config.source_project_path} {Color.W}"
)

if __name__ == "__main__":
    exit()
