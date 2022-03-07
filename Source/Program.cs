
namespace MyTelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bot bot = new Bot()
            {
                Token = Config.BotToken,
            };

            bot.Init().Wait();
        }
    }
}
