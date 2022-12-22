using System.Text.RegularExpressions;

namespace Aluraflix.Models;

public class Video
{
    public int Id { get; set; }
    private string _title ="";
    public string Title
    {
        get
        {
            return _title;
        }

        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("O título do vídeo é necessário.");

            if (value.Length < 3 || value.Length > 30)
                throw new Exception("O título deve conter entre 3 e 30 caracteres.");

            _title = value;
        }
    }
    

    private string _description = "";
    public string Description
    {
        get
        {
            return _description;
        }

        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("A descrição do vídeo é necessária.");

            _description = value;
        }
    }

    private string _url = "";
    public string Url
    {
        get
        {
            return _url;
        } 
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("A url é necessária.");
            if (!IsValidUrl(value))
                throw new Exception("É preciso informar uma url válida.Ex.http://meusite.com");
            _url = value;

        }
    }

    public int CategoryId { get; set; } = 0;
    public Category Category { get; set; } = null;


    public static bool IsValidUrl(string url)
    {
        Regex regex = new Regex(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?",RegexOptions.IgnoreCase);
        var isValid = regex.Match(url);
        if (isValid.Success)
            return true;
        else return false;
    }

}