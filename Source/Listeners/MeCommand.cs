using System.Threading;
using System.Threading.Tasks;
using Model;
using MyTelegramBot.Types;

namespace MyTelegramBot.Listeners {
    public class MeCommand : Command {
        public MeCommand(Bot bot): base(bot) {
            Names = new string[]{"/me", "!me"};
        }
        public override async Task<string> RunAsync(Context context, CancellationToken cancellationToken)
        {
            var message = context.Update.Message;
            Session session;
            if (ArgumentParser.Validate(message.Text, 1))
            {
                long userId;
                string args = ArgumentParser.Parse(message.Text).ArgumentsText;
                long.TryParse(args, out userId);
                SessionContext db = Database.GetConnection().SessionContext;
                session = await db.FindAsync<Session>(
                    message.Chat.Id,
                    userId
                );    
                if (session == null)
                {
                    return "User not found";
                }
            }
            else 
            {
                session = await GetSession(context.Update.Message);
            }
            return "<b>MyTestBot profile</b> \n\n" +
                $"TG-ID: {session.UserID} \n" +
                $"Name: {session.Name} \n" +
                $"Messages: {session.Messages}\n";
        }
    }
}
