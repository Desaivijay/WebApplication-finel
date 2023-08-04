using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication_finel.Controllers
{
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var apiKey = _configuration["d1efcdf8"];
            var baseUrl = "http://www.omdbapi.com";
            var client = new RestClient(baseUrl);

            var request = new RestRequest(Method.GET);
            request.AddParameter("apikey", apiKey, ParameterType.QueryString);
            request.AddParameter("s", "Batman", ParameterType.QueryString); // Replace "Batman" with your search query

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var responseData = JsonConvert.DeserializeObject<OMDbApiResponse>(response.Content);

                if (responseData?.Search != null)
                {
                    var movies = responseData.Search
                        .Select(m => new Movie
                        {
                            Title = m.Title,
                            Year = m.Year,
                            PosterPath = m.Poster
                        })
                        .ToList();

                    return View(movies);
                }
            }

            // Handle error case or return an empty list of movies
            return View(new List<Movie>());
        }
    }
}
