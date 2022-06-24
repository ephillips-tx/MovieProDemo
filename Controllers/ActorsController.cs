using Microsoft.AspNetCore.Mvc;
using MovieProDemo.Services.Interfaces;

namespace MovieProDemo.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IRemoteMovieService _tmdbMovieService;
        private readonly IDataMappingService _mappingService;

        public ActorsController(IRemoteMovieService tmdbMovieService,
                                IDataMappingService mappingService)
        {
            _tmdbMovieService = tmdbMovieService;
            _mappingService = mappingService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await _tmdbMovieService.ActorDetailAsync(id);
            actor = _mappingService.MapActorDetail(actor);
            ViewData["HeaderImage"] = "/img/shannia-christanty-VLcR2YhFHN8-unsplash.jpg";

            return View(actor);
        }
    }
}
