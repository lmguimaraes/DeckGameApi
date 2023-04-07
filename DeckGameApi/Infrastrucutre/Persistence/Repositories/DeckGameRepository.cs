using DeckGameApi.Common.Interfaces;
using DeckGameApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace DeckGameApi.Infrastrucutre.Persistence.Repositories
{
    public class DeckGameRepository : IDeckGameRepository
    {
        private static int DeckId = 1;
        private readonly DeckGameDbContext _context;
        private readonly ILogger<DeckGameRepository> _logger;
        public DeckGameRepository(DeckGameDbContext context,
                                  ILogger<DeckGameRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<Deck> CreateDeck()
        {
            var deck = new Deck(DeckId);
            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            DeckId++;
            return deck;
        }

        public async Task<Deck> ShuffleGameDeck(int deckId, int gameId)
        {
            var deck = await GetDeck(deckId);
            if (deck is null)
            {
                _logger.LogError("GameDeck not found");
                return null;
            }
            deck.Shuffle();
            await _context.SaveChangesAsync();

            return deck;
        }

        public async Task<GameDeck> CreateGame()
        {
            var gameDeck = new GameDeck();
            await _context.GameDecks.AddAsync(gameDeck);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Game created with ID: {id}", gameDeck.Id);
            return gameDeck;
        }

        public async Task<Player> CreatePlayer()
        {
            var player = new Player();
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Player created with ID: {id}", player.Id);
            return player;
        }

        public async Task DeleteGame(int id)
        {
            try
            {
                var gameDeck = _context.GameDecks.Attach(new GameDeck { Id = id });
                gameDeck.State = EntityState.Deleted;
                _logger.LogInformation("Game deleted with ID: {id}", gameDeck.Entity.Id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e.Message);
            }

        }

        public async Task<GameDeck> AddDeckToGame(int deckId, int gameDeckId)
        {
            var gameDeck = await GetGameDeck(gameDeckId);
            if (gameDeck == null)
            {
                _logger.LogError("GameDeck not found");
                return null;
            }

            var deck = await GetDeck(deckId);

            if (deck == null)
            {
                _logger.LogError("Deck not found");
                return null;
            }

            gameDeck.Decks.Add(deck);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deck {deckId} added to game {id}", deck.Id, gameDeck.Id);
            return gameDeck;
        }

        public async Task<GameDeck> AddPlayerToGame(int playerId, int gameDeckId)
        {

            var gameDeck = await GetGameDeck(gameDeckId);
            if (gameDeck == null)
            {
                _logger.LogError("GameDeck not found");
                return null;
            }

            var player = await GetPlayer(playerId);
            if (player is null)
            {
                _logger.LogError("Player not found");
                return null;
            }

            gameDeck.Players.Add(player);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Player {playerId} added to game {gameDeckId}", player.Id, gameDeck.Id);

            return gameDeck;

        }

        public async Task<GameDeck> RemovePlayerFromGame(int playerId, int gameDeckId)
        {

            var gameDeck = await GetGameDeck(gameDeckId);
            if (gameDeck == null)
            {
                _logger.LogError("GameDeck not found");
                return null;
            }
            if (gameDeck.Players.FirstOrDefault(x => x.Id == playerId) == null)
            {
                _logger.LogError("Player not found");
                return null;
            }
            gameDeck.Players.Remove(gameDeck.Players.First(y => y.Id == playerId));
            await _context.SaveChangesAsync();
            _logger.LogInformation("Player {playerId} removed to game {gameDeckId}", playerId, gameDeck.Id);

            return gameDeck;
        }

        public async Task<Player> DealCardsToPlayer(int gameDeckId, int playerId, int numberOfCards)
        {

            var gameDeck = await GetGameDeck(gameDeckId);
            if (gameDeck is null)
            {
                _logger.LogError("GameDeck not found");
                return null;
            }
            var player = gameDeck.Players.FirstOrDefault(x => x.Id == playerId);
            if (player is null)
            {
                _logger.LogError("Player not found");
                return null;
            }
            if (gameDeck.Decks is null || gameDeck.Decks.Count == 0)
            {
                _logger.LogError("No deck added to the this game");
                return null;
            }
            gameDeck.GiveCardsToPlayer(player, numberOfCards);  
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<GameDeck> GetGameDeck(int id)
        {
            return await _context.GameDecks.Include(d => d.Decks)
                                           .ThenInclude(c => c.Cards)
                                           .Include(p => p.Players)
                                           .ThenInclude(p => p.Hand)
                                           .ThenInclude(h => h.Cards)
                                           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Deck> GetDeck(int id)
        {
            return await _context.Decks.Include(c => c.Cards)
                                       .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Player> GetPlayer(int id)
        {


            return await _context.Players.Include(h => h.Hand)
                                         .ThenInclude(c => c.Cards)
                                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Card>> GetPlayerCards(int id)
        {
            var player = await GetPlayer(id);

            if (player == null)
            {
                _logger.LogError("Player not found");
                return null;
            }

            return player.Hand.Cards;
        }
    }
}
