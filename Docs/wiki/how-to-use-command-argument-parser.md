Command arguments are the text parts that the user passes with a command:

![Command example](https://github.com/tsziming/TelegramBotBoilerplate/blob/master/Docs/wiki/command-example.png)

In TelegramBotBoilerplate command argument parsing is built-in with simple utility.

# Step-by-step Guide

1. Find `Run()` method in the command that you want to enhance with arguments:

```C#
        public override string Run(Context context, CancellationToken cancellationToken)
        {
            return "Coming soon...";
        }
```

2. Use `ArgumentParser` to validate message text for some number of arguments:

```C#
        public override string Run(Context context, CancellationToken cancellationToken)
        {
            var message = context.Update.Message;
            if (!ArgumentParser.Validate()) {
               return "no arguments provided";
            }
            if (ArgumentParser.Validate(message.Text, 1)) {
               return "one argument provided";
            }
            if (ArgumentParser.Validate(message.Text, 2, 4)) {
               return "Minimum 2 and maximum 4 arguments provided";
            }
            if (ArgumentParser.Validate(message.Text, 5, CommandParserValidateOptions.EqualsOrGreater)) {
               return "five or more arguments provided";
            }
            return "This return will never emit.";
        }
```

3. Use `ArgumentParser` to parse message text and make some business logic with `ParsedCommand` object:

```C#
        public override string Run(Context context, CancellationToken cancellationToken)
        {
            var message = context.Update.Message;
            if (!ArgumentParser.Validate()) {
               return "no arguments provided";
            }
            ParsedCommand args = ArgumentParser.Parse(message.Text);
            if (ArgumentParser.Validate(message.Text, 1)) {
               return $"one argument provided - {args.Arguments[0]}";
            }
            if (ArgumentParser.Validate(message.Text, 2, 4)) {
               return $"Minimum 2 and maximum 4 arguments provided ({args.Arguments.Length})";
            }
            if (ArgumentParser.Validate(message.Text, 5, CommandParserValidateOptions.EqualsOrGreater)) {
               return $"five or more arguments provided - \"{args.ArgumentsText}\"";
            }
            return "This return will never emit.";
        }
```

4. Complete! Now you can start your bot and test your new command with arguments!  
