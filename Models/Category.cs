using System.ComponentModel.DataAnnotations;

namespace Aluraflix.Models;
public class Category
{
  public int Id { get; set; }

  [Required(ErrorMessage = "O título da categoria é necessário")]
  public string Title { get; set; } = "";

  [Required(ErrorMessage = "É necessário informar uma cor. Ex.: white, #fff, pink, #000")]
  public string Color { get; set; } = "";

  public ICollection<Video> Videos { get; set; }
}
