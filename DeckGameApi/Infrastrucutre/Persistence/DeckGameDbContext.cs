using DeckGameApi.Domain.Entities;
using DeckGameApi.Domain.Entities.Enums;
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
            modelBuilder.Entity<Player>().HasOne(p => p.Hand);
            modelBuilder.Entity<Hand>().HasMany(c => c.Cards);
            modelBuilder.Entity<Deck>().HasMany(c => c.Cards);
        }


        public DbSet<Deck> Decks { get; set; }
        public DbSet<GameDeck> GameDecks { get; set; }
        public DbSet<Hand> Hands { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
