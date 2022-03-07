using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyTelegramBot.Listeners;
using MyTelegramBot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace MyTelegramBot {
    public class Bot {
        public List<Listener> Listeners { get; set; }
        public User Me { get; set; }
        public string Token { get; set; }
        public Bot()
        {
            Listeners = new List<Listener> {
                new StartCommand(this),
                new HelpCommand(this),
                new MeCommand(this),
                new EchoCommand(this),
                new MessageListener(this),
                // TODO: Put more commands and other listeners.
            };
        }
        public async Task Init() 
        {
            Console.WriteLine("Initializing bot...");
            TelegramBotClient botClient = new TelegramBotClient(Token);
            using CancellationTokenSource cts = new CancellationTokenSource();
            ReceiverOptions receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = {},
            };

            Console.WriteLine("Starting bot..."); 
            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token
            );

            Me = await botClient.GetMeAsync();
            Console.WriteLine($"Start listening for @{Me.Username}");
            Console.Read();

            cts.Cancel();
        }
        public Task EmitUpdate(Context context, CancellationToken cancellationToken)
        {
            return HandleUpdateAsync(context.BotClient, context.Update, cancellationToken);
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Context context = new Context(update, botClient);
            foreach (Listener listener in Listeners) {
                if (listener.Validate(context, cancellationToken))
                {
                    await listener.Handler(context, cancellationToken);
                }
            }
        }
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
