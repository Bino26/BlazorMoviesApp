using MoviesApp.Models;
using System.Net.Http.Json;

namespace MoviesApp.Services
{
    public class TMDBClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;

        public TMDBClient(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            this.config = config;

            httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

            string apiKey = config["TMDBKey"] ?? throw new Exception("TMDBKey not found");
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);

        }

        public Task<PopularMoviePagedResponse?> GetPopularMovieAsync()
        {

            //if (page < 1) page = 1;
            //if (page > 500) page = 500;

            return httpClient.GetFromJsonAsync<PopularMoviePagedResponse>("movie/popular");


        }

        public Task<MovieDetails?> GetMoviesDetailsAsync(int id)
        {
            return httpClient.GetFromJsonAsync<MovieDetails>($"movie/{id}");
        }
    }
}
