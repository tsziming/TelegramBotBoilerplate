using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using MyTelegramBot.Utils;

namespace MyTelegramBot.Types {
    /// <summary>
    /// Class <c>CommandNameInvalidException</c> describes an exception thrown when command name
    /// for some reason is invalid.
    /// </summary>
    public class CommandNameInvalidException : Exception
    {
        /// <summary>
        /// Creates a <c>CommandNameInvalidException</c> with default data.
        /// </summary>
        public CommandNameInvalidException() { }
        /// <summary>
        /// Creates a <c>CommandNameInvalidException</c> with certain message.
        /// </summary>
        /// <param name="message">the message that describes the error</param>
        public CommandNameInvalidException(string message) : base(message) { }
        /// <summary>
        /// Creates a <c>CommandNameInvalidException</c> with certain message 
        /// and inner exception.
        /// </summary>
        /// <param name="message">the message that describes the error</param>
        /// <param name="inner">
        /// the exception that is a cause of the current 
        /// exception or a null-reference if no inner exception is specified.
        /// </param>
        public CommandNameInvalidException(string message, Exception inner): base(message, inner) { }
    }
    /// <summary> 
    /// Abstract Class <c>Command</c> describes a bot command handler with
    /// a simplified interface, utilities and validate conditions.
    /// </summary>
    public abstract class Command : Listener
    {
        /// <value>
        /// Property <c>ArgumentParser</c> represents the command argument parser.
        /// </value>
        public CommandParser ArgumentParser { get; set; }
        private string[] names;
        /// <value>
        ///  Property <c>Names</c> represents the command trigger names.
        /// </value>
        public string[] Names {
            get => names;
            set {
                foreach (string name in value)
                {
                    if (name.Contains(ArgumentParser.Divider))
                    {
                        throw new CommandNameInvalidException(
                            "Command name must not contain command parser divider."
                        );
                    }
                }
                names = value;
            }
        }
        /// <summary>
        ///  Creates a <c>Command</c> for the specified <c>Bot</c>.
        /// </summary>
        public Command(Bot bot) : base(bot) 
        {
            ArgumentParser = new CommandParser();
        }
        /// <summary>Checks if the <c>Command</c> matches the command conditions.</summary>
        public override bool Validate(Context context, CancellationToken cancellationToken)
        {
            if (context.Update.Type != UpdateType.Message)
                return false;
            if (context.Update.Message!.Type != MessageType.Text)
                return false;
            string messageText = context.Update.Message.Text.Replace($"@{Bot.Me.Username}","");

            foreach(string name in Names) {
                if (messageText.StartsWith($"{name} ") || messageText.Equals(name)) {
                    return true;
                }
            }
            return false;
        }
        /// <summary>Handles the <c>Command</c> if it is successfully validated.</summary>
        public override async Task Handler(Context context, CancellationToken cancellationToken)
        {
            string response = await RunAsync(context, cancellationToken);
            Int64 chatId = context.Update.Message.Chat.Id;
            if (response.Length == 0)
            {
                return;
            }
            Message sentMessage = await context.BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: response,
                parseMode: Config.ParseMode,
                replyToMessageId: context.Update.Message.MessageId
            );
        }
        /// <summary>Processes a command synchronously.</summary>
        /// <returns>Command result string.</returns>
        public virtual string Run(Context context, CancellationToken cancellationToken) {
            return "This command is under development and not currently available.";
        }
        /// <summary>Processes a command asynchronously.</summary>
        /// <returns>Command result string.</returns>
        public virtual async Task<string> RunAsync(Context context, CancellationToken cancellationToken) {
            return Run(context, cancellationToken);
        }
    }
}
