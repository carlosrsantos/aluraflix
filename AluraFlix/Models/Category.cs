using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Aluraflix.Models;
public class Category
{
    public int Id { get; set; }

    private string _title = "";
    public string Title
    {
        get
        {
            return _title;
        }

        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidationException("O título do vídeo é necessário");

            if (value.Length < 3 || value.Length > 30)
                throw new ValidationException("O título deve conter entre 3 e 30 caracteres");

            _title = value;
        }
    }
    
    private string _color = "";
    public string Color
    {
        get
        {
            return _color;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidationException("É preciso informar uma cor em Hexadecimal.");
            if (!IsValidColor(value))
                throw new ValidationException("Formato de cor inválida. Tente seguindo o exemplo: #fff, #000");
            _color = value;

        }
    }
    public ICollection<Video> Videos { get; set; }

    public static bool IsValidColor(string color)
    {
        Regex regex = new Regex(@"^#?([0-9a-fA-F]{2})([0-9a-fA-F]{2})([0-9a-fA-F]{2})|([0-9a-fA-F]{3})$", RegexOptions.IgnoreCase);
        var isValid = regex.Match(color);
        if (isValid.Success)
            return true;
        else return false;
    }

}
