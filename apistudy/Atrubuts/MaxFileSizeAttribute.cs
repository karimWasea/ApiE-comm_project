//using System.ComponentModel.DataAnnotations;

//namespace apistudy.Atrubuts
//{
//    public class MaxFileSizeAttribute : ValidationAttribute
//    {
//        private readonly int _maxFileSize;

//        public MaxFileSizeAttribute(int maxFileSize)
//        {
//            _maxFileSize = maxFileSize;
//        }

//        protected override ValidationResult? IsValid
//            (object? value, ValidationContext validationContext)
//        {
//            var file = value as IFormFile;

//            if (file is not null)
//            {
//                if (file.Length > _maxFileSize)
//                {
//                    return new ValidationResult($"Maximum allowed size is {_maxFileSize} bytes");
//                }
//        }

//            return ValidationResult.Success;
//        }
//    }
//}
//using Microsoft.AspNetCore.Http;

//using System.ComponentModel.DataAnnotations;

//public class MaxFileSizeAttribute : ValidationAttribute
//{
//    private readonly long _maxFileSizeBytes;

//    public MaxFileSizeAttribute(int maxFileSizeMegabytes)
//    {
//        _maxFileSizeBytes = maxFileSizeMegabytes * 1024 * 1024; // Convert megabytes to bytes
//    }

//    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//    {
//        if (value is IFormFile file)
//        {
//            if (file.Length > _maxFileSizeBytes)
//            {
//                // Consider using localization or resource files for error messages
//                return new ValidationResult($"Maximum allowed size is {_maxFileSizeBytes / (1024 * 1024)} MB");
//            }
//        }

//        return ValidationResult.Success;
//    }
//}
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maxFileSizeBytes;

    public MaxFileSizeAttribute(int maxFileSizeMegabytes)
    {
        _maxFileSizeBytes = maxFileSizeMegabytes * 10242 * 10242; // Convert megabytes to bytes
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFileCollection files)
        {
            foreach (var file in files)
            {
                if (file.Length > _maxFileSizeBytes)
                {
                    // Consider using localization or resource files for error messages
                    return new ValidationResult($"Maximum allowed size is {_maxFileSizeBytes / (1024 * 1024)} MB");
                }
            }

            return ValidationResult.Success;
        }

        // Return a validation error if the value is not an IFormFileCollection
        return new ValidationResult("Value is not an IFormFileCollection.");
    }
}
