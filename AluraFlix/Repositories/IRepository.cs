using System.Linq.Expressions;

namespace AluraFlix.Repositories;
public interface IRepository<T>
{
  IReadOnlyList<T> GetAll(int page, int pageSize); 

  Decimal Total();   
  T GetById(int id);
  void Post(T entity);
  void Update(int id, T entity);
  void Delete(int id);
}