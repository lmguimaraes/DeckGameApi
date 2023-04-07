using DeckGameApi.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeckGameApi.Domain.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public Suit Suit { get; set; }
        public CardNumber CardNumber { get; set; }
    }
}

