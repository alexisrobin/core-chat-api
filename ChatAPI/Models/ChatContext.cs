using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatAPI.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatUser>()
                .HasKey(e => new { e.ChatId, e.UserId });

            modelBuilder.Entity<ChatUser>()
                .HasOne(e => e.Chat)
                .WithMany(c => c.ChatUsers)
                .HasForeignKey(e => e.ChatId);

            modelBuilder.Entity<ChatUser>()
                .HasOne(e => e.User)
                .WithMany(u => u.ChatUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Message>()
                .HasOne(e => e.ChatUser)
                .WithMany(b => b.Messages)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
