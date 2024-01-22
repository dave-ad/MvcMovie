namespace MvcMovie.Repositories.Implementation;
public class MovieService : IMovieService
{
    private readonly DatabaseContext _databaseContext;
    public MovieService(DatabaseContext databaseContext)
    {
        this._databaseContext = databaseContext;
    }
    public bool Add(Movie model)
    {
        try
        {
            _databaseContext.Movie.Add(model);
            _databaseContext.SaveChanges();
            foreach (var genreId in model.Genres)
            {
                var movieGenre = new MovieGenre
                {
                    MovieId = model.Id,
                    GenreId = genreId
                };
                _databaseContext.MovieGenre.Add(movieGenre);
            }
            _databaseContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var data = this.GetById(id);
            if (data == null)
                return false;
            var movieGenres = _databaseContext.MovieGenre.Where(x => x.MovieId == data.Id);
            foreach (var movieGenre in movieGenres)
            {
                _databaseContext.MovieGenre.Remove(movieGenre);
            }
            _databaseContext.Movie.Remove(data);
            _databaseContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Movie GetById(int id)
    {
        return _databaseContext.Movie.Find(id);
    }

    public MovieListModel List(string term="", bool paging = false, int currentPage = 0)
    {
        var data = new MovieListModel();
        var list = _databaseContext.Movie.ToList();
        if (!string.IsNullOrEmpty(term))
        {
            term = term.ToLower();
            list = list.Where(x => x.Title.ToLower().StartsWith(term)).ToList();
        }
        if (paging)
        {
            // Apply Paging
            int pageSize = 5;
            int count = list.Count;
            int totalPages = (int)Math.Ceiling(count / (double) pageSize);
            list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            data.PageSize = pageSize;
            data.CurrentPage = currentPage;
            data.TotalPages = totalPages;
        }
        foreach (var movie in list)
        {
            var genres = (from genre in _databaseContext.Genre 
                          join mg in _databaseContext.MovieGenre
                          on genre.Id equals mg.GenreId
                          where mg.MovieId == movie.Id
                          select genre.GenreName).ToList();
            var genreNames = string.Join(',', genres);
            movie.GenreNames = genreNames;
        }
        data.MovieList = list.AsQueryable();
        return data;
    }

    public bool Update(Movie model)
    {
        try
        {
            // These genreIds are not selected by the user and is still present in the movieGenre table corresponding to this movieId.
            // So these Ids should be removed.
            var genresToDelete = _databaseContext.MovieGenre.Where(x => x.MovieId == model.Id && !model.Genres.Contains(x.GenreId)).ToList();
            foreach (var movGenre in genresToDelete)
            {
                //var genre = _databaseContext.MovieGenre.Find(genreId);
                _databaseContext.MovieGenre.Remove(movGenre);
            }
            foreach (int genId in model.Genres)
            {
                var movieGenre = _databaseContext.MovieGenre.FirstOrDefault(x => x.MovieId == model.Id && x.GenreId == genId);
                if (movieGenre == null)
                {
                    movieGenre = new MovieGenre { GenreId = genId, MovieId = model.Id };
                    _databaseContext.MovieGenre.Add(movieGenre);
                }
            }
            _databaseContext.Movie.Update(model);
            // We have to add these genre ids in the movieGenre table
            _databaseContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<int> GetGenreByMovieId(int movieId)
    {
        var genreIds = _databaseContext.MovieGenre.Where(x => x.MovieId == movieId).Select(y => y.GenreId).ToList();
        return genreIds;
    }
}