using AspNetCoreMvcNotesAPP.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMvcNotesAPP.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-48HDLSD;Database=NotesAppDB;Trusted_Connection=true");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}
