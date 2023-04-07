using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckGameApi.Domain.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("GameDeck")]
        public int GameDeckId { get; set; }
        public Hand Hand { get; set; }
    }
}
