using Aluraflix.Data;
using Aluraflix.Extensions;
using Aluraflix.Models;
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

  [Route("/videos")]
  [HttpGet]
  public async Task<IActionResult> GetAsync()
  {
    var videos = await _context.Videos.ToListAsync();
    return  Ok(new ResultViewModel<List<Video>>(videos));
  }

  [Route("/videos/{id}")]
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


  [Route("/videos")]
  [HttpPost]
  public async Task<IActionResult> PostVideoAsync(
    [FromBody] Video video)
  {

    if (!ModelState.IsValid)
      return BadRequest(new ResultViewModel<Video>(ModelState.GetErrors()));
    try
    {
      var newVideo = new Video{
        Title = video.Title,
        Description = video.Description,
        Url = video.Url
      };
      await _context.Videos.AddAsync(newVideo);
      await _context.SaveChangesAsync();
      return Created($"/videos/{newVideo.Id}", new ResultViewModel<Video>(newVideo));
    }
    catch(DbUpdateException e)
    {
      return BadRequest(new ResultViewModel<Video>("Não foi possível incluir o vídeo.Verifique as informações e tente novamente."));
    }
    catch
    {
       return StatusCode(500, new ResultViewModel<Video>("Falha interna no servidor"));
    }
  }

  [Route("/videos/{id}")]
  [HttpPut]
  public async Task<IActionResult> UpdateVideoAsync(
    [FromRoute] int id,
    [FromBody] Video video)
  {
    try
    {
      var videoToUpdate = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
      if (videoToUpdate != null)
      {

        videoToUpdate = video;

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


  [Route("/videos/{id}")]
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
}
