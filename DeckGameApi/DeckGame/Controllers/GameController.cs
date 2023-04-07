using DeckGameApi.Common.Interfaces;
using DeckGameApi.Core.Entities;
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

        // DELETE /game/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _deckGameRepository.DeleteGame(id);
            return NoContent();
        }

        // POST /game/decks/create
        [HttpPost("decks/create")]
        public async Task<IActionResult> CreateDeck()
        {
            var entity = await _deckGameRepository.CreateDeck();
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
            if(entity == null)
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

        // POST /game/decks/add/{id}/{gameId}
        [HttpPost("decks/add/{deckId}/{gameId}")]
        public async Task<IActionResult> AddDeckToGame(int deckId, int gameId)
        {
            var entity = await _deckGameRepository.AddDeckToGame(deckId, gameId);
            return CreatedAtAction(nameof(GetGameDeck), new { id = gameId }, entity);
        }

        // POST /game/players/add/{gameId}
        [HttpPost("players/add/{gameId}")]
        public async Task<IActionResult> AddPlayerToGame(int playerId, int gameId)
        {
            var entity = await _deckGameRepository.AddPlayerToGame(gameId);
            return CreatedAtAction(nameof(GetGameDeck), new { id = gameId }, entity);
        }

        // POST /game/players/remove/{playerId}/{gameId}
        [HttpPost("players/remove/{playerId}/{gameId}")]
        public async Task<IActionResult> RemovePlayerFromGame(int playerId, int gameId)
        {
            var entity = await _deckGameRepository.RemovePlayerFromGame(playerId, gameId);
            return CreatedAtAction(nameof(GetGameDeck), new { id = gameId }, entity);
        }

        // POST /game/players/deal/{playerId}
        [HttpPost("players/deal/{gameDeckId}/{playerId}/{numberOfCards}")]
        public async Task<IActionResult> DealCardsToPlayer(int gameId, int playerId, int numberOfCards)
        {
            var entity = await _deckGameRepository.DealCardsToPlayer(gameId, playerId, numberOfCards);
            return CreatedAtAction(nameof(GetPlayer), new { id = playerId }, entity);
        }
    }
}