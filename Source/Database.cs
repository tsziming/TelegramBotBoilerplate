using Model;

namespace MyTelegramBot {
    public class Database {
        private static Database database;
        public static Database GetConnection() {
            if (database == null) {
                database = new Database();
            }
            return database;
        }

        public SessionContext SessionContext { get; set; }
        public Database() {
            SessionContext = new SessionContext();
        }
    }
}
