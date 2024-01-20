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

    public MovieListModel List()
    {
        var list = _databaseContext.Movie.ToList();
        foreach(var movie in list)
        {
            var genres = (from genre in _databaseContext.Genre 
                          join mg in _databaseContext.MovieGenre
                          on genre.Id equals mg.GenreId
                          where mg.MovieId == movie.Id
                          select genre.GenreName).ToList();
            var genreNames = string.Join(',', genres);
            movie.GenreNames = genreNames;
        }
        var data = new MovieListModel
        {
            MovieList = list.AsQueryable()
        };
        return data;
    }

    public bool Update(Movie model)
    {
        try
        {
            _databaseContext.Movie.Update(model);
            _databaseContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
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