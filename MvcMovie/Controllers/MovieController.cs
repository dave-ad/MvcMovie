﻿namespace MvcMovie.Controllers;
//[Authorize]
public class MovieController : Controller
{
    private readonly IMovieService _movieService;
    private readonly IFileService _fileService;
    private readonly IGenreService _genreService;
    public MovieController(IMovieService movieService, IFileService fileService, IGenreService genreService)
    {
        _movieService = movieService;
        _fileService = fileService;
        _genreService = genreService;

    }

    public IActionResult Add()
    {
        var model = new Movie();
        model.GenreList = _genreService.List().Select(a => new SelectListItem {Text = a.GenreName, Value = a.Id.ToString()});
        return View(model);
    }

    [HttpPost]
    public IActionResult Add(Movie model)
    {
        model.GenreList = _genreService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString()});
        try
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    TempData["msg"] += $"{error.ErrorMessage} ";
                }
                return View(model);
            }

            if (model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File Could not save";
                    return View(model);
                }

                var imageName = fileResult.Item2;
                model.MovieImage = imageName;
            }

            var result = _movieService.Add(model);

            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        catch (Exception ex)
        {
            TempData["msg"] = $"Error on server side: {ex.Message}";
            return View(model);
        }
    }

    public IActionResult Edit(int id)
    {
        var model = _movieService.GetById(id);
        var selectedGenres = _movieService.GetGenreByMovieId(model.Id);
        MultiSelectList multiGenreList = new MultiSelectList(_genreService.List(), "Id", "GenreName", selectedGenres);
        model.MultiGenreList = multiGenreList;
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(Movie model)
    {
        var selectedGenres = _movieService.GetGenreByMovieId(model.Id);
        MultiSelectList multiGenreList = new MultiSelectList(_genreService.List(), "Id", "GenreName", selectedGenres);
        model.MultiGenreList = multiGenreList;
        if (!ModelState.IsValid)
            return View(model);
        if (model.ImageFile != null)
        {
            var fileResult = this._fileService.SaveImage(model.ImageFile);
            if (fileResult.Item1 == 0)
            {
                TempData["msg"] = "File Could not save";
                return View(model);
            }

            var imageName = fileResult.Item2;
            model.MovieImage = imageName;
        }
        var result = _movieService.Update(model);
        if (result)
        {
            TempData["msg"] = "Update Successful";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
            //return RedirectToAction(nameof(MovieList));
        }
        else
        {
            TempData["msg"] = "Error on server side";
            return View(model);
        }
    }

    public IActionResult MovieList()
    {
        var data = this._movieService.List();
        return View(data);
        //return Ok(data);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var result = _movieService.Delete(id);
        return RedirectToAction(nameof(MovieList));
    }
}
