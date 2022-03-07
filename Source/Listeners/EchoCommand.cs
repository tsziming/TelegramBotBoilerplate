using System.Threading;
using MyTelegramBot.Types;

namespace MyTelegramBot.Listeners {
    public class EchoCommand : Command {
        public EchoCommand(Bot bot): base(bot) {
            Names = new string[]{"/echo", "!echo"};
        }
        public override string Run(Context context, CancellationToken cancellationToken)
        {
            var message = context.Update.Message;
            if (ArgumentParser.Validate(message.Text)) {
                var args = ArgumentParser.Parse(message.Text);
                return args.ArgumentsText;
            }
            return "No arguments provided.";
        }
    }
}
