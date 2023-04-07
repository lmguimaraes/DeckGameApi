using DeckGameApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeckGameApi.Infrastrucutre.Persistence
{
    public class DeckGameDbContext : DbContext
    {
        public DeckGameDbContext(DbContextOptions<DeckGameDbContext> options)
                : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDeck>().HasMany(d => d.Decks);
            modelBuilder.Entity<GameDeck>().HasMany(p => p.Players);
        }

        public DbSet<Deck> Decks { get; set; }
        public DbSet<GameDeck> GameDecks { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
