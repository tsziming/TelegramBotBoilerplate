"""This module is used for script configuration setting or changing."""

import json
from os import path
from core.color import Color
from core.console import loading, prompt, exit
from core.workdir import workdir_reset

workdir_reset()

class Config:
    # Relative path to source project from the solution root folder 
    source_project_path = "Source"
    # Relative path to tests project from the solution root folder
    tests_project_path = "Tests"
    # Relative path to scripts folder from the solution root folder
    scripts_folder_path = "Scripts"
    def __init__(self):
        self.source_project_path = "Source"
        self.scripts_folder_path = "Scripts"
        self.tests_project_path = "Tests"

config: Config = Config()
with open("config.json", "a+", encoding="utf-8") as f:
    pass
with open("config.json", "r+", encoding="utf-8") as f:
    f.seek(0)
    if f.read(1):
        f.seek(0)
        data: dict = json.load(f)
        config.source_project_path = data.get("source_project_path", "Source")
        config.scripts_folder_path = data.get("scripts_project_path", "Scripts")
        config.tests_project_path = data.get("tests_project_path", "Tests")
    f.seek(0)
    json.dump(config.__dict__, f, ensure_ascii=False, indent=4)

if __name__ == "__main__":
    print(f"{Color.B}Started Configuration Master!{Color.W}")
    config.source_project_path = prompt(
        f"Enter new source project path or just enter to skip this step " + 
        "(Relative path to source project from the solution root folder)"
    ) or config.source_project_path
    config.scripts_folder_path = prompt(
        f"Enter new scripts folder path or just enter to skip this step " +
        "(Relative path to scripts folder from the solution root folder)"
    ) or config.scripts_folder_path
    config.tests_project_path = prompt(
        f"Enter new tests project path or just enter to skip this step " +
        "(Relative path to tests project from the solution root folder)" 
    ) or config.tests_project_path
    loading("Writing changes to \"config.json\"")
    with open("config.json", "w", encoding="utf-8") as f:
        json.dump(config.__dict__, f, ensure_ascii=False, indent=4)
    print(f"{Color.G}Success! Configuration changed. {Color.W}")
    exit()
