# How to create a command?

In TelegramBotBoilerplate commands are the classes that inherited from `Command` type.

Command is a `Listener` child, so it is also stored in "Listeners" folder:

![Folder structure](/TelegramBotBoilerplate/Docs/wiki/listeners-folder-structure.png)

## Step-by-step Guide

1. Create a class named with "<YourOriginalName>Command" pattern and place it in "Listeners" folder or its subfolders:

```C#
namespace MyTelegramBot.Listeners {
    public class StartCommand : Command {}
}
```

2. Add the constructor with `Bot` parameter and its triggers (`Names` property) array:

```C#
namespace MyTelegramBot.Listeners {
    public class StartCommand : Command {
        public StartCommand(Bot bot): base(bot) {
            Names = new string[]{"/start", "/starting", "!start"};
        }
    }
}
```

3. Override `Run()` **or** `RunAsync()` methods where you put your business logic:

```C#
namespace MyTelegramBot.Listeners {
    public class StartCommand : Command {
        public StartCommand(Bot bot): base(bot) {
            Names = new string[]{"/start", "/starting", "!start"};
        }
        public override string Run(Context context, CancellationToken cancellationToken)
        {
            // Here you put your business logic
            return "Welcome! Press /help to see my functions.";
        }
    }
}
```

4. Add the command object to bot listeners array in `/Source/Bot.cs` constructor and pass `this` as a parameter:

```C#
        ...
        public Bot()
        {
            Listeners = new List<Listener> {
                new StartCommand(this),
                new HelpCommand(this),
                ...
                // TODO: Put more commands and other listeners.
            };
        }
        ...
```

5. Complete! Now you can start your bot and test your new command!


