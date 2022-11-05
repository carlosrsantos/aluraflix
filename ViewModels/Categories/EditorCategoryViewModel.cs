using System.ComponentModel.DataAnnotations;

namespace Aluraflix.ViewModel.Categories;
public class EditorCategoryViewModel
{
  [Required(ErrorMessage = "O título é obrigatório")]
  [StringLength(30, MinimumLength = 3, ErrorMessage = "O título deve conter entre 3 e 30 caracteres")]
  public string Title { get; set; }

  [Required(ErrorMessage = "É preciso informar uma cor em Hexadecimal.")]
  [RegularExpression(@"^#?([0-9a-fA-F]{2})([0-9a-fA-F]{2})([0-9a-fA-F]{2})|([0-9a-fA-F]{3})$",
  ErrorMessage = "Formato de cor inválida. Tente seguindo o exemplo: #fff, #000")]
  public string Color { get; set; }
}
