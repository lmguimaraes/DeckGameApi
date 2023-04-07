using DeckGameApi.Domain.Entities;

namespace DeckGameApi.DeckGame.DTO
{
    public class CardCollectionDto
    {     
        public List<SuitDTO> Suits { get; set; }

        public CardCollectionDto(List<Card> cards)
        {
            Suits = new List<SuitDTO>();

            var suitGroups = cards.GroupBy(c => c.Suit);

            foreach (var group in suitGroups)
            {
                var suitDto = new SuitDTO
                {
                    Name = group.Key.ToString(),
                    Count = group.Count(),
                    Cards = group.OrderByDescending(c => c.CardNumber)
                                .Select(c => new CardDTO
                                {
                                    Name = c.CardNumber.ToString(),
                                    Value = (int)c.CardNumber
                                })
                                .ToList()
                };

                Suits.Add(suitDto);
            }
        }
    }

    public class CardDTO
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class SuitDTO
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public List<CardDTO> Cards { get; set; }
    }
}
