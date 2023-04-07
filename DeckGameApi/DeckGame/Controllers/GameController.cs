using DeckGameApi.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        // GET /game/helloworld
        [HttpGet("helloworld")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello World");
        }
        // GET /game/decks
        [HttpGet("decks")]
        public IActionResult Get()
        {
            return Ok(_deckGameRepository.GetDecks());
        }
    }
}