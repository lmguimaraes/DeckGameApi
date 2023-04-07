using DeckGameApi.Domain.Entities.Enums;
using DeckGameApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckGameApiTEST.Domain
{
    public class GameDeckTests
    {
        [Fact]
        public void GetPlayersHandValues_ReturnsCorrectHandValues()
        {
            // Arrange
            var gameDeck = new GameDeck
            {
                Players = new List<Player>
                {
                    new Player { Id = 1, Hand = new Hand { Cards = new List<Card>
                    {
                        new Card { Suit = Suit.Clubs, CardNumber = CardNumber.Ace },
                        new Card { Suit = Suit.Diamonds, CardNumber = CardNumber.Two },
                        new Card { Suit = Suit.Hearts, CardNumber = CardNumber.Five }
                    } } },
                    new Player { Id = 2, Hand = new Hand { Cards = new List<Card>
                    {
                        new Card { Suit = Suit.Clubs, CardNumber = CardNumber.King },
                        new Card { Suit = Suit.Diamonds, CardNumber = CardNumber.Queen },
                        new Card { Suit = Suit.Spades, CardNumber = CardNumber.Jack }
                    } } }
                }
            };

            // Act
            var handValues = gameDeck.GetPlayersHandValues();

            // Assert
            Assert.Equal(2, handValues.Count);
            Assert.Equal(36, handValues[0].HandValue); // Sum of card values in Player 1's hand
            Assert.Equal(8, handValues[1].HandValue); // Sum of card values in Player 2's hand
        }

        [Fact]
        public void GiveCardsToPlayer_AddsCorrectNumberOfCardsToPlayerHand()
        {
            // Arrange
            var gameDeck = new GameDeck
            {
                Players = new List<Player> { new Player { Id = 1 } },
                Decks = new List<Deck>
                {
                    new Deck(1) { Cards = new List<Card>
                    {
                        new Card { Suit = Suit.Clubs, CardNumber = CardNumber.Ace },
                        new Card { Suit = Suit.Diamonds, CardNumber = CardNumber.Two },
                        new Card { Suit = Suit.Hearts, CardNumber = CardNumber.Five },
                        new Card { Suit = Suit.Spades, CardNumber = CardNumber.Ten }
                    } }
                }
            };
            var player = gameDeck.Players.First();

            // Act
            gameDeck.GiveCardsToPlayer(player, 2);

            // Assert
            Assert.Equal(2, player.Hand.Cards.Count);
        }

        [Fact]
        public void GiveCardsToPlayer_DistributesCardsFromMultipleDecks()
        {
            // Arrange
            var gameDeck = new GameDeck
            {
                Id = 1,
                Players = new List<Player>(),
                Decks = new List<Deck>
        {
            new Deck(1),
            new Deck(2),
            new Deck(3)
        }
            };

            var player = new Player
            {
                Id = 1,
                Hand = null
            };

            gameDeck.Players.Add(player);

            // Ensure there are cards in each deck
            foreach (var deck in gameDeck.Decks)
            {
                deck.Shuffle();
            }

            // Act
            gameDeck.GiveCardsToPlayer(player, 7);

            // Assert
            // Check that the player's hand contains 7 cards
            Assert.Equal(7, player.Hand.Cards.Count);

            // Check that cards were taken from each deck
            Assert.NotEmpty(gameDeck.Decks[0].Cards);
            Assert.NotEmpty(gameDeck.Decks[1].Cards);
            Assert.NotEmpty(gameDeck.Decks[2].Cards);

            // Check that no deck has more than 52 cards
            foreach (var deck in gameDeck.Decks)
            {
                Assert.InRange(deck.Cards.Count, 0, 52);
            }

            // Check that the correct number of cards were taken from each deck
            int totalCardsTaken = player.Hand.Cards.Count;
            int cardsTakenFromDeck1 = player.Hand.Cards.Count(c => c.Suit == Suit.Clubs && c.CardNumber == CardNumber.Two);
            int cardsTakenFromDeck2 = player.Hand.Cards.Count(c => c.Suit == Suit.Diamonds && c.CardNumber == CardNumber.Three);
            int cardsTakenFromDeck3 = totalCardsTaken - cardsTakenFromDeck1 - cardsTakenFromDeck2;

            Assert.True(cardsTakenFromDeck1 > 0);
            Assert.True(cardsTakenFromDeck2 > 0);
            Assert.True(cardsTakenFromDeck3 > 0);
            Assert.Equal(cardsTakenFromDeck1 + cardsTakenFromDeck2 + cardsTakenFromDeck3, totalCardsTaken);
        }
    }
}
