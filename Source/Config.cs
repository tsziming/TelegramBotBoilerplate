using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types.Enums;

namespace MyTelegramBot {
    public static class Config {
        public static ParseMode ParseMode { get; } = ParseMode.Html;
        public static string BotToken { get => Configuration["BotToken"]; }
        public static string DbString { get => Configuration.GetConnectionString("DefaultConnection"); }
        public static string DbProvider { get => Configuration.GetSection("Database")?["Provider"]?.ToLower() ?? "sqlite"; }
        
        // Common
        public static IConfiguration Configuration { get; set; }
        static Config() {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddUserSecrets<Program>()
                            .AddEnvironmentVariables();
            Configuration = builder.Build();
            if (DbString == null)
            {
                throw new NullReferenceException("Database connection string is null!");
            }
            if (DbProvider == null)
            {
                throw new NullReferenceException("Database provider is null!");
            }
        }
    }
}
