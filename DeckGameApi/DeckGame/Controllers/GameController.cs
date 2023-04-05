using Microsoft.AspNetCore.Mvc;

namespace DeckGameApi.DeckGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        // GET /game/helloworld
        [HttpGet("helloworld")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello World");
        }
    }
}