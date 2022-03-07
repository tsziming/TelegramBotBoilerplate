using System.Threading;
using MyTelegramBot.Types;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTelegramBot.Listeners {
    public class StartCommand : Command {
        public StartCommand(Bot bot): base(bot) {
            Names = new string[]{"/start", "/starting", "!start"};
        }
        public override string Run(Context context, CancellationToken cancellationToken)
        {
            return "Welcome! Press /help to see my functions.";
        }
    }
}
