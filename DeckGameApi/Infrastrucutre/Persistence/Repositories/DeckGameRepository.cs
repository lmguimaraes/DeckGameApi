using DeckGameApi.Common.Interfaces;
using DeckGameApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeckGameApi.Infrastrucutre.Persistence.Repositories
{
    public class DeckGameRepository : IDeckGameRepository
    {
        public DeckGameRepository() 
        { 
            using (var context = new DeckGameDbContext()) 
            {
                var decksSeed = Enumerable.Range(1, 2).Select(_ => {
                                                var deck = new Deck();
                                                deck.Reset();
                                                deck.Shuffle();
                                                return deck;
                                                }).ToList();
                context.Decks.AddRange(decksSeed);
                context.SaveChanges();
            }
        }
        public void AddDeckToGame()
        {
            throw new NotImplementedException();
        }

        public void CreateDeck()
        {
            throw new NotImplementedException();
        }

        public void CreateGame()
        {
            throw new NotImplementedException();
        }

        public void DeleteGame()
        {
            throw new NotImplementedException();
        }
        //Used to test the in memory database
        public List<Deck> GetDecks()
        {
            using (var context = new DeckGameDbContext())
            {
                {
                    return context.Decks
                        .ToList();
                }
            }
        }
       

    }
}
