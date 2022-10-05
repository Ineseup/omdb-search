using omdb_search.Models.Omdb;

namespace omdb_search.Services.Omdb
{
    public interface IOmdbService
    {
        public Task<Movie> SearchById(string name);
        public Task<string> SearchByName(string name);
    }
}
