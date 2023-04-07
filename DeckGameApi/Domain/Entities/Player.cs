using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckGameApi.Core.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("GameDeck")]
        public int GameDeckId { get; set; }
        public List<Card> Cards { get; set; }
    }
}
