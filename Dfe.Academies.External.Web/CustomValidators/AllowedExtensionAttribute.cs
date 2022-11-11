using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.CustomValidators
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;
		public AllowedExtensionsAttribute(string[] extensions)
		{
			_extensions = extensions;
		}

		protected override ValidationResult IsValid(
			object? value, ValidationContext validationContext)
		{

			if (value is List<IFormFile> { Count: > 0 } files)
			{
				foreach (var file in files)
				{
					string fileName = file.FileName;
					string extension = Path.GetExtension(fileName);
					if (!_extensions.Contains(extension.ToLower()))
					{
						return new ValidationResult(GetErrorMessage(fileName));
					}
				}

			}

			return ValidationResult.Success!;
		}

		public string GetErrorMessage(string fileName)
		{
			return $"File Type is not Allowed, file: {fileName}. Allowed Types are: {string.Join(", ", _extensions)} ";
		}
	}
}
