using Aluraflix.Data;
using Aluraflix.Extensions;
using Aluraflix.Models;
using Aluraflix.ViewModel.Categories;
using AluraFlix.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aluraflix.Controllers;

[ApiController]
[Route(route)]
public class CategoryController : ControllerBase
{
  private const string route = "/api/v1/categories";

  private AluraflixContext _context;

  public CategoryController(AluraflixContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<IActionResult> GetAsync()
  {
    var Categories = await _context.Categories.ToListAsync();
    return  Ok(new ResultViewModel<List<Category>>(Categories));
  }

  [HttpGet("/{id}")]
  public async Task<IActionResult> GetByIdAsync(
    [FromRoute] int id)
  {
    var Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    if (Category != null)
      return Ok(new ResultViewModel<Category>(Category));
    else
      return NotFound(new ResultViewModel<Category>("Categoria não encontrada."));
  }



  [HttpPost]
  public async Task<IActionResult> PostCategoryAsync(
    [FromBody] EditorCategoryViewModel category)
  {

    if (!ModelState.IsValid)
      return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
    try
    {
      var newCategory = new Category{
        Title = category.Title,
        Color = category.Color,
        Videos = null
      };
      await _context.Categories.AddAsync(newCategory);
      await _context.SaveChangesAsync();
      return Created($"{route}/{newCategory.Id}", new ResultViewModel<Category>(newCategory));
    }
    catch(DbUpdateException e)
    {
      return BadRequest(new ResultViewModel<Category>("Não foi possível incluir a categoria.Verifique as informações e tente novamente."));
    }
    catch
    {
       return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
    }
  }

  [HttpPut("/{id}")]
  public async Task<IActionResult> UpdateCategoryAsync(
    [FromRoute] int id,
    [FromBody] EditorCategoryViewModel category)
  {
    try
    {
      var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
      if (categoryToUpdate != null)
      {

        categoryToUpdate.Title = category.Title;
        categoryToUpdate.Color = category.Color;

        _context.Categories.Update(categoryToUpdate);
        await _context.SaveChangesAsync();
        return Ok(new ResultViewModel<Category>(categoryToUpdate));
      }
      else
        return NotFound(new ResultViewModel<Category>("Categoria não encontrada."));
    }
    catch
    {
      return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
    }
  }

  [HttpDelete("/{id}")]
  public async Task<IActionResult> DeleteCategoryAsync(
    [FromRoute] int id)
  {
    try
    {
      var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
      if (categoryToDelete != null)
      {
        _context.Categories.Remove(categoryToDelete);
        await _context.SaveChangesAsync();
        return Ok(new ResultViewModel<Category>("Categoria deletada com sucesso."));
      }
      else
        return NotFound(new ResultViewModel<Category>("Categoria não encontrada."));
    }
    catch
    {
      return StatusCode(500, new ResultViewModel<Category>("Falha ao tentar deletar a categoria."));
    }
  }
}