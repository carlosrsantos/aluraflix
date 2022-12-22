using Aluraflix.Data;
using Aluraflix.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraFlix.Repositories;

public class CategoryRepository : IRepository<Category>
{
  private AluraflixContext _context;

  public IReadOnlyList<Category> GetAll(int page, int pageSize)
  {
    
    var categories = _context
        .Categories
        .AsNoTracking()
        .Skip(page * pageSize)
        .Take(pageSize)
        .OrderBy(x => x.Id)
        .ToList();
    return categories;   
  }

  public Category GetById(int id)
  {
    throw new NotImplementedException();
  }

  public void Post(Category entity)
  {
    throw new NotImplementedException();
  }

  public void Update(int id, Category entity)
  {
    throw new NotImplementedException();
  }
  public void Delete(int id)
  {
    throw new NotImplementedException();
  }

  public decimal Total()
  {
    return _context.Categories.AsNoTracking().Count();
  }
}