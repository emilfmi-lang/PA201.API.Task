using System.ComponentModel.DataAnnotations;

namespace FirstApp.Api.Attributes;

public class FileTypeAttribute : ValidationAttribute
{
    private readonly string[] _allowedTypes;
    public FileTypeAttribute(string[] allowedTypes)
    {
        _allowedTypes = allowedTypes;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var listFiles = new List<IFormFile>();
        if (value is IFormFile file)
        {
            listFiles.Add(file);
        }
        if (value is IFormFile[] files)
        {
            listFiles.AddRange(files);
        }
        if(listFiles != null )
        {

        }
        return base.IsValid(value, validationContext);
    }

}
