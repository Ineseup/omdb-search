using Microsoft.AspNetCore.Mvc;
using omdb_search.Models.Omdb;
using omdb_search.Services.Omdb;

namespace omdb_search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OmdbController : ControllerBase
    {
        private readonly ILogger<OmdbController> logger;
        private readonly IOmdbService omdbService;
        public OmdbController(
            ILogger<OmdbController> logger,
            IOmdbService omdbService
        )
        {
            this.logger = logger;
            this.omdbService = omdbService;
        }

        [HttpGet("name")]
        public async Task<string> GetMoviesByName(string name)
        {
            var request = HttpContext.Request;
            logger.LogTrace($"{request.Method} {request.Path} - {name}");
            return await omdbService.SearchByName(name);
        }

        [HttpGet("id")]
        public async Task<Movie> GetMovieDetailsById(string id)
        {
            var request = HttpContext.Request;
            logger.LogTrace($"{request.Method} {request.Path} - {id}");
            return await omdbService.SearchById(id);
        }
    }
}