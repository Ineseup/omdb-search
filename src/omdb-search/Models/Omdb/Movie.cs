namespace omdb_search.Models.Omdb
{
    public class Movie
    {
        public string Title { get; set; } = String.Empty;
        public string Poster { get; set; } = String.Empty;
        public Dictionary<string, dynamic> Details { get; set; } = new Dictionary<string, dynamic>();
    }
}
