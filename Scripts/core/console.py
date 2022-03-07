import os
from sys import stdout
from time import sleep
from core.color import Color
from core.workdir import workdir_reset, workdir_root


def confirm(question: str) -> bool:
    return True if input(f"({Color.O}?{Color.W}) {question} (Y/n): ") == 'Y' else False

def prompt(question: str) -> str:
    return input(f"({Color.O}?{Color.W}) {question}: ")

def warning(message: str) -> None:
    print(f"({Color.R}!{Color.W}) {message}")

def loading(message: str) -> None:
    stdout.write(f"({Color.B}|{Color.W}) {message}...")
    sleep(0.2)
    stdout.write(f"\r({Color.B}/{Color.W}) {message}...")
    sleep(0.2)
    stdout.write(f"\r({Color.B}\{Color.W}) {message}...")
    sleep(0.2)
    stdout.write(f"\r({Color.G}x{Color.W}) {message}... \n")

def run_at_relative(command, location):
    if location:
        os.chdir(os.path.realpath(location))
    os.system(command)
    workdir_reset()
    

def run_at(command, location):
    workdir_root()
    run_at_relative(command, location)

def exit():
    print("Press enter to exit...")
    input()
    quit()
