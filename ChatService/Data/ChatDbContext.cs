using ChatService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(
            DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
    }
}