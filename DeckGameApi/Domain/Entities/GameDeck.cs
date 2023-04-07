using DeckGameApi.DeckGame.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace DeckGameApi.Domain.Entities
{
    public class GameDeck
    {
        [Key]
        public int Id { get; set; }
        public List<Player> Players { get; set; }
        public List<Deck> Decks { get; set; }

        public List<PlayerHandValueDto> GetPlayersHandValues()
        {
            return Players.Select(p => new PlayerHandValueDto
            {
                PlayerId = p.Id,
                HandValue = p.Hand.Cards.Sum(c => (int)c.CardNumber)
            })
            .OrderByDescending(p => p.HandValue)
            .ToList();
        }

        public void GiveCardsToPlayer(Player player, int numberOfCards)
        {
            int cardsTaken = 0;
            foreach (var deck in Decks)
            {
                if (deck.Cards.Count > 0)
                {
                    deck.Shuffle();
                    var cardsToTake = Math.Min(numberOfCards - cardsTaken, deck.Cards.Count);
                    if (player.Hand is null)
                    {
                        player.Hand = new Hand();
                        player.Hand.Cards = new List<Card>();
                    }
                    player.Hand.Cards.AddRange(deck.TakeCards(cardsToTake));

                    cardsTaken += cardsToTake;

                    if (cardsTaken == numberOfCards)
                    {
                        break;
                    }
                }
            }
        }

    }
}
