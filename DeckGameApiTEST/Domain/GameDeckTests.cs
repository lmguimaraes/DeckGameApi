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
    }
}
