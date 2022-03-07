import os

scripts_path = os.path.join(__file__, "..")

def workdir_reset():
    os.chdir(os.path.dirname(os.path.realpath(scripts_path)))

def workdir_root():
    workdir_reset()
    os.chdir(os.path.dirname(os.path.realpath(".")))
