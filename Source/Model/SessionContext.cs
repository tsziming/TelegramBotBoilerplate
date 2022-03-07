using System;
using Microsoft.EntityFrameworkCore;
using MyTelegramBot;

namespace Model {
    public class SessionContext: DbContext {
        public DbSet<Session> Sessions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defines ChatID and UserID as a Session primary key.
            modelBuilder.Entity<Session>()
                .HasKey(c => new { c.ChatID, c.UserID });
            // Defines 0 as a default value for Session messages property.
            modelBuilder.Entity<Session>()
                .Property(c => c.Messages)
                .HasDefaultValue(0);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            if (Config.DbProvider == "sqlite") {
                options.UseSqlite(Config.DbString);
            }
            else if (Config.DbProvider == "mysql") {
                options.UseMySql(Config.DbString, ServerVersion.AutoDetect(Config.DbString));
            }
            else if (Config.DbProvider == "postgresql") {
                options.UseNpgsql(Config.DbString);
            }
            else if (Config.DbProvider == "sqlserver" || Config.DbProvider == "mssql") {
                options.UseSqlServer(Config.DbString);
            }
            else {
                throw new InvalidOperationException("Invalid provider.");
            }
        }
    }
}
