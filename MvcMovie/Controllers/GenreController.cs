namespace MvcMovie.Controllers;

//[Authorize(Roles ="admin")]
[Authorize]
public class GenreController : Controller
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {

        this._genreService = genreService;

    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([Bind("GenreName")] Genre model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = _genreService.Add(model);
        //TempData["msg"] = result? "Successfully Added": "Could not save...";

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

    public IActionResult Edit(int id)
    {
        var data = _genreService.GetById(id);
        return View(data);
    }

    [HttpPost]
    public IActionResult Update(Genre model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = _genreService.Update(model);

        if (result)
        {
            TempData["msg"] = "Update Successful";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
            //return RedirectToAction(nameof(GenreList));
        }
        else
        {
            TempData["msg"] = "Error on server side";
            return View(model);
        }
    }

    public IActionResult GenreList()
    {
        var data = this._genreService.List().ToList();
        return View(data);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var result = _genreService.Delete(id);
        return RedirectToAction(nameof(GenreList));
    }
}
