using System.ComponentModel.DataAnnotations;

namespace DeckGameApi.Core.Entities
{
    public class GameDeck
    {
        [Key]
        public int Id { get; set; }
        public List<Player> Players { get; set; }
        public List<Deck> Decks { get; set; }

    }
}
