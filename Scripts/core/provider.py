
class Provider:
    name: str
    standalone: bool
    def __init__(self, name: str, standalone: bool):
        self.name = name
        self.standalone = standalone

providers = [
    Provider("sqlite", False),
    Provider("mysql", True),
    Provider("postgresql", True),
    Provider("mssql", True),
]
