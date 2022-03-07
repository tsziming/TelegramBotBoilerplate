# How to create a custom listener?

In TelegramBoilerplate custom listeners are inherited from `Listener` type.

Listeners are stored in "Listeners" folder:

![Folder structure](https://github.com/tsziming/TelegramBotBoilerplate/blob/master/Docs/wiki/listeners-folder-structure.png)

## Step-by-step Guide

1. Create a class named with "<YourOriginalName>Listener" pattern and place it in "Listeners" folder or its subfolders:

```C#
namespace MyTelegramBot.Listeners {
    public class MessageListener : Listener {}
}
```

2. Add the constructor with `Bot` parameter:

```C#
namespace MyTelegramBot.Listeners {
    public class MessageListener : Listener
    {
        public MessageListener(Bot bot):base(bot) {}
    }
}
```

3. Override `Validate()` method where you put your listener filters:

```C#
namespace MyTelegramBot.Listeners {
    public class MessageListener : Listener
    {
        public MessageListener(Bot bot):base(bot) {}
        public override bool Validate(Context context, CancellationToken cancellationToken)
        {
            if (context.Update.Type != UpdateType.Message) // listener filters all non-message updates
                return false;
            return true;
        }
    }
}
```

4. Override `Handler()` method where you put your business logic:

```C#
namespace MyTelegramBot.Listeners {
    public class MessageListener : Listener
    {
        public MessageListener(Bot bot):base(bot) {}
        public override bool Validate(Context context, CancellationToken cancellationToken)
        {
            if (context.Update.Type != UpdateType.Message)
                return false;
            return true;
        }
        public override async Task Handler(Context context, CancellationToken cancellationToken)
        {
            // (Business logic) Listener increments message count for every call
            var session = await GetSession(context.Update.Message);
            session.Messages++;
            await SaveChanges();
        }
    }
}
```

5. Add the listener object to bot listeners array in `/Source/Bot.cs` constructor and pass `this` as a parameter:

```C#
        ...
        public Bot()
        {
            Listeners = new List<Listener> {
                ...
                new MessageListener(this)
                // TODO: Put more commands and other listeners.
            };
        }
        ...
```

6. Complete! Now you can start your bot and test your new listener!
