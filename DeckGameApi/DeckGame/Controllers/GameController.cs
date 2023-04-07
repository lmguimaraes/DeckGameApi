using DeckGameApi.Common.Interfaces;
using DeckGameApi.DeckGame.DTO;
using DeckGameApi.Domain.Entities;
using DeckGameApi.Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeckGameApi.DeckGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IDeckGameRepository _deckGameRepository;

        public GameController(ILogger<GameController> logger,
                              IDeckGameRepository deckGameRepository)
        {
            _logger = logger;
            _deckGameRepository = deckGameRepository;
        }

        // POST /game/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateGame()
        {
            var entity = await _deckGameRepository.CreateGame();
            return CreatedAtAction(nameof(GetGameDeck), new { id = entity.Id }, entity);
        }

        // POST /game/decks/create
        [HttpPost("decks/create")]
        public async Task<IActionResult> CreateDeck()
        {
            var entity = await _deckGameRepository.CreateDeck();
            return CreatedAtAction(nameof(GetDeck), new { id = entity.Id }, entity);
        }

        // POST /game/decks/shuffle/{id}/{gameId}
        [HttpPost("decks/shuffle/{deckId}/{gameId}")]
        public async Task<IActionResult> ShuffleGameDeck(int deckId, int gameId)
        {
            var entity = await _deckGameRepository.ShuffleGameDeck(deckId, gameId);
            return CreatedAtAction(nameof(GetDeck), new { id = entity.Id }, entity);
        }

        // POST /game/players/create
        [HttpPost("players/create")]
        public async Task<IActionResult> CreatePlayer()
        {
            var entity = await _deckGameRepository.CreatePlayer();
            return CreatedAtAction(nameof(GetDeck), new { id = entity.Id }, entity);
        }

        // GET /game/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GameDeck>> GetGameDeck(int id)
        {
            var entity = await _deckGameRepository.GetGameDeck(id);
            if (entity == null)
                return BadRequest("Invalid ID");
            return Ok(entity);
        }

        // GET /game/decks/{id}
        [HttpGet]
        [Route("decks/{id}")]
        public async Task<ActionResult<GameDeck>> GetDeck(int id)
        {
            var entity = await _deckGameRepository.GetDeck(id);
            if (entity == null)
                return BadRequest("Invalid ID");
            return Ok(entity);
        }

        // GET /game/players/{id}
        [HttpGet]
        [Route("players/{id}")]
        public async Task<ActionResult<GameDeck>> GetPlayer(int id)
        {
            var entity = await _deckGameRepository.GetPlayer(id);
            if (entity == null)
                return BadRequest("Invalid ID");
            return Ok(entity);
        }

        // GET /game/players/cards/{id}
        [HttpGet]
        [Route("players/cards/{id}")]
        public async Task<ActionResult<GameDeck>> GetPlayerCards(int id)
        {
            var entity = await _deckGameRepository.GetPlayerCards(id);
            if (entity == null)
                return BadRequest("Invalid ID");
            return Ok(entity);
        }

        //GET /game/{gameId}/players/sorted-by-hand-value"
        [HttpGet]
        [Route("{gameId}/players/sorted-by-hand-value")]
        public ActionResult<List<PlayerHandValueDto>> GetPlayersSortedByHandValue(int gameId)
        {
            var gameDeck = _deckGameRepository.GetGameDeck(gameId);
            if (gameDeck == null)
            {
                return NotFound();
            }

            return Ok(gameDeck.Result.GetPlayersHandValues());
        }

        //GET /game/{gameId}/{deckId}/cards/count-by-suit"
        [HttpGet]
        [Route("game/{gameId}/{deckId}/cards/count-by-suit")]
        public ActionResult<Dictionary<Suit, int>> GetCardsCountBySuit(int gameId, int deckId)
        {
            var gameDeck = _deckGameRepository.GetGameDeck(gameId);
            if (gameDeck is null)
            {
                return NotFound();
            }
            var deck = gameDeck.Result.Decks.First(x => x.Id == deckId);
            if (deck is null)
                return NotFound();
            return Ok(deck.CountCardsBySuite());
        }
        //GET /game/{gameId}/{deckId}/cards/count-by-suit-sorted-by-value"
        [HttpGet]
        [Route("game/{gameId}/{deckId}/cards/count-by-suit-sorted-by-value")]
        public ActionResult<CardCollectionDto> GetCardsCountBySuitSortedByValue(int gameId, int deckId)
        {
            var gameDeck = _deckGameRepository.GetGameDeck(gameId);
            if (gameDeck is null)
            {
                return NotFound();
            }
            var deck = gameDeck.Result.Decks.First(x => x.Id == deckId);
            if (deck is null)
                return NotFound();

            return Ok(new CardCollectionDto(deck.Cards));

        }

        // POST /game/decks/add/{deckId}/{gameId}
        [HttpPost("decks/add/{deckId}/{gameId}")]
        public async Task<IActionResult> AddDeckToGame(int deckId, int gameId)
        {
            var entity = await _deckGameRepository.AddDeckToGame(deckId, gameId);
            return CreatedAtAction(nameof(GetGameDeck), new { id = gameId }, entity);
        }

        // POST /game/players/add/{playerId}/{gameId}
        [HttpPost("players/add/{playerId}/{gameId}")]
        public async Task<IActionResult> AddPlayerToGame(int playerId, int gameId)
        {
            var entity = await _deckGameRepository.AddPlayerToGame(playerId, gameId);
            return CreatedAtAction(nameof(GetGameDeck), new { id = gameId }, entity);
        }

        // POST /game/players/remove/{playerId}/{gameId}
        [HttpPost("players/remove/{playerId}/{gameId}")]
        public async Task<IActionResult> RemovePlayerFromGame(int playerId, int gameId)
        {
            var entity = await _deckGameRepository.RemovePlayerFromGame(playerId, gameId);
            return CreatedAtAction(nameof(GetGameDeck), new { id = gameId }, entity);
        }

        // POST /game/players/deal/{playerId/{playerId}/{numberOfCards}
        [HttpPost("players/deal/{gameId}/{playerId}/{numberOfCards}")]
        public async Task<IActionResult> DealCardsToPlayer(int gameId, int playerId, int numberOfCards)
        {
            var entity = await _deckGameRepository.DealCardsToPlayer(gameId, playerId, numberOfCards);
            return CreatedAtAction(nameof(GetPlayer), new { id = playerId }, entity);
        }

        // DELETE /game/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _deckGameRepository.DeleteGame(id);
            return NoContent();
        }

    }
}