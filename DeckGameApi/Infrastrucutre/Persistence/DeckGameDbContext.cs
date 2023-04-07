using DeckGameApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeckGameApi.Infrastrucutre.Persistence
{
    public class DeckGameDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "GameDeckDb");
        }
        public DbSet<Deck> Decks { get; set; }
        //public DbSet<GameDeck> GameDecks { get; set; }
    }
}
