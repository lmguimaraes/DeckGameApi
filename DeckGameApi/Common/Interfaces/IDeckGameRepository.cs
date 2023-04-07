using DeckGameApi.Core.Entities;

namespace DeckGameApi.Common.Interfaces
{
    public interface IDeckGameRepository
    {
        public Task<Deck> CreateDeck();
        public Task<GameDeck> CreateGame();
        public Task<Player> CreatePlayer();
        public Task DeleteGame(int id);
        public Task<GameDeck> GetGameDeck(int id);
        public Task<Deck> GetDeck(int id);
        public Task<Player> GetPlayer(int id);
        public Task<GameDeck> AddDeckToGame(int deckId, int gameDeckId);
        public Task<GameDeck> AddPlayerToGame(int playerId, int gameDeckId);
        public Task<GameDeck> RemovePlayerFromGame(int playerId, int gameDeckId);
        public Task<Player> DealCardsToPlayer(int gameDeckId, int playerId, int numberOfCards);
    }
}
