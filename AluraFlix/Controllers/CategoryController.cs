using Aluraflix.Data;
using Aluraflix.Extensions;
using Aluraflix.Models;
using Aluraflix.ViewModel.Categories;
using Aluraflix.ViewModels.Videos;
using AluraFlix.Repositories;
using AluraFlix.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aluraflix.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    private CategoryRepository _categoryRepository;
    private AluraflixContext _context;

    public CategoryController(AluraflixContext context, CategoryRepository categoryRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
    }

    [Route("/api/v1/categories")]
    [HttpGet]
    public IActionResult Get(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 5
    )
    {
        try{

            var count = _categoryRepository.Total();
            var categories = _categoryRepository.GetAll(page, pageSize);
            return Ok(new ResultViewModel<dynamic>
                    (new
                    {
                        total = count,
                        page,
                        pageSize,
                        categories
                    }
                    )
                );
        }
        catch(DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Category>("Não foi possível listar as categories."));
        }
    }

    [Route("/api/v1/categories/{id}")]
    [HttpGet]
    public IActionResult GetById(
    [FromRoute] int id)
    {
        var category = _categoryRepository.GetById(id);
        if (category != null)
            return Ok(new ResultViewModel<Category>(category));
        else
            return NotFound(new ResultViewModel<Category>("Categoria não encontrada."));
    }

    [Route("/api/v1/categories")]
    [HttpPost]
    public IActionResult PostCategory(
        [FromBody] EditorCategoryViewModel category)
    {

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        try
        {
            var newCategory = new Category
            {
                Title = category.Title.ToUpper(),
                Color = category.Color,
                Videos = null
            };
            _categoryRepository.Post(newCategory);
            return Created($"/api/categories/{newCategory.Id}", new ResultViewModel<Category>(newCategory));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Category>("Não foi possível incluir a categoria.Verifique as informações e tente novamente."));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
        }
    }

    [Route("/api/v1/categories/{id}")]
    [HttpPut]
    public IActionResult UpdateCategory(
        [FromRoute] int id,
        [FromBody] EditorCategoryViewModel category)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

        try
        {
            var categoryToUpdate = _categoryRepository.GetById(id);
            if (categoryToUpdate != null)
            {

                categoryToUpdate.Title = category.Title.ToUpper();
                categoryToUpdate.Color = category.Color;

                _categoryRepository.Update(categoryToUpdate);
                
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

    [Route("/api/v1/categories/{id}")]
    [HttpDelete]
    public IActionResult DeleteCategory(
        [FromRoute] int id)
    {
        try
        {
            var categoryToDelete = _categoryRepository.GetById(id);
            if (categoryToDelete != null)
            {
                _categoryRepository.Delete(categoryToDelete);
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

    [Route("/api/v1/categories/{categoryName}/videos")]
    [HttpGet]
    public IActionResult GetVideosByCategory(
        [FromRoute] string categoryName,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 5
    )
    {
        try
        {
            var total = _categoryRepository.TotalByCategory(categoryName);
            var videos = _categoryRepository.GetVideosByCategory(
                categoryName, page, pageSize);
            return Ok(new ResultViewModel<dynamic>
                (new
                {
                    total,
                    page = page,
                    pageSize,
                    videos
                }
                )
            );
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
        }
    }
}