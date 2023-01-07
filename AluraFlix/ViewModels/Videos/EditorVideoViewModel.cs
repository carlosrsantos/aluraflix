using System.ComponentModel.DataAnnotations;

namespace Aluraflix.ViewModels.Videos
{
  public class EditorVideoViewModel
  {
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "O título deve conter entre 3 e 30 caracteres")]
    public string Title { get; set; }= "";

    [Required(ErrorMessage = "A descrição é necessária.")]
    public string Description { get; set; }= "";

    [Required(ErrorMessage = "A url é necessária.")]
    [RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?",
    ErrorMessage = "É preciso informar uma url válida. Ex. https://www.meusite.com")]
    public string Url { get; set; }= "";
    public int CategoryId { get; set; } =0;

  }
}