"""This module is used for changing or setting database configuration."""
from importlib import import_module
import json
import os
from config import config
from core.color import Color
from core.console import confirm, loading, prompt, run_at, warning, exit
from core.workdir import workdir_reset, workdir_root
from core.provider import providers

workdir_reset()

if __name__ == "__main__":
    print(f"{Color.B}Started Database Installation Master!{Color.W}")
    if not confirm("Would you like to change database provider?"):
        exit()
    if not confirm("To change provider I will delete existing database migrations. Are you ok with this?"):
        exit()
    warning(f"This will delete \"Migrations\" folder in \"{config.source_project_path}\"")
    if not confirm("Are you sure want to delete existing database migrations?"):
        print("Fuuh... You've almost performed a barely cancelable action.")
        exit()
    loading("Deleting migrations")
    run_at("rm -r Migrations", config.source_project_path)

# 1. Choosing Database Provider
for i, provider in enumerate(providers):
    print(f"{Color.G}{i+1}. {provider.name.capitalize()}{Color.W}")
index = None
while index is None:
    try:
        index = int(prompt(f"Choose your database provider (1-{len(providers)})"))-1
        if  0 >= index+1 or index+1 > len(providers):
            raise ValueError()
    except ValueError:
        index = None
        warning("No such option")
        pass
provider = providers[index]

# 2. Filling Database Properties
if provider.standalone:
    connection_string = "Server={};Port={};Database={};Uid={};Pwd={};".format(
        prompt(f"Enter your {provider.name}-server (without port)"),
        prompt(f"Enter your {provider.name}-server port"),
        prompt(f"Enter your {provider.name}-database name"),
        prompt(f"Enter your {provider.name}-server username"),
        prompt(f"Enter your {provider.name}-server password"),
    )
else:
    workdir_root()
    connection_string = "Data Source={};".format(
       os.path.abspath(
        prompt(f"Enter your full (absolute) {provider.name}-database path (with *.db)"),
       )
    )
    workdir_reset()

# 3. Creating "appsetting.json" File
appsettings = {
    "ConnectionStrings": {
        "DefaultConnection": connection_string,
    },
    "Database": {
        "Provider": provider.name,
    }
}
loading("Generating appsettings.json...")
with open(f'../{config.source_project_path}/appsettings.json','w', encoding='utf-8') as f:
    json.dump(appsettings, f, ensure_ascii=False, indent=4)

if confirm("To proceed installation you need to install dotnet-ef globally. Do you agree?"):
    run_at("dotnet tool install --global dotnet-ef", config.source_project_path)
    loading("Loading dotnet-ef...")
else:
    warning("You cannot proceed without ef-dotnet installation.")
    exit()

# 4. Database Migration
import_module("migration")

# 5. Finish!
print(f"{Color.G}Database successfuly configured!{Color.W}")

if __name__ == "__main__":
    exit()
