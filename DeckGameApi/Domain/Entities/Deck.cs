using DeckGameApi.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckGameApi.Domain.Entities
{
    public class Deck
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("GameDeck")]
        public int? GameDeckId { get; set; }
        public List<Card> Cards { get; set; }
        public Deck(int id)
        {
            Id = id;
            Cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (CardNumber cardNumber in Enum.GetValues(typeof(CardNumber)))
                    Cards.Add(new Card { CardNumber = cardNumber, Suit = suit });
            }
        }

        public void Shuffle()
        {
            Random r = new Random();
            List<Card> shuffledCards = new List<Card>();
            //Fisher-Yates shuffling algorythm
            for (int n = Cards.Count; n > 0; n--)
            {
                int k = r.Next(n);

                var temp = Cards[k];
                shuffledCards.Add(temp);

                Cards.RemoveAt(k);
            }
            Cards = shuffledCards;
        }

        public Card TakeCard()
        {
            var card = Cards.FirstOrDefault();
            Cards.Remove(card);

            return card;
        }

        public IEnumerable<Card> TakeCards(int numberOfCards)
        {
            var cards = Cards.Take(numberOfCards);

            var takeCards = cards as Card[] ?? cards.ToArray();
            Cards.RemoveAll(takeCards.Contains);

            return takeCards;
        }

        public Dictionary<Suit, int> CountCardsBySuite()
        {
            return Cards.GroupBy(c => c.Suit)
                         .ToDictionary(g => g.Key, g => g.Count());
        }

        public List<Card> SortBySuiteAndValue()
        {
            return Cards.OrderBy(c => c.Suit)
                        .ThenByDescending(c => c.CardNumber)
                        .ToList();
        }
    }
}

