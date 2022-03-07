using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;
using Model;

namespace MyTelegramBot.Types {
    /// <summary>Abstract Class <c>Listener</c> describes a bot event handler with 
    /// utilities and validate conditions. </summary>
    public abstract class Listener
    {
        /// <value>
        /// Property <c>Bot</c> represents a <c>Bot</c> instance with which <c>Listener</c> is related.
        /// </value>
        public Bot Bot { get; set; }
        /// <summary>
        ///  Creates a <c>Listener</c> for the specified <c>Bot</c>.
        /// </summary>
        public Listener(Bot bot)
        {
            Bot = bot;
        }
        /// <summary>Checks if the <c>Update</c> matches the listener condition.</summary>
        public abstract bool Validate(Context context, CancellationToken cancellationToken);
        /// <summary>Handles the <c>Update</c> if it is successfully validated.</summary>
        public abstract Task Handler(Context context, CancellationToken cancellationToken);
        /// <returns>The session of the sender of a given <c>Message</c> object.</returns>
        public async Task<Session> GetSession(Message message) {
            var db = Database.GetConnection().SessionContext;
            var session = await db.FindAsync<Session>(
                message.Chat.Id,
                message.From.Id
            );
            if (session == null) {
                session = (await db.AddAsync<Session>(new Session() {
                    ChatID=message.Chat.Id,
                    UserID=message.From.Id,
                    Name=message.From.Username ?? message.From.FirstName
                })).Entity;
            }
            return session;
        }
        /// <returns>The session of the sender of a given <c>CallbackQuery</c> object.</returns>
        public async Task<Session> GetSession(CallbackQuery query) {
            var db = Database.GetConnection().SessionContext;
            var session = await db.FindAsync<Session>(
                query.Message.Chat.Id,
                query.From.Id
            );
            if (session == null) {
                session = (await db.AddAsync<Session>(new Session() {
                    ChatID=query.Message.Chat.Id,
                    UserID=query.From.Id,
                    Name=query.From.Username ?? query.From.FirstName
                })).Entity;
            }
            return session;
        }
        /// <summary>Saves all changes to <c>Session</c> object.</summary>
        public async Task SaveChanges() {
            var db = Database.GetConnection().SessionContext;
            await db.SaveChangesAsync();
        }
    }
}
