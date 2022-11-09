using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.CustomValidators
{
	public class MaxFileSizeAttribute : ValidationAttribute
	{
		private readonly int _maxFileSize;
		public MaxFileSizeAttribute(int maxFileSize)
		{
			_maxFileSize = maxFileSize;
		}

		protected override ValidationResult IsValid(
			object? value, ValidationContext validationContext)
		{
			if (value is List<IFormFile> { Count: > 0 } files)
			{
				foreach (var file in files)
				{
					if (file.Length > _maxFileSize)
					{
						return new ValidationResult(GetErrorMessage(file.FileName));
					}
				}
			}

			return ValidationResult.Success!;
		}

		public string GetErrorMessage(string fileName)
		{
			return $"Maximum allowed file exceeded for file: {fileName}, max size is {_maxFileSize} bytes.";
		}
	}
}
