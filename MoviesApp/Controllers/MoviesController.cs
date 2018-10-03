using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MoviesApp.Models;
using NoDb;

namespace MoviesApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Movies")]
    public class MoviesController : Controller
    {
        private IBasicCommands<Movie> _movieCommands;
        private IBasicQueries<Movie> _movieQueries;
        private IMemoryCache _cache;
        private const string projectId = "movie-app";
        private const string moviesCacheKey = "all-movies";

        public MoviesController(IBasicCommands<Movie> movieCommands, IBasicQueries<Movie> movieQueries, IMemoryCache memoryCache)
        {
            _movieCommands = movieCommands;
            _movieQueries = movieQueries;
            _cache = memoryCache;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            var movies = await GetAllMovies();

            return movies.OrderBy(m => m.Title);
        }

        private async Task<IEnumerable<Movie>> GetAllMovies()
        {
            IEnumerable<Movie> movies;
            if (_cache.TryGetValue(moviesCacheKey, out movies))
            {
                return movies;
            }

            movies = await _movieQueries.GetAllAsync(projectId).ConfigureAwait(false);

            _cache.Set(moviesCacheKey, movies);
            return movies;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movies = await GetAllMovies();
            var movie = movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie([FromRoute] int id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            await _movieCommands.UpdateAsync(projectId, id.ToString(), movie).ConfigureAwait(false);
            _cache.Remove(moviesCacheKey);

            return NoContent();
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<IActionResult> PostMovie([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            movie.Id = await GetNextIDAsync();

            await _movieCommands.CreateAsync(projectId, movie.Id.ToString(), movie).ConfigureAwait(false);
            _cache.Remove(moviesCacheKey);

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movies = await GetAllMovies();
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            await _movieCommands.DeleteAsync(projectId, id.ToString()).ConfigureAwait(false);
            _cache.Remove(moviesCacheKey);

            return Ok(movie);
        }

        private async Task<int> GetNextIDAsync()
        {
            var movies = await _movieQueries.GetAllAsync(projectId).ConfigureAwait(false);
            if (movies.Any())
            {
                return movies.Max(m => m.Id) + 1;
            }
            return 1;
        }
    }
}
