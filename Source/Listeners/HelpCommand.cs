using System.Threading;
using MyTelegramBot.Types;

namespace MyTelegramBot.Listeners {
    public class HelpCommand : Command {
        public HelpCommand(Bot bot): base(bot) {
            Names = new string[]{"/help", "!help"};
        }
        public override string Run(Context context, CancellationToken cancellationToken)
        {
            return "<b>MyTestBot commands</b> \n\n" +
                "/start - starts bot \n" +
                "/help - opens this message \n" +
                "/me - user profile \n\n" +
                "Bot calculates message number for each user in each chat.";
        }
    }
}
