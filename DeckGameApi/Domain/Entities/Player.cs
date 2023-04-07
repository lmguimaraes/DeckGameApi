using System.ComponentModel.DataAnnotations;

namespace DeckGameApi.Core.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
    }
}
