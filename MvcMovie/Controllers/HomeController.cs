namespace MvcMovie.Controllers;

public class HomeController : Controller
{
    private readonly IMovieService _movieService;

    public HomeController(IMovieService movieService)
    {
            _movieService = movieService;
    }

    public IActionResult Index(string term="", int currentPage = 1)
    {
        var movies = _movieService.List(term, true, currentPage);
        return View(movies);
    }
    
    public IActionResult About()
    {
        return View();
    }
    
    public IActionResult MovieDetails(int movieId)
    {
        var movie = _movieService.GetById(movieId);
        return View(movie);
    }
}
