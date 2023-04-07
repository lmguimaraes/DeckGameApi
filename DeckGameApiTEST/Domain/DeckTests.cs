using DeckGameApi.Domain.Entities;
using DeckGameApi.Domain.Entities.Enums;

namespace DeckGameApiTEST.Domain
{
    public class DeckTests
    {

        [Fact]
        public void TestDeckConstructor()
        {
            // Arrange
            int deckId = 1;

            // Act
            var deck = new Deck(deckId);

            // Assert
            Assert.NotNull(deck.Cards);
            Assert.Equal(52, deck.Cards.Count);
            Assert.Equal(deckId, deck.Id);
        }

        [Fact]
        public void TestShuffle()
        {
            // Arrange
            var deck = new Deck(1);

            // Act
            deck.Shuffle();

            // Assert
            Assert.NotNull(deck.Cards);
            Assert.Equal(52, deck.Cards.Count);

            // The chances of the deck being in the same order after shuffling are extremely low.
            // Here we're testing if at least one card is different.
            Assert.NotEqual(deck.Cards[0], new Card { CardNumber = CardNumber.Ace, Suit = Suit.Clubs });
        }

        [Fact]
        public void TestTakeCard()
        {
            // Arrange
            var deck = new Deck(1);
            var expectedCard = new Card { CardNumber = CardNumber.Ace, Suit = Suit.Clubs };

            // Act
            var card = deck.TakeCard();

            // Assert
            Assert.NotNull(card);
            Assert.Equal(51, deck.Cards.Count);
            Assert.DoesNotContain(card, deck.Cards);
        }

        [Fact]
        public void TestTakeCards()
        {
            // Arrange
            var deck = new Deck(1);

            // Act
            var cards = deck.TakeCards(2);

            // Assert
            Assert.NotNull(cards);
            Assert.Equal(50, deck.Cards.Count);
            foreach (var card in cards)
            {
                Assert.DoesNotContain(card, deck.Cards);
            }
        }

        [Fact]
        public void TestCountCardsBySuite()
        {
            // Arrange
            var deck = new Deck(1);
            var expectedCount = new Dictionary<Suit, int>
            {
                { Suit.Clubs, 13 },
                { Suit.Diamonds, 13 },
                { Suit.Hearts, 13 },
                { Suit.Spades, 13 }
            };

            // Act
            var countBySuite = deck.CountCardsBySuite();

            // Assert
            Assert.NotNull(countBySuite);
            Assert.Equal(expectedCount.Count, countBySuite.Count);
            foreach (var kvp in expectedCount)
            {
                Assert.Equal(kvp.Value, countBySuite[kvp.Key]);
            }
        }
    }
}