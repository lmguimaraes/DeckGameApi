using DeckGameApi.Core.Entities;

namespace DeckGameApi.Common.Interfaces
{
    public interface IDeckGameRepository
    {
        public List<Deck> GetDecks();
        public void CreateDeck();
        public void CreateGame();
        public void DeleteGame();
        public void AddDeckToGame();
    }
}
