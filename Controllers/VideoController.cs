using Aluraflix.Data;
using Aluraflix.Extensions;
using Aluraflix.Models;
using Aluraflix.ViewModels.Videos;
using AluraFlix.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aluraflix.Controllers;

[ApiController]
public class VideoController : ControllerBase
{
  private AluraflixContext _context;

  public VideoController(AluraflixContext context)
  {
    _context = context;
  }

  [Route("/api/v1/videos")]
  [HttpGet]
  public async Task<IActionResult> GetAsync()
  {
    var videos = await _context.Videos.ToListAsync();
    return Ok(new ResultViewModel<List<Video>>(videos));
  }

  [Route("/api/v1/videos/{id}")]
  [HttpGet]
  public async Task<IActionResult> GetByIdAsync(
     [FromRoute] int id)
  {
    var video = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
    if (video != null)
      return Ok(new ResultViewModel<Video>(video));
    else
      return NotFound(new ResultViewModel<Video>("Conteúdo não encontrado"));
  }



  [Route("/api/v1/videos")]
  [HttpPost]
  public async Task<IActionResult> PostVideoAsync(
    [FromBody] EditorVideoViewModel video)
  {

    if (!ModelState.IsValid)
      return BadRequest(new ResultViewModel<Video>(ModelState.GetErrors()));
    try
    {
      var newVideo = new Video
      {
        Title = video.Title,
        Description = video.Description,
        Url = video.Url,
        CategoryId = video.CategoryId == 0 ? 1 : video.CategoryId 
      };
      await _context.Videos.AddAsync(newVideo);
      await _context.SaveChangesAsync();
      return Created($"api/v1/videos/{newVideo.Id}", new ResultViewModel<Video>(newVideo));
    }
    catch (DbUpdateException)
    {
      return BadRequest(new ResultViewModel<Video>("Não foi possível incluir o vídeo.Verifique as informações e tente novamente."));
    }
    catch
    {
      return StatusCode(500, new ResultViewModel<Video>("Falha interna no servidor"));
    }
  }

  [Route("/api/v1/videos/{id}")]
  [HttpPut]
  public async Task<IActionResult> UpdateVideoAsync(
    [FromRoute] int id,
    [FromBody] EditorVideoViewModel video)
  {
    try
    {
      var videoToUpdate = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
      if (videoToUpdate != null)
      {
        videoToUpdate.Title = video.Title;
        videoToUpdate.Description = video.Description;
        videoToUpdate.Url = video.Url;
        videoToUpdate.CategoryId = video.CategoryId == 0 ? 1 : video.CategoryId;
        _context.Videos.Update(videoToUpdate);
        await _context.SaveChangesAsync();
        return Ok(new ResultViewModel<Video>(videoToUpdate));
      }
      else
        return NotFound(new ResultViewModel<Video>("Conteúdo não encontrado"));
    }
    catch
    {
      return StatusCode(500, new ResultViewModel<Video>("Falha interna no servidor"));
    }
  }

  [Route("/api/v1/videos/{id}")]
  [HttpDelete]
  public async Task<IActionResult> DeleteVideoAsync(
     [FromRoute] int id)
  {
    try
    {
      var videoToDelete = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
      if (videoToDelete != null)
      {
        _context.Videos.Remove(videoToDelete);
        await _context.SaveChangesAsync();
        return Ok(new ResultViewModel<Video>("Vídeo deletado com sucesso."));
      }
      else
        return NotFound(new ResultViewModel<Video>("Conteúdo não encontrado"));
    }
    catch
    {
      return StatusCode(500, new ResultViewModel<Video>("Falha ao tentar deletar o vídeo."));
    }
  }

   [Route("/api/v1/videos/category/")]
  [HttpGet]
  public async Task<IActionResult> GetVideosBySearchCategoryTitle([FromQuery] string search)
  {
    if(String.IsNullOrEmpty(search))
    {
      var videos = await _context
        .Videos
        .AsNoTracking()
        .Include(x => x.Category)
        .Select(x => new ListVideosViewModel
        {
          Id = x.Id,
          Title = x.Title,
          Description = x.Description,
          Url = x.Url,
          Category = x.Category.Title
        }
        )
        .ToListAsync();
      return Ok(new ResultViewModel<dynamic>(new { videos }));
    }
    try
    {
      var videos = await _context
        .Videos
        .AsNoTracking()
        .Include(x => x.Category)
        .Where(x => x.Category.Title == search)
        .Select(x => new ListVideosViewModel
        {
          Id = x.Id,
          Title = x.Title,
          Description = x.Description,
          Url = x.Url,
          Category = x.Category.Title
        }
        )
        .ToListAsync();
      return Ok(new ResultViewModel<dynamic>(new { videos }));
    }
    catch
    {
      return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
    }
  }
}
