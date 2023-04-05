using DeckGameApi.Core.Entities.Enums;

namespace DeckGameApi.Core.Entities
{
    public class Deck
    {
        public Deck() => Reset();

        public List<Card> Cards { get; set; }

        public void Reset()
        {
            Cards = Enumerable.Range(1, 4)
                .SelectMany(s => Enumerable.Range(1, 13)
                                    .Select(c => new Card()
                                    {
                                        Suit = (Suit)s,
                                        CardNumber = (CardNumber)c
                                    }))
                .ToList();
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
    }
}

