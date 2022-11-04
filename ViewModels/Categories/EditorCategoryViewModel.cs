using System.ComponentModel.DataAnnotations;

namespace Aluraflix.ViewModel.Categories;
public class EditorCategoryViewModel
{
  [Required(ErrorMessage = "O título é obrigatório")]
  [StringLength(30, MinimumLength = 3, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
  public string Title { get; set; }

   [Required(ErrorMessage = "É necessário informar uma cor. Ex.: white, #fff, pink, #000")]
  public string Color { get; set; }
}
