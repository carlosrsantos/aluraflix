using System.Linq.Expressions;

namespace AluraFlix.Repositories;
public interface IRepository<T>
{
  IReadOnlyList<T> GetAll(int page, int pageSize);
  decimal? Total();   
  T? GetById(int id);
  T Post(T entity);
  void Update(T entity);
  void Delete(T entity);
}