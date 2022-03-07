using System.Threading;
using System.Threading.Tasks;
using MyTelegramBot.Types;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
            var session = await GetSession(context.Update.Message);
            session.Messages++;
            await SaveChanges();
        }
    }
}
