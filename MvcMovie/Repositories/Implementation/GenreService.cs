namespace MvcMovie.Repositories.Implementation;

public class GenreService : IGenreService
{
    private readonly DatabaseContext _databaseContext;
    public GenreService(DatabaseContext databaseContext)
    {
        this._databaseContext = databaseContext;
    }

    public bool Add(Genre model)
    {
        try
        {
            _databaseContext.Genre.Add(model);
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
            _databaseContext.Genre.Remove(data);
            _databaseContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Genre GetById(int id)
    {
        return _databaseContext.Genre.Find(id);
    }

    public IQueryable<Genre> List()
    {
        var data = _databaseContext.Genre.AsQueryable();
        return data;
    }

    public bool Update(Genre model)
    {
        try
        {
            _databaseContext.Genre.Update(model);
            _databaseContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
