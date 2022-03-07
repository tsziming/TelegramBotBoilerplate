using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTelegramBot.Types
{
    public class Context
    {
        public ITelegramBotClient BotClient { get; }
        public Update Update { get; }
        public Context(Update update, ITelegramBotClient botClient)
        {
            Update = update;
            BotClient = botClient;
        }
    }
}
