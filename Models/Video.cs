using System.ComponentModel.DataAnnotations;

namespace Aluraflix.Models;

public class Video
{
  public int Id { get; set; }

  [Required(ErrorMessage = "O título do vídeo é necessário")]
  public string Title { get; set; } = "";

  [Required(ErrorMessage = "A descrição do vídeo é necessária.")]
  public string Description { get; set; } = "";

  [Required(ErrorMessage = "A url é necessária.")]
  [RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?",
    ErrorMessage = "É preciso informar uma url válida. Ex. http://meusite.com")]
  public string Url { get; set; } = "";

  public int CategoryId { get; set; }
  public Category Category { get; set; }


}