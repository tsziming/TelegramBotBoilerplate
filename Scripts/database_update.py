
# Updating database
from config import config
from core.color import Color
from core.console import confirm, loading, run_at, exit


if __name__ == "__main__" and not confirm("Would you like to update database?"):
    print("Then, it's OK. Stopping...")
loading("Updating database")
run_at(f"dotnet ef database update --project {config.source_project_path}","")

if __name__ == "__main__":
    print(f"{Color.G}Complete!{Color.W}")
    exit()
