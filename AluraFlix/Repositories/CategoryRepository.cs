using Aluraflix.Data;
using Aluraflix.Models;
using Aluraflix.ViewModel.Categories;
using Microsoft.EntityFrameworkCore;

namespace AluraFlix.Repositories;

public class CategoryRepository : IRepository<Category>
{
  private AluraflixContext _context;

  public CategoryRepository(AluraflixContext context)
  {
    _context = context;
  }

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
  public decimal? Total()
  {
    return _context.Categories.Count();
  }

  public Category? GetById(int id)
  {
    var category = _context.Categories.FirstOrDefault(x => x.Id == id);
    return category != null ? category : null;
  }

  public Category Post(Category entity)
  {    
    _context.Categories.Add(entity);
    _context.SaveChanges();
    return entity;
  }

  public void Update(Category entity)
  {
    _context.Categories.Update(entity);
    _context.SaveChanges();
  }
  public void Delete(Category entity)
  {
    _context.Categories.Remove(entity);
    _context.SaveChanges();
  }
}