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
        catch (Exception)
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
        var list = _databaseContext.Movie.AsQueryable();
        var data = new MovieListModel
        {
            MovieList = list
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
}