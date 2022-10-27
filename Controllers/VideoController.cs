using Aluraflix.Data;
using Aluraflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aluraflix.Controllers;

[ApiController]
public class VideoController : ControllerBase
{
  [Route("/videos")]
  [HttpGet]
  public async Task<IActionResult> GetAsync(
    [FromServices] AluraflixContext context)
 => Ok(context.Videos.ToList());
  
  [Route("/videos/{id}")]
  [HttpGet]
  public async Task<IActionResult> GetByIdAsync(
    [FromRoute] int id,
    [FromServices] AluraflixContext context)
    {
      var video = await context.Videos.FirstOrDefaultAsync(x => x.Id == id);
      if(video != null)
        return Ok(video);
      else
        return NotFound();
    }


  [Route("/videos")]
  [HttpPost]
  public async Task<IActionResult> PostVideoAsync(
    [FromBody] Video video,
    [FromServices] AluraflixContext context)
    {
      try
      {
        await context.Videos.AddAsync(video);
        await context.SaveChangesAsync();
        return Created($"/videos/{video.Id}", video);
      }
      catch
      {
        return BadRequest("Ops... Verifique as informações e tente novamente.");
      }      
    }
  
  [Route("/videos/{id}")]
  [HttpPut]
  public async  Task<IActionResult> UpdateVideoAsync(
    [FromRoute] int id,
    [FromBody] Video video,
    [FromServices] AluraflixContext context)
 {
    try
    {
      var videoToUpdate = await context.Videos.FirstOrDefaultAsync(x => x.Id == id);
      if(videoToUpdate != null)
      {
        videoToUpdate.Id = id;
        videoToUpdate.Title = video.Title;
        videoToUpdate.Description = video.Description;
        videoToUpdate.Url = video.Url;

        context.Videos.Update(videoToUpdate);
        await context.SaveChangesAsync();
        return Ok(videoToUpdate);
      }
      else
        return NotFound();
    }
    catch
    {
      return StatusCode(500, "Falha ao atualizar vídeo.");
    }
 }
 
  
  [Route("/videos/{id}")]
  [HttpDelete]
  public async Task<IActionResult> DeleteVideoAsync(
    [FromRoute] int id,
    [FromServices] AluraflixContext context)
    {
      try
      {
        var videoToDelete = await context.Videos.FirstOrDefaultAsync(x => x.Id == id);
        if(videoToDelete != null)
        {
          context.Videos.Remove(videoToDelete);
          await context.SaveChangesAsync();
          return NoContent();
        }
        else 
          return NotFound();        
      }
      catch 
      {
        return StatusCode(500, "Falha ao deletar vídeo.");
      }
    }
}
