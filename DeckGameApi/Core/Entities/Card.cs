using DeckGameApi.Core.Entities.Enums;

namespace DeckGameApi.Core.Entities
{
    public class Card
    {
        public Suit Suit { get; set; }
        public CardNumber CardNumber { get; set; }
    }
}
