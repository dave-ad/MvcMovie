namespace MvcMovie.Repositories.Abstract;
public interface IMovieService
{
    bool Add(Movie model);
    bool Update(Movie model);
    Movie GetById(int id);
    bool Delete(int id);
    MovieListModel List();
    List<int> GetGenreByMovieId(int movieId);
}
