using DeckGameApi.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckGameApi.Core.Entities
{
    [Keyless]
    [NotMapped]
    public class Card
    {
        public Suit Suit { get; set; }
        public CardNumber CardNumber { get; set; }
    }
}
