using DeckGameApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckGameApi.Domain.Entities
{
    public class Hand
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public List<Card> Cards { get; set; }
    }
}
