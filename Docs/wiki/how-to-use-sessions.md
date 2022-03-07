# How to use sessions?

Session data is the user data in every separate chat. 

Session is an EF-Entity with shared primary key - `UserID` and `ChatID`.

There is a recommended way to use sessions via built-in Listener async methods `GetSession(Message | CallbackQuery)` and `SaveChanges()`.

But you can sure use session natively for more complex operations as a EntityFramework Core Database.

## Built-in Way (Recommended where possible)

1. In your listener/command `Handler()`/`Run()`/`RunAsync()` method initialize a Session variable and pass `Message` or `CallbackQuery`:

```C#
        public override async Task Handler(Context context, CancellationToken cancellationToken)
        {
            var session = await GetSession(context.Update.Message);
        }
```

2. Get all values you want from session:

```C#
        public override async Task Handler(Context context, CancellationToken cancellationToken)
        {
            var session = await GetSession(context.Update.Message);
            Console.WriteLine(session.Name);
        }
```

3. Change all values you want:

```C#
        public override async Task Handler(Context context, CancellationToken cancellationToken)
        {
            var session = await GetSession(context.Update.Message);
            Console.WriteLine(session.Name);
            session.Name = "[VIP] " + session.Name;
        }
```

4. Save your changes with `SaveChanges()` method:

```C#
        public override async Task Handler(Context context, CancellationToken cancellationToken)
        {
            var session = await GetSession(context.Update.Message);
            Console.WriteLine(session.Name);
            session.Name = "[VIP] " + session.Name;
            await SaveChanges();
        }
```

5. Complete! Now you can start your bot and test your new session-ful business logic.

## Entity Framework Way

1. In any place of your code get database context:

```C#
SessionContext db = Database.GetConnection().SessionContext;
```

2. Then you can perform some [Entity Framework Core operations](https://docs.microsoft.com/en-us/ef/core/):

```C#
using (SessionContext db = Database.GetConnection().SessionContext) 
{
       session = await db.FindAsync<Session>(
                    message.Chat.Id,
                    userId
                );
}
```

3. Complete! Now you can start your bot and test your new session-ful business logic.
