"""This module is used for performing migrations after some DB-schema changing."""
from importlib import import_module
from config import config
from core.color import Color
from core.console import confirm, exit, loading, prompt, exit, run_at
from core.workdir import workdir_reset

workdir_reset()

if __name__ == "__main__":
    print(f"{Color.B}Started Database Migration Master!{Color.W}")

# Performing migration
if __name__ == "__main__" and not confirm("Would you like to perform migration?"):
    print("Then, it's OK. Stopping...")
    exit()
name = "Initial"
if __name__ == "__main__":
    name = prompt("Enter migration name")
loading("Performing initial database migration")
run_at(f"dotnet ef migrations add {name} --project {config.source_project_path}","")

# Updating database
import_module("database_update")

if __name__ == "__main__":
    print(f"{Color.G}Complete!{Color.W}")
    exit()
