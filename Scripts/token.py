"""This module is used for setting up the telegram bot token to project secrets."""
from config import config
from core.color import Color
from core.console import loading, prompt, run_at, exit
from core.workdir import workdir_reset

workdir_reset()

token = prompt(f"Enter your bot token from @BotFather")
loading("Setting token")
run_at(f"dotnet user-secrets set BotToken \"{token}\"", config.source_project_path)

if __name__ == "__main__":
    print(f"{Color.G} Success! {Color.W}")
    exit()
